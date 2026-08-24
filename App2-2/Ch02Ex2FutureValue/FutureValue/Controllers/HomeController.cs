using Microsoft.AspNetCore.Mvc;
using FutureValue.Models;

namespace FutureValue.Controllers
{
    public class HomeController : Controller
    {
        // Figure 2-12: GET request — initial form with FV of 0
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.FV = 0;
            return View();
        }

        // Figure 2-12: POST request — bind the model and calculate
        [HttpPost]
        public IActionResult Index(FutureValueModel model)
        {
            ViewBag.FV = model.CalculateFutureValue();
            return View(model);
        }
    }
}
