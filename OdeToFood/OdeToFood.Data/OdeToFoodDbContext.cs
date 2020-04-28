using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    // sle note: main gateway into the database and all tables and other connecting objects.
    public class OdeToFoodDbContext : DbContext
    {
        // sle note: Inject the Configuration string (dependency injection).
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options)
            : base(options)
        {
                
        }

        // sle note: represents a single table in the database.
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
