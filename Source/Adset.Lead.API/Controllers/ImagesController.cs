using Microsoft.AspNetCore.Mvc;

namespace Adset.Lead.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly string _imageDirectory;

    public ImagesController(IWebHostEnvironment environment)
    {
        _environment = environment;
        _imageDirectory = Path.Combine("C:", "_adset.images");
        
        // Criar o diretório se não existir
        if (!Directory.Exists(_imageDirectory))
        {
            Directory.CreateDirectory(_imageDirectory);
        }
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { error = "Nenhum arquivo foi enviado" });
        }

        // Validar tipo de arquivo
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        
        if (!allowedExtensions.Contains(fileExtension))
            return BadRequest(new { error = "Tipo de arquivo não permitido. Use: " + string.Join(", ", allowedExtensions) });
        
        // Validar tamanho do arquivo (máximo 5MB)
        if (file.Length > 5 * 1024 * 1024)
            return BadRequest(new { error = "Arquivo muito grande. Tamanho máximo: 5MB" });

        try
        {
            // Gerar nome único para o arquivo
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_imageDirectory, fileName);

            // Salvar o arquivo
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Retornar a URL da imagem e o nome do arquivo
            var imageUrl = $"/api/images/{fileName}";
            
            return Ok(new { 
                url = imageUrl,
                fileName = fileName,
                originalName = file.FileName,
                size = file.Length
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Erro interno do servidor: {ex.Message}" });
        }
    }

    [HttpGet("{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        var filePath = Path.Combine(_imageDirectory, fileName);
        
        if (!System.IO.File.Exists(filePath))
            return NotFound("Imagem não encontrada");
        
        var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
        var contentType = fileExtension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".webp" => "image/webp",
            _ => "application/octet-stream"
        };

        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, contentType);
    }

    [HttpDelete("{fileName}")]
    public IActionResult DeleteImage(string fileName)
    {
        var filePath = Path.Combine(_imageDirectory, fileName);
        
        if (!System.IO.File.Exists(filePath))
            return NotFound("Imagem não encontrada");
        
        try
        {
            System.IO.File.Delete(filePath);
            return Ok("Imagem excluída com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao excluir imagem: {ex.Message}");
        }
    }
}
