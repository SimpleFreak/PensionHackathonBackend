using Microsoft.EntityFrameworkCore;
using PensionHackathonBackend.Core.Abstractions;
using PensionHackathonBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionHackathonBackend.DataAccess.Repositories
{
    /* Репозиторий файла CSV для дальнейшей реализации CRUD запросов */
    public class FileCSVRepository(PensionHackathonDbContext context) : IFileCSVRepository
    {
        private readonly PensionHackathonDbContext _context = context;

        /* Получение пользователей */
        public async Task<List<FileCSV>> Get()
        {
            var fileEntities = await _context.FileCSVs
                .AsNoTracking()
                .ToListAsync();

            var files = fileEntities
                .Select(file => FileCSV
                    .Create(file.Id, file.FileName, file.FilePath, file.DateAdded).fileCSV)
                .ToList();

            return files;
        }

        /* Создание нового пользователя */
        public async Task<Guid> Create(FileCSV file)
        {
            var (fileCSV, _) = FileCSV.Create(file.Id, file.FileName, file.FilePath, file.DateAdded);

            await _context.FileCSVs.AddAsync(fileCSV);
            await _context.SaveChangesAsync();

            return fileCSV.Id;
        }

        /* Обновление пользователя */
        public async Task<Guid> Update(Guid id, string fileName, string filePath, DateTime dateAdded)
        {
            await _context.FileCSVs
                .Where(file => file.Id == id)
                    .ExecuteUpdateAsync(set => set
                        .SetProperty(file => file.FileName, file => fileName)
                        .SetProperty(file => file.FilePath, file => filePath)
                        .SetProperty(file => file.DateAdded, file => dateAdded));

            return id;
        }

        /* Удаление пользователя */
        public async Task<Guid> Delete(Guid id)
        {
            await _context.FileCSVs
                .Where(file => file.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
