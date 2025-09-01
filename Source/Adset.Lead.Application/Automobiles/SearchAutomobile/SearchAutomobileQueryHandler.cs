using Adset.Lead.Application.Abstractions.CQRSCommunication;
using Adset.Lead.Application.Abstractions.Data;
using Adset.Lead.Domain.Abstractions;
using Dapper;
using System.Text.Json;

namespace Adset.Lead.Application.Automobiles.SearchAutomobile;

internal sealed class SearchAutomobileQueryHandler : IQueryHandler<SearchAutomobileQuery, IReadOnlyList<SearchAutomobileResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchAutomobileQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<SearchAutomobileResponse>>> Handle(SearchAutomobileQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        
        try
        {
            var (query, parameters) = SearchAutomobileQueryBuilder.BuildQuery(request);
            var automobiles = await connection.QueryAsync<SearchAutomobileResponse>(query, parameters);
            
            return automobiles.ToList();
        }
        catch (Exception ex)
        {
            return Result.Failure<IReadOnlyList<SearchAutomobileResponse>>(
                new Error("SearchAutomobile.Failed", $"Erro ao buscar automóveis: {ex.Message}"));
        }
    }
}