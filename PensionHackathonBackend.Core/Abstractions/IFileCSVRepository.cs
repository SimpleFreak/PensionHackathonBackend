using PensionHackathonBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionHackathonBackend.Core.Abstractions
{
    public interface IFileCSVRepository
    {
        Task<Guid> Create(FileCSV file);

        Task<Guid> Delete(Guid id);

        Task<List<FileCSV>> Get();

        Task<Guid> Update(Guid id, string fileName, string filePath, DateTime dateAdded);
    }
}