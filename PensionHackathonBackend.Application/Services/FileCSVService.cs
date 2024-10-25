using PensionHackathonBackend.Application.Interfaces;
using PensionHackathonBackend.Core.Abstractions;
using PensionHackathonBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionHackathonBackend.Application.Services
{
    /* Класс сервиса файла CSV по реализации репозитория файла CSV */
    public class FileCSVService(IFileCSVRepository fileCSVRepository)
        : IFileCSVService
    {
        private readonly IFileCSVRepository _fileCSVRepository = fileCSVRepository;

        /* Получение всех файлов CSV */
        public async Task<List<FileCSV>> GetAllFilesCSV()
        {
            return await _fileCSVRepository.Get();
        }

        /* Создание нового файла CSV */
        public async Task<Guid> CreateFileCSV(FileCSV fileCSV)
        {
            return await _fileCSVRepository.Create(fileCSV);
        }

        /* Обновление файла CSV */
        public async Task<Guid> UpdateFileCSV(Guid id,
            string fileName, string filePath, DateTime dateAdded)
        {
            return await _fileCSVRepository.Update(id,
                fileName, filePath, dateAdded);
        }

        /* Удаление файла CSV */
        public async Task<Guid> DeleteFileCSV(Guid id)
        {
            return await _fileCSVRepository.Delete(id);
        }
    }
}
