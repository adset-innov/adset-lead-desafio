using AdSet.Lead.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AdSet.Lead.Infrastructure.Services;

public class LocalImageStorageService(IConfiguration configuration) : IImageStorageService
{
    private readonly string _rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    private readonly string _baseUrl = configuration["App:BaseUrl"] ?? "http://localhost:5100";

    public async Task<string> SaveImageAsync(Stream fileStream, string fileName, string folder)
    {
        var folderPath = Path.Combine(_rootFolder, folder);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        var filePath = Path.Combine(folderPath, newFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(stream);
        }

        var absolutePath = $"{_baseUrl}/uploads/{folder}/{newFileName}".Replace("\\", "/");
        return absolutePath;
    }

    public void DeleteImage(string relativePath)
    {
        var fullPath = Path.Combine(_rootFolder, relativePath.TrimStart('/'));
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}