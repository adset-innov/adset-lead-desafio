using System.Text;
using Dapper;

namespace Adset.Lead.Application.Automobiles.SearchAutomobile;

internal static class SearchAutomobileQueryBuilder
{
    public static (string Query, DynamicParameters Parameters) BuildQuery(SearchAutomobileQuery request)
    {
        var sqlBuilder = new StringBuilder();
        var parameters = new DynamicParameters();
        
        BuildBaseQuery(sqlBuilder);
        ApplyFilters(request, sqlBuilder, parameters);
        AddOrderBy(sqlBuilder);
        
        return (sqlBuilder.ToString(), parameters);
    }
    
    private static void BuildBaseQuery(StringBuilder sqlBuilder)
    {
        sqlBuilder.Append("""
            SELECT 
                a.Id,
                a.Brand,
                a.Model,
                a.Year,
                a.Plate,
                a.Km,
                a.Color,
                a.Price,
                a.Features AS FeaturesJson,
                a.Photos AS PhotosJson,
                pp.Portal,
                pp.Package
            FROM Automobiles a
            LEFT JOIN PortalPackages pp ON a.Id = pp.AutomobileId
            WHERE 1=1
            """);
    }
    
    private static void ApplyFilters(SearchAutomobileQuery request, StringBuilder sqlBuilder, DynamicParameters parameters)
    {
        ApplyStringFilter(request.Plate, "a.Plate", sqlBuilder, parameters, "@Plate");
        ApplyStringFilter(request.Brand, "a.Brand", sqlBuilder, parameters, "@Brand");
        ApplyStringFilter(request.Model, "a.Model", sqlBuilder, parameters, "@Model");
        ApplyStringFilter(request.Color, "a.Color", sqlBuilder, parameters, "@Color");
        
        ApplyRangeFilters(request, sqlBuilder, parameters);
        ApplyPhotosFilter(request.HasPhotos, sqlBuilder);
        ApplyPortalPackageFilters(request, sqlBuilder, parameters);
        ApplyFeatureFilter(request.Feature, sqlBuilder, parameters);
    }
    
    private static void ApplyStringFilter(string? value, string columnName, StringBuilder sqlBuilder, 
        DynamicParameters parameters, string parameterName)
    {
        if (!string.IsNullOrEmpty(value))
        {
            sqlBuilder.Append($" AND {columnName} LIKE {parameterName}");
            parameters.Add(parameterName, $"%{value}%");
        }
    }
    
    private static void ApplyRangeFilters(SearchAutomobileQuery request, StringBuilder sqlBuilder, DynamicParameters parameters)
    {
        if (request.YearMin.HasValue)
        {
            sqlBuilder.Append(" AND a.Year >= @YearMin");
            parameters.Add("@YearMin", request.YearMin.Value);
        }
        
        if (request.YearMax.HasValue)
        {
            sqlBuilder.Append(" AND a.Year <= @YearMax");
            parameters.Add("@YearMax", request.YearMax.Value);
        }
        
        if (request.PriceMin.HasValue)
        {
            sqlBuilder.Append(" AND a.Price >= @PriceMin");
            parameters.Add("@PriceMin", request.PriceMin.Value);
        }
        
        if (request.PriceMax.HasValue)
        {
            sqlBuilder.Append(" AND a.Price <= @PriceMax");
            parameters.Add("@PriceMax", request.PriceMax.Value);
        }
    }
    
    private static void ApplyPhotosFilter(bool? hasPhotos, StringBuilder sqlBuilder)
    {
        if (hasPhotos.HasValue)
        {
            if (hasPhotos.Value)
            {
                sqlBuilder.Append(" AND (a.Photos IS NOT NULL AND a.Photos != '[]' AND a.Photos != '')");
            }
            else
            {
                sqlBuilder.Append(" AND (a.Photos IS NULL OR a.Photos = '[]' OR a.Photos = '')");
            }
        }
    }
    
    private static void ApplyPortalPackageFilters(SearchAutomobileQuery request, StringBuilder sqlBuilder, DynamicParameters parameters)
    {
        if (request.Portal.HasValue)
        {
            sqlBuilder.Append(" AND pp.Portal = @Portal");
            parameters.Add("@Portal", request.Portal.Value);
        }
        
        if (request.Package.HasValue)
        {
            sqlBuilder.Append(" AND pp.Package = @Package");
            parameters.Add("@Package", request.Package.Value);
        }
    }
    
    private static void ApplyFeatureFilter(int? feature, StringBuilder sqlBuilder, DynamicParameters parameters)
    {
        if (feature.HasValue)
        {
            // Como Features é armazenado como JSON array, fazemos busca por string no JSON
            sqlBuilder.Append(" AND a.Features LIKE @Feature");
            parameters.Add("@Feature", $"%{feature.Value}%");
        }
    }
    
    private static void AddOrderBy(StringBuilder sqlBuilder)
    {
        sqlBuilder.Append(" ORDER BY a.Brand, a.Model, a.Year");
    }
}
