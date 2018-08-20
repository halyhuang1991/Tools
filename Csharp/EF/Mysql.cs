using System;
using System.Data;
using System.Threading.Tasks;
using Csharp.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
//using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.MySqlClient;
namespace Csharp.EF
{
    public class Mysql
    {
         public static void run(){
            try
            {
                //net core2 的mysql引用有问题
                // var db = new RemarkContext();
                // db.remark.Add(new remark { id = 56, name = "efbook" });
                // var count = db.SaveChanges();
                // Console.WriteLine("{0} records saved to database", count);
                MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=test2;uid=root;pwd=admin;SslMode=None");

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                MySqlCommand com = con.CreateCommand();
                com.CommandText = "insert into remark values(13, '12')";
                com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
    public class RemarkContext : DbContext
    {
       
        public DbSet<remark> remark { get; set; } //和表同名
        private string connstr{ get; set; }
        public RemarkContext(){
           var connString = "Server=localhost;Port=3306;Database=test2; User=root;Password=admin;SslMode=none;";
           connstr=connString;
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(this.connstr); 
        }
    }
}