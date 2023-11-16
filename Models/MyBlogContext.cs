

using Album.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Album.MyContext {
    public class MyBlogContext : IdentityDbContext<AppUser> {
        public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
        {
            // Phương thức khởi tạo này chứa options để kết nối đến MS SQL Server
            // Thực hiện điều này khi Inject trong dịch vụ hệ thống
        }

        public DbSet<Article> Article {set;get;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            // Bỏ tiền tố AspNet
            foreach( var entityType in modelBuilder.Model.GetEntityTypes()) {
                string? tableName = entityType.GetTableName();
                if(!string.IsNullOrEmpty(tableName)) {
                    if(tableName.StartsWith("AspNet")) {
                        entityType.SetTableName(tableName.Substring(6));
                    }
                }
            }
        }
    }
}