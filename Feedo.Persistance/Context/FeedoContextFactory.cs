using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Feedo.Persistance.Context;

namespace RapSys.Infrastructure.Persistence.Contexts
{
    public class FeedoContextFactory : IDesignTimeDbContextFactory<FeedoContext>
    {
        public FeedoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FeedoContext>();

            optionsBuilder
                .UseSqlServer(
                "Server=Server=TFML19593\\DBSQLEXPRESS;Database=Feedo;Trusted_Connection=True;connect timeout=500;MultipleActiveResultSets=True;",
                   b => b.MigrationsAssembly(typeof(FeedoContext).Assembly.FullName));

            return new FeedoContext(optionsBuilder.Options);
        }
    }
}
