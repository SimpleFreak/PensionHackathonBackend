using Microsoft.EntityFrameworkCore;
using PensionHackathonBackend.Core.Models;

namespace PensionHackathonBackend.DataAccess
{
    /* Класс контекста базы данных */
    public class PensionHackathonDbContext(DbContextOptions<PensionHackathonDbContext> options)
        : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<FileCSV> FileCSVs { get; set; }
    }
}
