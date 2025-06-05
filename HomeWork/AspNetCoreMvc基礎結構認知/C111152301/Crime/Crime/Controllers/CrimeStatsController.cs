using Crime.Data;
using Crime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;
public class CrimeStatsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CrimeStatsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int? page, string? year, string? month, string? sortBy, string? sortOrder)
    {
        var query = _context.CrimeStats.AsQueryable();

        // 篩選條件
        if (!string.IsNullOrEmpty(year) && int.TryParse(year, out var y))
            query = query.Where(c => c.Year == y);

        if (!string.IsNullOrEmpty(month) && int.TryParse(month, out var m))
            query = query.Where(c => c.Month == m);

        // 排序邏輯
        bool descending = sortOrder == "desc";

        query = sortBy switch
        {
            "Year" => descending ? query.OrderByDescending(c => c.Year) : query.OrderBy(c => c.Year),
            "Month" => descending ? query.OrderByDescending(c => c.Month) : query.OrderBy(c => c.Month),
            "TotalCases" => descending ? query.OrderByDescending(c => c.TotalCases) : query.OrderBy(c => c.TotalCases),
            "RapeCases" => descending ? query.OrderByDescending(c => c.RapeCases) : query.OrderBy(c => c.RapeCases),
            "RobberyCases" => descending ? query.OrderByDescending(c => c.RobberyCases) : query.OrderBy(c => c.RobberyCases),
            "OtherCases" => descending ? query.OrderByDescending(c => c.OtherCases) : query.OrderBy(c => c.OtherCases),
            _ => query.OrderBy(c => c.Year).ThenBy(c => c.Month), // 預設排序
        };

        // 分頁
        int pageSize = 20;
        int pageNumber = page ?? 1;

        var pagedData = query.ToPagedList(pageNumber, pageSize);

        // 提供給下拉選單用
        ViewBag.Years = _context.CrimeStats.Select(c => c.Year).Distinct().OrderByDescending(y => y).ToList();
        ViewBag.Months = Enumerable.Range(1, 12).ToList();

        // 把目前選擇傳回 View 顯示
        ViewBag.SortBy = sortBy;
        ViewBag.SortOrder = sortOrder;
        ViewBag.Year = year;
        ViewBag.Month = month;

        return View(pagedData);
    }

    public IActionResult Chart()
    {
        var data = _context.CrimeStats
            .GroupBy(c => new { c.Year, c.Month })
            .OrderBy(c => c.Key.Year).ThenBy(c => c.Key.Month)
            .Select(g => new
            {
                Date = $"{g.Key.Year}/{g.Key.Month:D2}",
                Total = g.Sum(x => x.TotalCases),
                Rape = g.Sum(x => x.RapeCases),
                Robbery = g.Sum(x => x.RobberyCases),
                Other = g.Sum(x => x.OtherCases)
            }).ToList();

        ViewBag.Labels = data.Select(d => d.Date).ToList();
        ViewBag.Total = data.Select(d => d.Total).ToList();
        ViewBag.Rape = data.Select(d => d.Rape).ToList();
        ViewBag.Robbery = data.Select(d => d.Robbery).ToList();
        ViewBag.Other = data.Select(d => d.Other).ToList();

        return View();
    }
    //public async Task<IActionResult> Chart()
    //{
    //    var data = await _context.CrimeStats.OrderBy(c => c.Year).ToListAsync();
    //    return View(data);
    //}

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var stat = await _context.CrimeStats.FindAsync(id);
        if (stat == null) return NotFound();
        return View(stat);
    }

    [Authorize(Roles = "User,Admin")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CrimeStat stat)
    {
        if (ModelState.IsValid)
        {
            _context.CrimeStats.Add(stat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(stat);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var stat = await _context.CrimeStats.FindAsync(id);
        if (stat == null) return NotFound();
        return View(stat);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(CrimeStat stat)
    {
        if (ModelState.IsValid)
        {
            _context.Update(stat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(stat);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var stat = await _context.CrimeStats.FindAsync(id);
        if (stat == null) return NotFound();
        return View(stat);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var item = _context.CrimeStats.Find(id);
        if (item != null)
        {
            _context.CrimeStats.Remove(item);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}