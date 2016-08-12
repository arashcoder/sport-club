using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClubWebSite.Models
{
    public class Club
    {
        public Club()
        {
            Pics = new List<Pic>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Pic> Pics { get; set; } 
    }
}