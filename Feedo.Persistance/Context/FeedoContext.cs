using Feedo.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedo.Persistance.Context
{
    public class FeedoContext : DbContext
    {
        public FeedoContext(DbContextOptions<FeedoContext> options) : base(options)
        {

        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=TFML19593\\DBSQLEXPRESS;Database=Feedo;Trusted_Connection=True;connect timeout=500;MultipleActiveResultSets=True"); 
            }
        }
    }
}
