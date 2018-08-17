using System;
using System.Threading.Tasks;
using Csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace Csharp.EF
{
    public class EF_MSSQL
    {
        public static void run(){
            var db = new BooksContext();

            db.book.Add(new book { name = "efbook", id = 56, booknum = 122 });
            var count = db.SaveChanges();
            int count1 = db.Database.ExecuteSqlCommand("delete from book where name='efbook'");
            int count2 = db.Database.ExecuteSqlCommand("update book set name='3' where name='sdas1'");
            Console.WriteLine("{0} records saved to database,{1} deleted,{2} updated", count, count1, count2);
            Console.WriteLine("All blogs in database:");
            db = new BooksContext();//重新从数据库取数
            foreach (var book in db.book)
            {
                Console.WriteLine(" - {0}", book.name);
            }
               
            
        }

    }
    public class BooksContext : DbContext
    {
        private DbContextOptionsBuilder _optionsBuilder;
        public DbSet<book> book { get; set; } //和表同名
        private string connstr{ get; set; }
        public BooksContext(){
           // _optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=test; User=root;Password=;"); 
           var hostname = "XPHP0004\\HALY";
           var username="haly";
            var password = "admin";
            var connString = $"Data Source={hostname};Initial Catalog=test;User ID={username};Password={password};pooling=false";
           //_optionsBuilder.UseSqlServer(connString); 
           connstr=connString;
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          // optionsBuilder=_optionsBuilder;
          optionsBuilder.UseSqlServer(this.connstr); 
        }
    }
    
}
    