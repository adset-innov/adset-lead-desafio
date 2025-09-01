using Adset.Lead.Domain.Abstractions;
using Adset.Lead.Domain.Automobiles.Events;

namespace Adset.Lead.Domain.Automobiles;

public sealed class Automobile : Entity
{
    public Automobile(
        Guid id,
        string brand,
        string model,
        int year, string plate,
        int? km,
        string color,
        decimal price,
        List<OptionalFeatures> features,
        List<Photo> photos) : base(id)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Plate = plate;
        Km = km;
        Color = color;
        Price = price;
        Features = features ?? new List<OptionalFeatures>();
        Photos = photos;
    }

    private Automobile()
    {
    }
    
    public string Brand { get; private set; } = null!;
    public string Model { get; private set; } = null!;
    public int Year { get; private set; }
    public string Plate { get; private set; } = null!;
    public int? Km { get; private set; }
    public string Color { get; private set; } = null!;
    public decimal Price { get; private set; }

    public List<OptionalFeatures> Features { get; private set; } = new();
    public List<Photo> Photos { get; set; } = new();
    public PortalPackage? PortalPackage { get; private set; }
    
    public Portal? Portal { get; private set; }
    public Package? Package { get; private set; }
    
    public static Automobile RegisterAnNewAutomobile(
        string brand,
        string model,
        int year,
        string plate,
        int? km,
        string color,
        decimal price,
        List<OptionalFeatures> features,
        List<Photo> photos,
        Portal portal,
        Package package)
    {
        var automobile = new Automobile(
            Guid.NewGuid(),
            brand,
            model,
            year,
            plate,
            km,
            color,
            price,
            features,
            photos);

        // Define o portal e package no momento da criação
        var portalPackage = new PortalPackage(
            Guid.NewGuid(),
            automobile.Id,
            portal,
            package);
        
        automobile.PortalPackage = portalPackage;

        automobile.RaiseDomainEvent(new AutomobileRegisteredDomainEvent(automobile.Id));

        return automobile;
    }
    
    public Result Update(string brand, string model, int year, string plate, string color, decimal price, int? km,
        Portal portal, Package package, List<OptionalFeatures> features, IReadOnlyCollection<string>? photos)
    {
        if (Id == Guid.Empty)
            return Result.Failure(AutomobileErrors.NotActive);
        
        Brand = brand;
        Model = model;
        Year = year;
        Plate = plate;
        Color = color;
        Price = price;
        Km = km;
        Portal = portal;
        Package = package;
        Features = features ?? new List<OptionalFeatures>();
        
        if (photos != null)
        {
            Photos.Clear();
            
            foreach (var photoUrl in photos)
            {
                Photos.Add(new Photo(photoUrl));
            }
        }
        
        return Result.Success();
    }
    
    public Result SetPortalPackageLast(Portal portal, Package package)
    {
        if (Id == Guid.Empty)
            return Result.Failure(AutomobileErrors.NotActive);

        var portalPackage = new PortalPackage(
            Guid.NewGuid(),
            Id,
            portal,
            package);

        PortalPackage = portalPackage;
        
        return Result.Success();
    }

    public Result UpdatePortalPackage(Portal portal, Package package)
    {
        if (Id == Guid.Empty)
            return Result.Failure(AutomobileErrors.NotActive);

        if (PortalPackage == null)
            return Result.Failure(AutomobileErrors.PortalPackageNotSet);

        // Cria um novo PortalPackage com os novos valores
        var updatedPortalPackage = new PortalPackage(
            PortalPackage.Id, // Mantém o mesmo Id
            Id,
            portal,
            package);

        PortalPackage = updatedPortalPackage;
        
        return Result.Success();
    }

    public Result RemovePortalPackage()
    {
        if (Id == Guid.Empty)
            return Result.Failure(AutomobileErrors.NotActive);

        if (PortalPackage == null)
            return Result.Failure(AutomobileErrors.PortalPackageNotSet);

        PortalPackage = null;
        
        return Result.Success();
    }

}