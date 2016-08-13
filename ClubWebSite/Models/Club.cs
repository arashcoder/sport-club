using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        [MaxLength(2000, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(1000, ErrorMessage = "Address is too long.")]
        public string Address { get; set; }
        public virtual ICollection<Pic> Pics { get; set; } 
    }
}