using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClubWebSite.Models;
using System.IO;
using System.Web.UI.WebControls;
using ClubWebSite.Helpers;

namespace ClubWebSite.Controllers
{
    public class PicController : Controller
    {
        
        public ActionResult Index()
        {
            ClubDbContext dbContext = new ClubDbContext();
            var club = dbContext.Clubs.FirstOrDefault();
            if (club == null)
            {
                /*If a club does not exist, user is prompted to create one.*/
                ViewBag.ClubId = 0;
                return View(new List<Pic>());
            }
            else
            {
                var pics = GetPics(dbContext, club.Id);
                ViewBag.ClubId = club.Id;
                return View(pics);
            }
        }
   

        [HttpPost]
        public ActionResult ToggleImageActivation(int id)
        {
            ClubDbContext dbContext = new ClubDbContext();
            var pic = GetPicById(dbContext, id);
            pic.IsActivated = !pic.IsActivated;
            dbContext.SaveChanges();
            return Json(pic.IsActivated ? "true" : "false");
        }

        [HttpPost]
        public ActionResult SetImageAsDefault(int clubId, int picId)
        {
            /*Sets the image as the cover image.*/
            ClubDbContext dbContext = new ClubDbContext();
            var pics = GetPics(dbContext, clubId);
            pics.ForEach(p => p.IsDefault = false);//Remove default image.
            var pic = GetPicById(dbContext, picId);
            pic.IsDefault = true;
            dbContext.SaveChanges();
            return Json("ok");
        }

        [HttpPost]
        public ActionResult DeleteImage(int picId)
        {
            ClubDbContext dbContext = new ClubDbContext();
            var pic = GetPicById(dbContext, picId);
            if (pic != null)
            {               
                dbContext.Pics.Remove(pic);
                dbContext.SaveChanges();
                if (System.IO.File.Exists(Server.MapPath(Config.ImageUploadPath + pic.FileName)))
                {
                    System.IO.File.Delete(Server.MapPath(Config.ImageUploadPath + pic.FileName));
                }
                return Json("ok");
            }
            return Json("error");
        }

        [HttpPost]
        public ActionResult UploadImage(int clubId, HttpPostedFileBase file)
        {
            ClubDbContext dbContext = new ClubDbContext();
            var club = dbContext.Clubs.SingleOrDefault(c => c.Id == clubId);
            if (file == null)
            {
                return RedirectToAction("Index");
            }

            /*Geting the file name*/
            string filename = System.IO.Path.GetFileName(file.FileName);
            /*Saving the file in server folder*/
            file.SaveAs(Server.MapPath(Config.ImageUploadPath + filename));

           
            
            int greatestOrder = club.Pics.Count > 0 ? club.Pics.Max(m => m.DisplayOrder) : 0;  //gets the max display order.      
            club.Pics.Add(new Pic
            {
                FileName = filename,
                DisplayOrder = ++greatestOrder,
                Club = club,
                IsActivated = true,
                IsDefault = club.Pics.Count == 0
            });

            dbContext.SaveChanges();

            return RedirectToAction("Index"); 
        }

        [HttpPost]
        public ActionResult UpdateDisplayOrders(int clubId, int[] idList, int[] orderList)
        {
            /*Called when user changes the order of the image rows by drag & drop.
             For each id in idList, its order is stored in corresponding index in orderList.*/
            ClubDbContext dbContext = new ClubDbContext();
            var pics = GetPics(dbContext, clubId);
            for (int i = 0; i < idList.Length; i++)
            {
                int id = idList[i];
                var pic = pics.Single(p => p.Id == id);
                pic.DisplayOrder = orderList[i];
            }

            dbContext.SaveChanges();
            return Json("ok");
        }

        private List<Pic> GetPics(ClubDbContext dbContext, int clubId)
        {
            var pics = dbContext.Pics.Where(p => p.ClubId == clubId).OrderBy(s => s.DisplayOrder).ToList();
            return pics;
        }

        private Pic GetPicById(ClubDbContext dbContext, int picId)
        {
            var pic = dbContext.Pics.SingleOrDefault(p => p.Id == picId);
            return pic;
        }
    }
}