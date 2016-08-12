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
        public int DisplayOrder { get; set; }
        public bool IsActivated { get; set; }
        public bool IsDefault { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}