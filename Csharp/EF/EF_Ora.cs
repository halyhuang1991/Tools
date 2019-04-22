using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.EntityFramework;
namespace Csharp.EF
{
    public class EF_Ora
    {
        public class CommonDBContext : DbContext
        {
            //public CommonDBContext(DbContextOptions options) : base(options)
            //{
            //}

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                var logger = new LoggerFactory();
                var log=logger.CreateLogger("test");
                optionsBuilder.UseLoggerFactory(logger);
                //optionsBuilder.UseOracle("DATA SOURCE=127.0.0.1:1521/tjims;PASSWORD=test;PERSIST SECURITY INFO=True;USER ID=test");
                base.OnConfiguring(optionsBuilder);
            }
            //public DbSet<Department> Department { get; set; }
        }
        
    }
}