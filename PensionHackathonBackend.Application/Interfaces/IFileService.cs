﻿using PensionHackathonBackend.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PensionHackathonBackend.Application.Interfaces
{
    /* Интерфейс файла CSV для облегения добаления новых методов */
    public interface IFileService
    {
        Task<int> SaveFileAsync(IFormFile file);

        Task DeleteFileAsync(int fileId);

        Task<List<FileRecord>> GetFilesAsync();

        Task<FileRecord> GetFileByIdAsync(int fileId);
    }
}