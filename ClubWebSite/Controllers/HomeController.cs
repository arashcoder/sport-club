using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClubWebSite.Models;

namespace ClubWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ClubDbContext dbContext = new ClubDbContext();
            Club club = dbContext.Clubs.FirstOrDefault();
            return View(club);
        }
     
    }
}