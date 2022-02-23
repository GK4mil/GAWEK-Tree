using gawektree_back.Entities;
using Microsoft.EntityFrameworkCore;

namespace gawektree_back.Persistence
{
    public class TreeContext : DbContext
    {


        public TreeContext(DbContextOptions<TreeContext> options)
            : base(options)
        {
        }

        public DbSet<Record> Records { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Record>().HasData(new Record() { name = "root", parentGuid=Guid.Empty, guid=Guid.NewGuid()});
        }
    }
}
