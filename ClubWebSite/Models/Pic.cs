using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClubWebSite.Models
{
    public class Pic
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int DisplayOrder { get; set; } // Display order of the image.
        public bool IsActivated { get; set; } //Whether image is displayed on home page or not.
        public bool IsDefault { get; set; }// The cover photo, which is displayed differently on home page.
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}