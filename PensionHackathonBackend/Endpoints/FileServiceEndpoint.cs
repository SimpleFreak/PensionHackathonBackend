using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PensionHackathonBackend.Application.Services;

namespace PensionHackathonBackend.Endpoints;

public static class FileServiceEndpoint
{
    /* Добавление методов для отображения запросов по работе с файлами */
    public static IEndpointRouteBuilder AddFileServiceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("UploadFile", UploadFile)
            .DisableAntiforgery(); ;
        app.MapDelete("DeleteFile/{id:int}", DeleteFile);
        app.MapGet("GetAllFiles", GetAllFilesAsync);
        app.MapGet("GetFile/{id:int}", GetFileByIdAsync);
        return app;
    }

    /* Загрузка файлов */
    private static async Task<IResult> UploadFile(IFormFile file, FileService fileService)
    {
        if (file == null || file.Length == 0)
        {
            return Results.BadRequest("Invalid file.");
        }

        return await ExecuteWithRetry(async () =>
        {
            var fileId = await fileService.SaveFileAsync(file);
            return Results.Ok(new { Message = "File uploaded successfully!", FileId = fileId });
        });
    }

    /* Удаление файла */
    private static async Task<IResult> DeleteFile(int id, FileService fileService)
    {
        return await ExecuteWithRetry(async () =>
        {
            await fileService.DeleteFileAsync(id);
            return Results.Ok("File deleted successfully.");
        });
    }

    /* Получение всех файлов */
    private static async Task<IResult> GetAllFilesAsync(FileService fileService)
    {
        return await ExecuteWithRetry(async () =>
        {
            var files = await fileService.GetFilesAsync();
            return Results.Ok(files);
        });
    }

    /* Получение конкретного файла по идентификатору */
    private static async Task<IResult> GetFileByIdAsync(int id, FileService fileService)
    {
        return await ExecuteWithRetry(async () =>
        {
            var file = await fileService.GetFileByIdAsync(id);
            return file != null
                ? Results.Ok(file)
                : Results.NotFound(new { Message = "File not found." });
        });
    }

    /* Выполнение с повторением */
    private static async Task<IResult> ExecuteWithRetry(Func<Task<IResult>> operation, int retries = 5)
    {
        for (int attempt = 0; attempt < retries; attempt++)
        {
            try
            {
                return await operation();
            }
            catch (FileNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Attempt {attempt + 1} failed: {ex.Message}");

                if (attempt == retries - 1)
                {
                    return Results.BadRequest($"Error occurred: {ex.Message}");
                }

                await Task.Delay(1000);
            }
        }

        return Results.BadRequest("Unexpected error occurred.");
    }
}
