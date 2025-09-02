using Adset.Lead.Application.Abstractions.CQRSCommunication;
using Adset.Lead.Domain.Abstractions;
using Adset.Lead.Domain.Automobiles;

namespace Adset.Lead.Application.Automobiles.UpdateAutomobile;

internal sealed class UpdateAutomobileCommandHandler : ICommandHandler<UpdateAutomobileCommand>
{
    private readonly IAutomobileRepository _automobilerepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateAutomobileCommandHandler(IAutomobileRepository automobilerepository, IUnitOfWork unitOfWork)
    {
        _automobilerepository = automobilerepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(UpdateAutomobileCommand request, CancellationToken cancellationToken)
    {
        var automobile = await _automobilerepository.GetByIdAsync(request.AutomobileId, cancellationToken);
        
        if (automobile is null)
            return Result.Failure(AutomobileErrors.NotFound);

        // Converte as features para lista, removendo duplicatas e None
        var optionalFeatures = request.OptionalFeatures?
            .Where(f => f != OptionalFeatures.None)
            .Distinct()
            .ToList() ?? new List<OptionalFeatures>();

        var updateResult = automobile.Update(
            brand:  request.Brand,
            model:  request.Model,
            year: request.Year,
            plate: request.Plate,
            color: request.Color,
            price: request.Price,
            km: request.Km,
            portal: request.Portal,
            package: request.Package,
            features: optionalFeatures,
            photos: request.FileNames);

        if (updateResult.IsFailure)
            return updateResult;

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("UpdateAutomobile.Failed", $"Falha ao atualizar o automóvel: {ex.Message}"));
        }
    }
}