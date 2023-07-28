using CRUDWebApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebApplication.Data
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TaskbookModel> TaskbookData { get; set; }
        public DbSet<UserModel> UserData { get; set; }
    }
}
