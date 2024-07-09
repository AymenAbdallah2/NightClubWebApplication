using NightClub.Models;
using Microsoft.EntityFrameworkCore;

namespace NightClub.DataAccess
{
    public class NightClubContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<IdentityCard> IdentityCards { get; set; }
        public DbSet<MembershipCard> MembershipCards { get; set; }

        public NightClubContext(DbContextOptions<NightClubContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityCard>()
                .HasIndex(ic => ic.CardNumber)
                .IsUnique();
        }
    }
}