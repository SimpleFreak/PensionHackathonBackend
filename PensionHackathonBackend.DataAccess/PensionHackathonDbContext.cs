using Microsoft.EntityFrameworkCore;
using PensionHackathonBackend.Core.Models;

namespace PensionHackathonBackend.DataAccess
{
    /* Класс контекста базы данных */
    public class PensionHackathonDbContext(DbContextOptions<PensionHackathonDbContext> options)
        : DbContext(options)
    {
        /* Хранение пользователей в базе данных */
        public DbSet<User> Users { get; set; }

        /* Хранение файлов в базе данных */
        public DbSet<FileRecord> FileRecords { get; set; }
    }
}
