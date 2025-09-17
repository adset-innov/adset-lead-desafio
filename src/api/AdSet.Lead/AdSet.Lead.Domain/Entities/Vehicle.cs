using AdSet.Lead.Core.Exceptions;
using AdSet.Lead.Core.Validators;
using AdSet.Lead.Domain.Enums;
using AdSet.Lead.Domain.Interfaces;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Domain.Entities;

public sealed class Vehicle : IEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime UpdatedOn { get; private set; }

    public string Brand { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public LicensePlate LicensePlate { get; private set; }
    public int Mileage { get; private set; }
    public Color Color { get; private set; }
    public decimal Price { get; private set; }

    private readonly List<VehicleOption> _options = [];

    public IReadOnlyCollection<VehicleOption> Options =>
        _options.AsReadOnly();

    private readonly List<Photo> _photos = [];
    public IReadOnlyCollection<Photo> Photos => _photos.AsReadOnly();

    private readonly List<PortalPackage> _portalPackages = [];
    public IReadOnlyCollection<PortalPackage> PortalPackages => _portalPackages.AsReadOnly();

    private Vehicle()
    {
    }

    public Vehicle(
        string brand,
        string model,
        int year,
        string licensePlate,
        string color,
        decimal price,
        int mileage = 0,
        IEnumerable<VehicleOption>? options = null,
        IEnumerable<Photo>? photos = null,
        IEnumerable<PortalPackage>? portalPackages = null
    )
    {
        var optionList = options?.ToList() ?? [];
        var photoList = photos?.ToList() ?? [];
        var portalPackageList = portalPackages?.ToList() ?? [];

        Validate(brand, model, year, price, photoList, portalPackageList);

        Id = Guid.NewGuid();
        CreatedOn = DateTime.UtcNow;
        UpdatedOn = DateTime.UtcNow;

        Brand = brand;
        Model = model;
        Year = year;
        LicensePlate = new LicensePlate(licensePlate);
        Color = new Color(color);
        Price = price;
        Mileage = mileage;
        _options.AddRange(optionList);
        _photos.AddRange(photoList);
        _portalPackages.AddRange(portalPackageList);
    }

    private static void Validate(
        string brand,
        string model,
        int year,
        decimal price,
        List<Photo> photos,
        List<PortalPackage> portalPackages
    )
    {
        StringValidator.Validate(brand, "Brand", 1, 100);

        StringValidator.Validate(model, "Model", 1, 100);

        if (year < 1900 || year > DateTime.UtcNow.Year + 1)
            throw new DomainValidationException("Invalid year.");

        if (price <= 0)
            throw new DomainValidationException("Price must be positive.");

        if (photos.Count > 15)
            throw new DomainValidationException("A vehicle can have a maximum of 15 photos.");

        if (portalPackages
            .GroupBy(p => p.Portal)
            .Any(g => g.Count() > 1))
            throw new DomainValidationException("A vehicle can only have one package per portal.");
    }

    public void UpdateDetails(
        string brand,
        string model,
        int year,
        string licensePlate,
        string color,
        decimal price,
        int mileage,
        IEnumerable<VehicleOption>? options = null
    )
    {
        Validate(brand, model, year, price, _photos, _portalPackages);

        Brand = brand;
        Model = model;
        Year = year;
        LicensePlate = new LicensePlate(licensePlate);
        Color = new Color(color);
        Price = price;
        Mileage = mileage;

        if (options is not null)
        {
            _options.Clear();
            _options.AddRange(options);
        }

        UpdatedOn = DateTime.UtcNow;
    }

    public void AddOption(VehicleOption option)
    {
        if (option is null)
            throw new DomainValidationException("Option cannot be null.");

        if (_options.Any(o => o.Name.Equals(option.Name, StringComparison.OrdinalIgnoreCase)))
            throw new DomainValidationException($"Option '{option.Name}' already exists.");

        _options.Add(option);
        UpdatedOn = DateTime.UtcNow;
    }

    public void RemoveOption(Guid optionId)
    {
        var option = _options.FirstOrDefault(o => o.Id == optionId);
        if (option is null)
            throw new DomainValidationException("Option not found.");

        _options.Remove(option);
        UpdatedOn = DateTime.UtcNow;
    }


    public void AddPhoto(Photo photo)
    {
        if (photo is null)
            throw new DomainValidationException("Photo cannot be null.");

        if (_photos.Count >= 15)
            throw new DomainValidationException("A vehicle can have a maximum of 15 photos.");

        photo.SetVehicleId(Id);
        _photos.Add(photo);
        UpdatedOn = DateTime.UtcNow;
    }

    public void RemovePhoto(Guid photoId)
    {
        var photo = _photos.FirstOrDefault(p => p.Id == photoId);
        if (photo is null)
            throw new DomainValidationException("Photo not found.");

        _photos.Remove(photo);
        UpdatedOn = DateTime.UtcNow;
    }

    public void AddOrUpdatePortalPackage(PortalPackage portalPackage)
    {
        if (portalPackage is null)
            throw new DomainValidationException("PortalPackage cannot be null.");

        var existingPortalPackage = _portalPackages.FirstOrDefault(p => p.Portal == portalPackage.Portal);

        if (existingPortalPackage is not null)
            _portalPackages.Remove(existingPortalPackage);

        _portalPackages.Add(portalPackage);
        UpdatedOn = DateTime.UtcNow;
    }

    public void RemovePortalPackage(Portal portal)
    {
        var existingPortalPackage = _portalPackages.FirstOrDefault(p => p.Portal == portal);
        if (existingPortalPackage is null)
            throw new DomainValidationException($"No package found for portal {portal}.");

        _portalPackages.Remove(existingPortalPackage);
        UpdatedOn = DateTime.UtcNow;
    }
}