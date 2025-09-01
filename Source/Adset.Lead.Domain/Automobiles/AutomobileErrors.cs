using Adset.Lead.Domain.Abstractions;

namespace Adset.Lead.Domain.Automobiles;

public static class AutomobileErrors
{
    public static readonly Error NotActive = new(
        "AutomobileErrors.NotActive",
        "O cadastro do automóvel não está ativo.");
        
    public static readonly Error CreationFailed = new(
        "Automobile.CreationFailed",
        "Falha ao criar o cadastro do automóvel.");
    
    public static readonly Error NotFound = new(
        "AutomobileErrors.NotFound",
        "Registro do automóvel não encontrado.");
    
    public static readonly Error PortalPackageNotSet = new(
        "AutomobileErrors.PortalPackageNotSet",
        "Portal/Package não está configurado para este automóvel.");
}