using CRUDWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebApplication.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TaskbookModel> TaskbookData { get; set; }
        public DbSet<UserModel> UserData { get; set; }
    }
}
