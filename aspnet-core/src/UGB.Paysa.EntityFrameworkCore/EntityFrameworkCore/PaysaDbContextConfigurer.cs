using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace UGB.Paysa.EntityFrameworkCore
{
    public static class PaysaDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<PaysaDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<PaysaDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}