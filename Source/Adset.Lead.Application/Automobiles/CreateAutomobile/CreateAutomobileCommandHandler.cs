using Adset.Lead.Application.Abstractions.CQRSCommunication;
using Adset.Lead.Domain.Abstractions;
using Adset.Lead.Domain.Automobiles;

namespace Adset.Lead.Application.Automobiles.CreateAutomobile;

internal sealed class CreateAutomobileCommandHandler : ICommandHandler<CreateAutomobileCommand, Guid> 
{
    private readonly IAutomobileRepository _automobileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAutomobileCommandHandler(
        IAutomobileRepository automobileRepository,
        IUnitOfWork unitOfWork)
    {
        _automobileRepository = automobileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateAutomobileCommand request, CancellationToken cancellationToken)
    {
        // Validação do comando
        var validationResult = CreateAutomobileCommandValidator.Validate(request);
        if (validationResult.IsFailure)
            return Result.Failure<Guid>(validationResult.Error);

        // Converte URLs de fotos para objetos Photo
        var photos = request.FileNames?.Select(fileName => new Photo(fileName)).ToList() ?? new List<Photo>();
        
        // Converte as features para lista, removendo duplicatas e None
        var optionalFeatures = request.OptionalFeatures?
            .Where(f => f != OptionalFeatures.None)
            .Distinct()
            .ToList() ?? new List<OptionalFeatures>();

        try
        {
            // Cria o automóvel usando o método de factory do domínio
            var automobile = Automobile.RegisterAnNewAutomobile(
                brand: request.Brand,
                model: request.Model,
                year: request.Year,
                plate: request.Plate,
                km: request.Km,
                color: request.Color,
                price: request.Price,
                features: optionalFeatures,
                photos: photos,
                portal: request.Portal,
                package: request.Package);

            // Adiciona ao repositório
            await _automobileRepository.AddAsync(automobile);

            // Salva as alterações
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(automobile.Id);
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(
                new Error("CreateAutomobile.Failed", $"Falha ao criar automóvel: {ex.Message}"));
        }
    }
}