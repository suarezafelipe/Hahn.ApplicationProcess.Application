using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

}
