using Adset.Lead.Application.Abstractions.CQRSCommunication;
using Adset.Lead.Domain.Abstractions;
using Adset.Lead.Domain.Automobiles;

namespace Adset.Lead.Application.Automobiles.DeleteAutomobile;

internal sealed class DeleteAutomobileCommandHandler : ICommandHandler<DeleteAutomobileCommand>
{
    private readonly IAutomobileRepository _automobileRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteAutomobileCommandHandler(IAutomobileRepository automobileRepository, IUnitOfWork unitOfWork)
    {
        _automobileRepository = automobileRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(DeleteAutomobileCommand request, CancellationToken cancellationToken)
    {
        var automobile = await _automobileRepository.GetByIdAsync(request.AutomobileId, cancellationToken);
        
        if (automobile is null)
        {
            return Result.Failure(AutomobileErrors.NotFound);
        }

        await _automobileRepository.DeleteAsync(request.AutomobileId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
