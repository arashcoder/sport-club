using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClubWebSite.Helpers;
using ClubWebSite.Models;
using Microsoft.ApplicationInsights.WindowsServer;

namespace ClubWebSite.Controllers
{
    public class ClubController : Controller
    {
        // GET: Club
        public ActionResult Index()
        {
            ClubDbContext dbContext = new ClubDbContext();
            var club = dbContext.Clubs.FirstOrDefault();
            return View(club);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Club club)
        {

                var newClub = new Club();
                newClub.Address = club.Address;
                newClub.Name = club.Name;

               ClubDbContext dbContext = new ClubDbContext();
            dbContext.Clubs.Add(newClub);
            dbContext.SaveChanges();
           
            return RedirectToAction("Index", "Pic", new {id = newClub.Id});
        }

    }
}