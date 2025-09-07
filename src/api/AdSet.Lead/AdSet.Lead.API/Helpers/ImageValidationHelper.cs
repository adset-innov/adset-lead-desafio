namespace AdSet.Lead.API.Helpers;

public static class ImageValidationHelper
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxFileSize = 5 * 1024 * 1024; // 5MB 

    public static string? Validate(List<IFormFile>? files)
    {
        if (files == null || files.Count == 0)
            return null;

        foreach (var file in files)
        {
            switch (file.Length)
            {
                case 0:
                    return $"File {file.FileName} is empty.";
                case > MaxFileSize:
                    return $"File {file.FileName} exceeds the 5MB limit.";
            }

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(ext))
                return $"Extension {ext} is not supported for file {file.FileName}.";
        }

        return null;
    }

    public static string? Validate(IFormFile? file)
    {
        return file == null ? null : Validate([file]);
    }
}