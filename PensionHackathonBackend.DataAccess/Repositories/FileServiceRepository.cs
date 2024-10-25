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
    public class FileServiceRepository(PensionHackathonDbContext context) : IFileServiceRepository
    {
        private readonly PensionHackathonDbContext _context = context;
        
        public async Task<Guid> AddFileRecordAsync(FileRecord fileRecord)
        {
            await _context.FileRecords.AddAsync(fileRecord);
            await _context.SaveChangesAsync();
            return fileRecord.Id;
        }

        public async Task<FileRecord?> GetFileRecordAsync(Guid fileId)
        {
            return await _context.FileRecords.FindAsync(fileId);
        }

        public async Task<List<FileRecord>> GetFilesAsync()
        {
            return await _context.FileRecords
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteFileRecordAsync(Guid fileId)
        {
            var file = await _context.FileRecords.FindAsync(fileId);
            if (file != null)
            {
                _context.FileRecords.Remove(file);
                await _context.SaveChangesAsync();
            }
        }
    }
}
