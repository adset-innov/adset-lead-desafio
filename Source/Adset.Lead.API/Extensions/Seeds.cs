using System.Data;
using System.Text.Json;
using Adset.Lead.Application.Abstractions.Data;
using Adset.Lead.Domain.Automobiles;
using Bogus;
using Dapper;

namespace Adset.Lead.API.Extensions;

internal static class Seeds
{
    public static void Seeding(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        // Verifica se já existem automóveis no banco
        var existingCount = connection.QuerySingleOrDefault<int>("SELECT COUNT(*) FROM Automobiles");
        if (existingCount > 0)
        {
            Console.WriteLine("Dados de seed já existem no banco. Pulando seeding.");
            return;
        }

        var faker = new Faker("pt_BR");

        List<object> automobiles = new();
        List<object> portalPackages = new();

        var brands = new[] { "Toyota", "Honda", "Volkswagen", "Chevrolet", "Ford", "Hyundai", "Nissan", "Fiat" };
        var colors = new[] { "Branco", "Prata", "Preto", "Vermelho", "Azul", "Cinza", "Bege" };
        
        for (int i = 0; i < 10; i++)
        {
            var automobileId = Guid.NewGuid();
            var brand = faker.PickRandom(brands);
            var model = GenerateModelForBrand(brand, faker);
            var year = faker.Random.Int(2010, 2024);
            var plate = GenerateBrazilianPlate(faker);
            var km = faker.Random.Bool(0.8f) ? faker.Random.Int(5000, 150000) : (int?)null;
            var color = faker.PickRandom(colors);
            var price = faker.Random.Decimal(25000, 120000);
            
            // Gera múltiplas features como lista
            var featuresArray = GenerateRandomOptionalFeaturesArray(faker, price);
            var featuresJson = JsonSerializer.Serialize(featuresArray.Select(f => (int)f).ToArray());
            
            // Gera URLs de fotos fake
            var photos = GeneratePhotos(faker, 2, 5);

            automobiles.Add(new
            {
                Id = automobileId,
                Brand = brand,
                Model = model,
                Year = year,
                Plate = plate,
                Km = km,
                Color = color,
                Price = price,
                Features = featuresJson,
                Photos = JsonSerializer.Serialize(photos)
            });

            // Cria um PortalPackage para cada automóvel
            var portal = faker.PickRandom<Portal>();
            var package = faker.PickRandom<Package>();
            
            portalPackages.Add(new
            {
                Id = Guid.NewGuid(),
                AutomobileId = automobileId,
                Portal = portal.ToString(),
                Package = package.ToString()
            });
        }

        // Insere automóveis
        const string automobileSql = """
                           INSERT INTO Automobiles
                           (Id, Brand, Model, Year, Plate, Km, Color, Price, Features, Photos)
                           VALUES(@Id, @Brand, @Model, @Year, @Plate, @Km, @Color, @Price, @Features, @Photos);
                           """;

        // Insere portal packages
        const string portalPackageSql = """
                           INSERT INTO PortalPackages
                           (Id, AutomobileId, Portal, Package)
                           VALUES(@Id, @AutomobileId, @Portal, @Package);
                           """;

        connection.Execute(automobileSql, automobiles);
        connection.Execute(portalPackageSql, portalPackages);
        
        Console.WriteLine($"Seeding concluído: {automobiles.Count} automóveis criados.");

    }

    // Gera modelos de carros e específicos para cada marca
    private static string GenerateModelForBrand(string brand, Faker faker) => brand switch
    {
        "Toyota" => faker.PickRandom("Corolla", "Camry", "Hilux", "Etios", "Yaris"),
        "Honda" => faker.PickRandom("Civic", "Accord", "Fit", "HR-V", "CR-V"),
        "Volkswagen" => faker.PickRandom("Golf", "Jetta", "Polo", "T-Cross", "Tiguan"),
        "Chevrolet" => faker.PickRandom("Onix", "Cruze", "S10", "Tracker", "Spin"),
        "Ford" => faker.PickRandom("Ka", "Focus", "Ecosport", "Ranger", "Edge"),
        "Hyundai" => faker.PickRandom("HB20", "Elantra", "Creta", "Tucson", "Santa Fe"),
        "Nissan" => faker.PickRandom("March", "Versa", "Sentra", "Kicks", "X-Trail"),
        "Fiat" => faker.PickRandom("Uno", "Palio", "Strada", "Toro", "Compass"),
        _ => faker.Vehicle.Model()
    };

    private static string GenerateBrazilianPlate(Faker faker)
    {
        // Formato brasileiro: ABC-1234 ou ABC-1A23 (Mercosul)
        var letters = faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        var isMercosul = faker.Random.Bool(0.3f); // 30% chance de ser Mercosul
        
        if (isMercosul)
        {
            var digit = faker.Random.Int(0, 9);
            var letter = faker.Random.Char('A', 'Z');
            var lastDigits = faker.Random.String2(2, "0123456789");
            return $"{letters}-{digit}{letter}{lastDigits}";
        }
        else
        {
            var numbers = faker.Random.String2(4, "0123456789");
            return $"{letters}-{numbers}";
        }
    }

    private static OptionalFeatures[] GenerateRandomOptionalFeaturesArray(Faker faker, decimal price = 0)
    {
        var availableFeatures = new[] 
        { 
            OptionalFeatures.AirConditioning,
            OptionalFeatures.Alarm,
            OptionalFeatures.Airbag,
            OptionalFeatures.AbsBrake,
            OptionalFeatures.Mp3Player
        };
        
        // Determina quantas features baseado no preço
        int featureCount;
        if (price >= 100000) // Carros premium - 3 a 5 features
        {
            featureCount = faker.Random.Int(3, 5);
        }
        else if (price >= 70000) // Carros médios - 2 a 4 features
        {
            featureCount = faker.Random.Int(2, 4);
        }
        else if (price >= 50000) // Carros básicos - 1 a 3 features
        {
            featureCount = faker.Random.Int(1, 3);
        }
        else // Carros econômicos - 1 a 2 features
        {
            featureCount = faker.Random.Int(1, 2);
        }
        
        // Garante que não exceda o número de features disponíveis
        featureCount = Math.Min(featureCount, availableFeatures.Length);
        
        // Seleciona features aleatórias sem repetição e retorna como array
        return faker.PickRandom(availableFeatures, featureCount).ToArray();
    }

    private static List<Photo> GeneratePhotos(Faker faker, int minCount, int maxCount)
    {
        var count = faker.Random.Int(minCount, maxCount);
        var photos = new List<Photo>();
        
        for (int i = 0; i < count; i++)
        {
            var photoUrl = $"https://picsum.photos/800/600?random={faker.Random.Int(1, 1000)}";
            photos.Add(new Photo(photoUrl));
        }
        
        return photos;
    }
}