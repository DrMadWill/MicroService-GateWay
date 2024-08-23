using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Context;

namespace Persistence;

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<GateWayContext>
{
    public GateWayContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<GateWayContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
        return new(dbContextOptionsBuilder.Options);
    }
}