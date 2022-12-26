using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace InstaShare.EntityFrameworkCore
{
    public static class InstaShareDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<InstaShareDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<InstaShareDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}
