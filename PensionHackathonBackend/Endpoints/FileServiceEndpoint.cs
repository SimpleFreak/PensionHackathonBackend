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
    public static IEndpointRouteBuilder AddFileServiceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("UploadFile", UploadFile)
            .DisableAntiforgery();;
        app.MapDelete("DeleteFile{id:guid}", DeleteFile);
        app.MapGet("GetAllFiles", GetAllFilesAsync);
        app.MapGet("GetFile/{id:guid}", GetFileByIdAsync);
        return app;
    }

    private static async Task<IResult> UploadFile(IFormFile file, FileService fileService)
    {
        if (file == null || file.Length == 0)
        {
            return Results.BadRequest("Invalid file.");
        }

        try
        {
            var fileId = await fileService.SaveFileAsync(file);
            return Results.Ok(new { Message = "File uploaded successfully!", FileId = fileId });
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Error uploading file: {ex.Message}");
        }
    }

    private static async Task<IResult> DeleteFile(Guid id, FileService fileService)
    {
        try
        {
            await fileService.DeleteFileAsync(id);
            return Results.Ok("File deleted successfully.");
        }
        catch (FileNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Error deleting file: {ex.Message}");
        }
    }
    
    private static async Task<IResult> GetAllFilesAsync(FileService fileService)
    {
        try
        {
            var files = await fileService.GetFilesAsync();
            return Results.Ok(files);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Error retrieving files: {ex.Message}");
        }
    }

    private static async Task<IResult> GetFileByIdAsync(Guid id, FileService fileService)
    {
        var file = await fileService.GetFileByIdAsync(id);
        return file != null
            ? Results.Ok(file)
            : Results.NotFound(new { Message = "File not found." });
    }
}