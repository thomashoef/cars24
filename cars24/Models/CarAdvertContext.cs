using System;
using Microsoft.EntityFrameworkCore;

namespace cars24.Models
{
    public class CarAdvertContext : DbContext
    {
        public CarAdvertContext(DbContextOptions<CarAdvertContext> options)
            : base(options)
        {
        }

        public DbSet<CarAdvert> CarAdverts { get; set; }
    }
}

