/*
 * Programmed by : Johnstanley Ajagu
 * Student Id: 8864315
 * Revision history:
 *      1-nov-2023: Project created
 *      1-nov-2023: Designed views
 *      4-nov-2023: updated student enrollment logic
 *      5-nov-2023: implemented cookies for the app's first visit date
 *      8-nov-2023: implemented push emails
 *      12-nov-2023: updated counts and status
 *      14-nov-2023: project completed
 */
using JAjagu_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JAjagu_Assignment2.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // render the page visit date and time on the app's home page
        public IActionResult Index()
        {
            string cookiesValue = GetPageVisitDate("Home_Cookies_Date");

            DateTime cookiesDate;

            if (DateTime.TryParse(cookiesValue, out cookiesDate))
            {
                SetPageVisitDate("Home_Cookies_Date", cookiesDate.ToString());

                ViewBag.CookiesPageVisitDate = cookiesDate;
            }
            else
            {
                SetPageVisitDate("Home_Cookies_Date", DateTime.Now.ToString());

                ViewBag.CookiesPageVisitDate = DateTime.Now;

            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}