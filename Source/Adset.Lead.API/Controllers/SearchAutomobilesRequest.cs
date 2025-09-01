namespace Adset.Lead.API.Controllers;

public class SearchAutomobilesRequest
{
    public string? Plate { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? YearMin { get; set; }
    public int? YearMax { get; set; }
    public decimal? PriceMin { get; set; }
    public decimal? PriceMax { get; set; }
    public string? Color { get; set; }
    public bool? HasPhotos { get; set; }
    public int? Portal { get; set; }
    public int? Package { get; set; }
    public int? Feature { get; set; }
}
