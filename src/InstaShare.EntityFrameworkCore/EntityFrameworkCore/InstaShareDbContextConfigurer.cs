using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace InstaShare.EntityFrameworkCore
{
    public static class InstaShareDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<InstaShareDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<InstaShareDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
