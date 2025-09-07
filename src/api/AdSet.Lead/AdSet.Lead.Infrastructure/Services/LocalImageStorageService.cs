using AdSet.Lead.Application.Interfaces;

namespace AdSet.Lead.Infrastructure.Services;

public class LocalImageStorageService : IImageStorageService
{
    private readonly string _rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];

    public async Task<string> SaveImageAsync(Stream fileStream, string fileName, string folder)
    {
        ValidateImage(fileStream, fileName);

        var folderPath = Path.Combine(_rootFolder, folder);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        var filePath = Path.Combine(folderPath, newFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(stream);
        }

        return $"/uploads/{folder}/{newFileName}".Replace("\\", "/");
    }

    public void DeleteImage(string relativePath)
    {
        var fullPath = Path.Combine(_rootFolder, relativePath.TrimStart('/'));
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    private static void ValidateImage(Stream fileStream, string fileName)
    {
        if (fileStream == null || fileStream.Length == 0)
            throw new ArgumentException("Invalid image.");

        if (fileStream.Length > 5 * 1024 * 1024)
            throw new ArgumentException("Image size cannot exceed 5MB.");

        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
            throw new ArgumentException($"Image type {extension} is not allowed.");
    }
}