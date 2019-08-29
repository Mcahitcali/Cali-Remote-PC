using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cali_Remote_Pc.Entity.DBContext
{
    public class CrpDBContext: IdentityDbContext<User>
    {
        public CrpDBContext(DbContextOptions<CrpDBContext> options):base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Action> Actions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Client>()
                .HasOne(u => u.User)
                .WithMany(c => c.Clients)
                .HasForeignKey(c=>c.UserId)
                .IsRequired(true);

            modelBuilder.Entity<Action>()
                .HasOne(u => u.User)
                .WithMany(c => c.Actions)
                .HasForeignKey(c => c.UserId)
                .IsRequired(true);
        }
    }
}
