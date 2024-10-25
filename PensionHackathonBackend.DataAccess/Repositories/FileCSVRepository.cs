using Microsoft.EntityFrameworkCore;
using PensionHackathonBackend.Core.Abstractions;
using PensionHackathonBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionHackathonBackend.DataAccess.Repositories
{
    public class FileCSVRepository(PensionHackathonDbContext context) : IFileCSVRepository
    {
        private readonly PensionHackathonDbContext _context = context;

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

        public async Task<Guid> Create(FileCSV file)
        {
            var (fileCSV, Error) = FileCSV.Create(file.Id, file.FileName, file.FilePath, file.DateAdded);

            await _context.FileCSVs.AddAsync(fileCSV);
            await _context.SaveChangesAsync();

            return fileCSV.Id;
        }

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

        public async Task<Guid> Delete(Guid id)
        {
            await _context.FileCSVs
                .Where(file => file.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
