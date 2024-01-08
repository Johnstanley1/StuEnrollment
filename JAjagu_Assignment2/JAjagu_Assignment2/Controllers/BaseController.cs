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
using Microsoft.AspNetCore.Mvc;

namespace JAjagu_Assignment2.Controllers
{
    public class BaseController : Controller
    {
        // set the page visit date 
        public void SetPageVisitDate(string key, string value)
        {
            if (!Request.Cookies.ContainsKey(key))
            {
                Response.Cookies.Append(key, value, new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30) // Extend the cookie's life to 30 minutes

                });
            }

        }

        // retrieve the page visit date
        public string GetPageVisitDate(string key)
        {
            if (Request.Cookies.ContainsKey(key))
            {
                return Request.Cookies[key];
            }
            return null;
        }
    }
}
