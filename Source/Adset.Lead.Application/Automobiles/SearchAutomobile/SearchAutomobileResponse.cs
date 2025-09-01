using System.Text.Json;

namespace Adset.Lead.Application.Automobiles.SearchAutomobile;

public sealed class SearchAutomobileResponse
{
    public Guid Id { get; init; }
    public string Brand { get; init; } = null!;
    public string Model { get; init; } = null!;
    public int Year { get; init; }
    public string Plate { get; init; } = null!;
    public int? Km { get; init; }
    public string Color { get; init; } = null!;
    public decimal Price { get; init; }
    
    // Propriedade interna para o Dapper mapear
    public string FeaturesJson { get; init; } = "[]";
    
    // Propriedade pública que retorna array de inteiros
    public int[] Features 
    { 
        get 
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FeaturesJson))
                    return Array.Empty<int>();
                    
                return JsonSerializer.Deserialize<int[]>(FeaturesJson) ?? Array.Empty<int>();
            }
            catch
            {
                return Array.Empty<int>();
            }
        } 
    }
    
    // Propriedade interna para o Dapper mapear
    public string PhotosJson { get; init; } = "[]";
    
    // Propriedade pública que retorna string de Photos tratando valores nulos/vazios
    public string Photos => string.IsNullOrWhiteSpace(PhotosJson) ? "[]" : PhotosJson;
    public string? Portal { get; init; }
    public string? Package { get; init; }
}