using PensionHackathonBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionHackathonBackend.Application.Interfaces
{
    /* Интерфейс файла CSV для облегения добаления новых методов */
    public interface IFileCSVService
    {
        Task<List<FileCSV>> GetAllFilesCSV();
        
        Task<Guid> CreateFileCSV(FileCSV fileCSV);

        Task<Guid> UpdateFileCSV(Guid id, string fileName, string filePath, DateTime dateAdded);

        Task<Guid> DeleteFileCSV(Guid id);
    }
}