using Microsoft.AspNetCore.Mvc;
using TipCalculator.Models;

namespace TipCalculator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(MealCostModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Tip15 = model.CalculateTip(15);
                ViewBag.Tip18 = model.CalculateTip(18);
                ViewBag.Tip20 = model.CalculateTip(20);
            }
            else
            {
                ViewBag.Tip15 = ViewBag.Tip18 = ViewBag.Tip20 = 0.00m;
            }

            return View("Index", model);
        }

        public IActionResult Clear()
        {
            ModelState.Clear();
            return RedirectToAction("Index");
        }
    }
}
