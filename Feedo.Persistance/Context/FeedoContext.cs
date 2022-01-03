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
    }
}
