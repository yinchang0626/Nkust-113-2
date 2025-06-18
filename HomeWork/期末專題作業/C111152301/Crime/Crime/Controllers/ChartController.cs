using Microsoft.AspNetCore.Mvc;

namespace Crime.Controllers
{
    using Crime.Data;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class ChartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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
    }
}
