using PensionHackathonBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionHackathonBackend.Core.Abstractions
{
    /* Интерфейс пользователя для облегчения добавления новых методов */
    public interface IFileServiceRepository
    {
        Task<Guid> AddFileRecordAsync(FileRecord fileRecord);
        Task<FileRecord> GetFileRecordAsync(Guid fileId);
        Task<List<FileRecord>> GetFilesAsync();
        Task DeleteFileRecordAsync(Guid fileId);
    }
}