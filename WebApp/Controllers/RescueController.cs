using Microsoft.AspNetCore.Mvc;
using System;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class RescueController : Controller
    {
        private readonly IRescueService _rescueService;
        private readonly AppDbContext _appDbContext;

        public RescueController(
            IRescueService rescueService,
            AppDbContext appDbContext
            )
        {
            _rescueService = rescueService;
            this._appDbContext = appDbContext;
        }

        // Index action to display rescue data
        public IActionResult Index()
        {
            var data = _rescueService.LoadData();
            return View(data);
        }

        public IActionResult ImportData()
        {
            if (_appDbContext.RescueDatas.Count() == 0)
            {
                var data = _rescueService.LoadData();
                foreach (var item in data)
                {
                    _appDbContext.RescueDatas.Add(item);


                }
                _appDbContext.SaveChanges();
                ViewBag.Message = "Data already success imported.";
            }
            else
            {
                ViewBag.Message = "Data already imported. skip import";
            }


            return View();
        }

    }
}
