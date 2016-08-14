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
     
        public ActionResult Save()
        {          
            ClubDbContext dbContext = new ClubDbContext();
            var club = dbContext.Clubs.FirstOrDefault();
            return View(club);
        }

        [HttpPost]
        public ActionResult Save(Club club)
        {
            /*this method is used for both creating and editing a club.
              If a club does not exist, it creates one, otherwise it updates the existing club.*/
            var newClub = new Club();
                ClubDbContext dbContext = new ClubDbContext();
                if (club.Id > 0)
                {
                    var existingclub = dbContext.Clubs.SingleOrDefault(c => c.Id == club.Id);
                    if (existingclub != null)
                    {
                        existingclub.Name = club.Name;
                        existingclub.Address = club.Address;
                        existingclub.Description = club.Description;
                    }
                }
                else
                {
                    newClub.Address = club.Address;
                    newClub.Name = club.Name;
                    newClub.Description = club.Description;
                    dbContext.Clubs.Add(newClub);
                }
                dbContext.SaveChanges();

                return RedirectToAction("Index", "Pic");
           
        }

    }
}