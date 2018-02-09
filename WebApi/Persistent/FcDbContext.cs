using DomainLibrary.Location;
using DomainLibrary.Meal;
using DomainLibrary.Menu;
using DomainLibrary.Shared;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Persistent
{
    public class FcDbContext : DbContext
    {
        public FcDbContext(DbContextOptions<FcDbContext> options) : base(options)
        {

        }

        public DbSet<Entree> Entrees { get; set; }
        public DbSet<EntreeCatagory> EntreeCatagorys { get; set; }
        public DbSet<EntreeStyle> EntreeStyles { get; set; }
        public DbSet<EntreeDetail> EntreeDetails { get; set; }
        public DbSet<EntreeDetailType> EntreeDetailTypes { get; set; }
        public DbSet<StapleFood> StapleFoods { get; set; }
        public DbSet<DomainLibrary.Member.User> Users { get; set; }
        public DbSet<Supermarket> Supermarkets { get; set; }
        public DbSet<ContactAddress> ContactAddresses { get; set; }
        public DbSet<DomainLibrary.Order.Order> Orders { get; set; }
        public DbSet<EntreePhoto> EntreePhotos { get; set; }
        public DbSet<ApplicationMenu> Menus { get; set; }
        public DbSet<DomainLibrary.Member.Member> Members { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to One
            modelBuilder.Entity<ContactAddress>()
                    .HasOne(ca => ca.Supermarket)
                    .WithOne(sm => sm.AddressInfo)
                    .HasForeignKey<Supermarket>(sm => sm.AddressRefId);
            //One to Many
            // configured by starting with "many" to "one" relationship:
            /*     
            modelBuilder.Entity<MealType>()
                    .HasMany(mt => mt.Entrees)
                    .WithOne(e => e.MealType);
            */
            // configured by starting with the "one" to "many" relationship
            modelBuilder.Entity<Entree>()
                    .HasOne(e => e.StapleFood)
                    .WithMany(sf => sf.EntreesWithCurrentStapleFood)
                    .OnDelete(DeleteBehavior.SetNull);
            //modelBuilder.Entity<User>()
            //        .HasMany(u => u.AddedEntrees)
            //        .WithOne(e => e.AddedBy)
            //        .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<User>()
            //        .HasMany(u => u.UpdatedEntrees)
            //        .WithOne(e => e.LastUpdatedBy)
            //        .OnDelete(DeleteBehavior.Restrict);


            //Many to Many
            modelBuilder.Entity<Entrees_Details>().HasKey(esds =>
                  new { esds.EntreeId, esds.EntreeDetailId });
            modelBuilder.Entity<Supermarket_EntreeDetail>().HasKey(sbe =>
                  new { sbe.SupermarketId, sbe.EntreeDetailId });
            modelBuilder.Entity<Supermarket_StapleFood>().HasKey(sbs =>
                  new { sbs.SuperMarketId, sbs.StapleFoodId });
            modelBuilder.Entity<Entrees_Orders>().HasKey(esos =>
                  new { esos.OrderId, esos.EntreeId });
        }
    }
}
