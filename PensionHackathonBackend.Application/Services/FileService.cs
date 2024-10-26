using PensionHackathonBackend.Application.Interfaces;
using PensionHackathonBackend.Core.Abstractions;
using PensionHackathonBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace PensionHackathonBackend.Application.Services
{
    /* Класс сервиса файла CSV по реализации репозитория файла CSV */
    public class FileService(IWebHostEnvironment environment, IFileServiceRepository fileServiceRepository) : IFileService
    {
        private readonly IWebHostEnvironment _environment = environment;
        private readonly IFileServiceRepository _fileServiceRepository = fileServiceRepository;
    
        /* Сохранение файлов в директорию */
        public async Task<int> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) throw new ArgumentException("File is invalid.");

            string uploadPath = Path.Combine(_environment.WebRootPath, "files");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileRecord = FileRecord.Create(file.FileName, DateTime.Today);
            var fileName = $"{fileRecord.fileRecord.Id}_{fileRecord.fileRecord.FileName}";
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _fileServiceRepository.AddFileRecordAsync(fileRecord.fileRecord);

            return fileRecord.fileRecord.Id;
        }

        /* Удаление файла */
        public async Task DeleteFileAsync(int fileId)
        {
            var fileRecord = await _fileServiceRepository.GetFileRecordAsync(fileId);
            if (fileRecord == null) throw new FileNotFoundException("File not found in the database.");

            string filePath = Path.Combine(_environment.WebRootPath, "images", $"{fileRecord.Id}_{fileRecord.FileName}");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await _fileServiceRepository.DeleteFileRecordAsync(fileId);
        }

        /* Получение всех файлов */
        public async Task<List<FileRecord>> GetFilesAsync()
        {
            return await _fileServiceRepository.GetFilesAsync();
        }

        /* Получение файла по идентификатору */
        public async Task<FileRecord> GetFileByIdAsync(int fileId)
        {
            return await _fileServiceRepository.GetFileRecordAsync(fileId);
        }
    }
}
