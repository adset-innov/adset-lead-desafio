namespace Adset.Lead.Domain.Automobiles;

public interface IAutomobileRepository
{
    Task<Automobile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca veículos aplicando os filtros disponíveis no layout.
    /// </summary>
    /// <param name="plate">Filtro por placa (texto livre ou vazio)</param>
    /// <param name="brand">Filtro por marca (texto livre ou vazio)</param>
    /// <param name="model">Filtro por modelo (texto livre ou vazio)</param>
    /// <param name="yearMin">Ano mínimo (null para ignorar)</param>
    /// <param name="yearMax">Ano máximo (null para ignorar)</param>
    /// <param name="priceRange">Faixa de preço (ex: "10mil a 50mil", "50mil a 90mil", "+90mil", null para todos)</param>
    /// <param name="hasPhotos">true=com fotos, false=sem fotos, null=ambos</param>
    /// <param name="optionals">Filtro por opcionais (texto livre ou vazio significa todos)</param>
    /// <param name="color">Filtro por cor (null ou vazio para todas)</param>
    /// <param name="orderBy">Campo de ordenação ("MarcaModelo", "Ano", "Preco", "Fotos")</param>
    /// <param name="orderAsc">Ordenação ascendente (true) ou descendente (false)</param>
    /// <param name="page">Página para paginação (começa em 1)</param>
    /// <param name="pageSize">Itens por página (ex: 100)</param>
    /// <returns>Lista paginada de veículos</returns>
    /// 
    Task<IEnumerable<Automobile>> SearchAsync(
        string? plate = null,
        string? brand = null,
        string? model = null,
        int? yearMin = null,
        int? yearMax = null,
        string? priceRange = null,
        bool? hasPhotos = null,
        string? optionals = null,
        string? color = null,
        string orderBy = "MarcaModelo",
        bool orderAsc = true,
        int page = 1,
        int pageSize = 100
    );

    /// <summary>
    /// Conta veículos aplicando os mesmos filtros da busca, para exibição de totais.
    /// </summary>
    Task<int> CountAsync(
        string? plate = null,
        string? brand = null,
        string? model = null,
        int? yearMin = null,
        int? yearMax = null,
        string? priceRange = null,
        bool? hasPhotos = null,
        string? optionals = null,
        string? color = null
    );

    /// <summary>
    /// Lista cores disponíveis cadastradas nos veículos para popular o filtro de cor.
    /// </summary>
    Task<IEnumerable<string>> GetAvailableColorsAsync();

    /// <summary>
    /// Lista opcionais disponíveis cadastrados nos veículos para popular o filtro de opcionais.
    /// </summary>
    Task<IEnumerable<string>> GetAvailableOptionalsAsync();

    #region Métodos específicos para Pacotes dos Portais (iCarros/WebMotors)
    /// <summary>
    /// Obtém o pacote selecionado para um veículo em um portal específico.
    /// </summary>
    Task<PortalPackage> GetPortalPackageAsync(Guid automobileId, Portal portal);

    /// <summary>
    /// Atualiza o pacote do veículo para um portal específico.
    /// </summary>
    Task SetPortalPackageAsync(Guid automobileId, Portal portal, Package package);

    /// <summary>
    /// Lista todos os pacotes disponíveis para um portal (para popular UI).
    /// </summary>
    Task<IEnumerable<PortalPackage>> GetAvailablePackagesForPortalAsync(PortalPackage portal);
    #endregion
    
    Task AddAsync(Automobile automobile);
    Task UpdateAsync(Automobile automobile);
    Task DeleteAsync(Guid id);
}