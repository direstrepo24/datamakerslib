using Microsoft.EntityFrameworkCore;

namespace datamakerslib.DataContext
{
   public class SampleAppContext : EntityContextBase<SampleAppContext>
    {
        public SampleAppContext(DbContextOptions<SampleAppContext> options) : base(options)
        { }
        ///colocar las clases
       // public DbSet<Building> Buildings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}