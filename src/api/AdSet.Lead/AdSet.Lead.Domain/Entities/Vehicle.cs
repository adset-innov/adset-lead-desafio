using AdSet.Lead.Core.Exceptions;
using AdSet.Lead.Core.Validators;
using AdSet.Lead.Domain.Interfaces;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Domain.Entities;

public sealed class Vehicle : IBaseEntity
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
    public VehicleOptions Options { get; private set; } = new(false, false, false, false);

    private readonly List<Photo> _photos = [];
    public IReadOnlyCollection<Photo> Photos => _photos.AsReadOnly();

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
        VehicleOptions? options = null,
        IEnumerable<Photo>? photos = null
    )
    {
        var photoList = photos?.ToList() ?? [];

        Validate(brand, model, year, price, photoList);

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
        Options = options ?? new VehicleOptions(false, false, false, false);
        _photos.AddRange(photoList);
    }

    private static void Validate(
        string brand,
        string model,
        int year,
        decimal price,
        ICollection<Photo> photos
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
    }

    public void AddPhoto(Photo photo)
    {
        if (_photos.Count >= 15)
            throw new DomainValidationException("A vehicle can have a maximum of 15 photos.");

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
}