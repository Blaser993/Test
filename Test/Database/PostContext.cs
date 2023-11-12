using Microsoft.EntityFrameworkCore;
using Test.Database;
using Test.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test.Database
{
    public class PostContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PostContext;" +
            "Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
