using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Student.Models
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        

    }
}
