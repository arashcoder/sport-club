using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ClubWebSite.Models;

namespace ClubWebSite
{
    public class ClubDbContext : DbContext
    {
        public ClubDbContext(): base("ClubDb")
        {

        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Pic> Pics { get; set; }
    }
}