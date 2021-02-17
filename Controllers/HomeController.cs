using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RandomPasscode.Models;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
    [HttpGet("")]        
        public IActionResult Index()
        {
            // generating a random passcode
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] strChars = new char[14];
            Random rand = new Random();

            for (int i = 0; i < strChars.Length; i++)
            {
                strChars[i] = chars[rand.Next(0, chars.Length)];
            }
            
            string passcode = new string(strChars);
            ViewBag.Passcode = passcode;

            // to keep track of passcode count:
            if (HttpContext.Session.GetInt32("counter") == null) //checking if session exists
            {
                HttpContext.Session.SetInt32("counter", 0);
                ViewBag.Counter = HttpContext.Session.GetInt32("counter");
            }
            else
            {
                var currentCounter = (int)HttpContext.Session.GetInt32("counter")+1;
                HttpContext.Session.SetInt32("counter", currentCounter);
                ViewBag.Counter = currentCounter;
            }

            return View("Index");
        }
    }
}
