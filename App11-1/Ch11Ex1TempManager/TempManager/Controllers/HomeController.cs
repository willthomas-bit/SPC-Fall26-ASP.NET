using Microsoft.AspNetCore.Mvc;
using TempManager.Models;

namespace TempManager.Controllers
{
    public class HomeController : Controller
    {
        private TempManagerContext data { get; set; }
        public HomeController(TempManagerContext ctx) => data = ctx;

        public ViewResult Index()
        {
            var temps = data.Temps.OrderBy(t => t.Date).ToList();
            return View(temps);
        }

        [HttpGet]
        public ViewResult Add() => View(new Temp());

        [HttpPost]
        public IActionResult Add(Temp temp)
        {
            if (temp.Date != null && data.Temps.FirstOrDefault(t => t.Date == temp.Date) != null)
            {
                ModelState.AddModelError("Date", "The date already exists.");
            }
            if (temp.Low != null && temp.High != null && temp.Low > temp.High)
            {
                ModelState.AddModelError("", "The low temperature can't be greater than the high temperature.");
            }

            if (ModelState.IsValid)
            {
                data.Temps.Add(temp);
                data.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(temp);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var temp = data.Temps.Find(id);
            return View(temp);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Temp temp)
        {
            data.Remove(temp);
            data.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
