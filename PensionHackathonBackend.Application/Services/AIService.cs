using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PensionHackathonBackend.Core.Abstractions;
using PensionHackathonBackend.Core.Models;
using PensionHackathonBackend.Modules.Functionality;

namespace PensionHackathonBackend.Application.Services;

public class AIService(IConfiguration config, IWebHostEnvironment environment, IFileServiceRepository fileServiceRepository)
{
    private readonly IConfiguration _config = config;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly IFileServiceRepository _fileServiceRepository = fileServiceRepository;

    public async Task<string> GetAIResult(IFormFile file)
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
        
        return PythonAIFunctionality.ExecuteScript(config["PythonExeFilePath"], config["PythonScriptPath"], filePath);
    }
}