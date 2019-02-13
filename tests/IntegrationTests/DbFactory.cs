using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests
{
    public class DbFactory
    {
        public static NorthwindDbContext Create(bool seedData = true)
        {
            var options = new DbContextOptionsBuilder<NorthwindDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new NorthwindDbContext(options);
            if (seedData)
            {
                NorthwindInitializer.Initialize(db);
                db.SaveChanges();
            }

            return db;
        }
    }
}
