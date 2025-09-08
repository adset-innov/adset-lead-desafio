namespace AdSet.Lead.Application.Interfaces;

public interface IImageStorageService
{
    Task<string> SaveImageAsync(Stream fileStream, string fileName, string folder);
    void DeleteImage(string relativePath);
}