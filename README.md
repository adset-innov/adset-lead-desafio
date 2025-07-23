<img src="https://github.com/adset-innov/adset-lead-desafio/blob/main/adset-lead.png">

# Desafio para candidatos (AdSet Lead)

## Solicitação:
- Criar funcionalidade para incluir, consultar, excluir e alterar cadastro de carros e na tela de consulta dos veículos possibilitar a seleção por veículo de apenas um pacote para cada portal iCarros e WebMotors, os pacotes serão (Bronze, Diamante, Platinum ou Básico), conforme layout.
- O cadastro deverá possuir os campos (Marca, Modelo, Ano, Placa, Km, Cor, Preço, lista de opcionais para atribuir ao veículo ex.: (Ar Condicionado, Alarme, Airbag, Freio ABS)).
- Deverá ser possível incluir até 15 fotos para o veículo.
- Apenas a km, opcionais e fotos não devem ser obrigatórios.
- O layout codificado deverá ser exatamente o mesmo do arquivo disponível (adset-layout.ai).
- Nos filtros de dropdown deverão ter as seguintes opções por cada drop (<b>Ano Min</b> = 2000, 2001, 2002.. até 2024 | <b>Ano Max</b> = 2000, 2001, 2002.. até 2024 | <b>Preço</b> = 10mil a 50mil, 50mil a 90mil, +90mil | <b>Fotos</b> = Com Fotos, Sem Fotos | <b>Cor</b> = Listar as cores com os valores em distinct dos veículos inseridos).

Após terminar seu teste submeta um pull request e aguarde seu feedback.

## Instruções:
- Criar Projeto no VSCODE para o Frond-end ultizando Angular CLI 12.x e NodeJS v16.x
- Criar Projeto no Visual Studio para o Back-end (endpoint) do tipo ASP.NET Web Application com Template MVC/WebAPI do tipo Restfull
- A tela de estoque / consulta deverá ser desenvolvida conforme o layout (https://github.com/adset-innov/adset-lead-desafio/blob/main/adset-layout.ai) criado no programa Adobe Illustrator.
- Deixe a estrutura completa do Migration para o Entity Framework Code-First pronta para apenas executarmos e gerar o banco e tabelas.
- Utilizar os conceitos de DDD, OO, POCO e SOLID que você julgar necessário;

## Pré-requisitos:
- HTML5, CSS, JavaScript, POO, C#, .NET 4.0+, WebApi, C#, ASP NET, SQL, LINQ, Entity Framework, Code First, Angular 12 ou superior *(TypeScript)*, Design Responsivo, WebServices (SOAP), APIs Restfull e Windows Services
- Angular Material (https://material.angular.io/)

### IDE:
 - Back-end Microsoft Visual Studio 2013+ Community (https://visualstudio.microsoft.com/pt-br/vs/community/)
 - Front-end Microsoft Visual Studio Code (https://code.visualstudio.com/download)
 
### Servidor de Banco:
 - Microsoft SQL Server 2012+ Express (https://go.microsoft.com/fwlink/p/?linkid=2216019&clcid=0x416&culture=pt-br&country=br)

## Notas:
* Lembre-se de fazer um fork deste repositório! Apenas cloná-lo vai te impedir de criar o pull request e dificultar a entrega;

## Sobre o Projeto

Este repositório contém uma aplicação de inventário de veículos desenvolvida como proposta de desafio. O back‑end foi construído em **ASP.NET Core 9** seguindo DDD e utiliza **Entity Framework Core** para persistência. Já o front‑end utiliza **Angular 12** e **Angular Material** para a interface.

No domínio principal há a entidade `Car` com diversos atributos obrigatórios, como mostrado na declaração a seguir:

```csharp
        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(50, ErrorMessage = "Brand cannot exceed 50 characters.")]
        [Column(name: "brand")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters.")]
        [Column(name: "model")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year is required.")]
        [Range(2000, 2024, ErrorMessage = "Year must be between 2000 and 2024.")]
        [Column(name: "year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Plate is required.")]
        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "Plate format is invalid.")]
        [Column(name: "plate")]
        public string Plate { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Kilometers cannot be negative.")]
        [Column(name: "km")]
        public int Km { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        [StringLength(30, ErrorMessage = "Color cannot exceed 30 characters.")]
        [Column(name: "color")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [Column(name: "price", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<CarOptional> Optionals { get; set; } = new List<CarOptional>();
        public ICollection<CarPhoto> Photos { get; set; } = new List<CarPhoto>();
public ICollection<CarPortalPackage> PortalPackages { get; set; } = new List<CarPortalPackage>();
```

O projeto expõe um controlador `CarsController` com rotas de CRUD, além de consultas com filtros e paginação. A configuração de serviços e do Swagger está definida em `Program.cs`.

```csharp
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "ADSET.DESAFIO", Version = "v1" }); });
builder.Services.AddGeneralInfrastructure(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});
```

## Como executar

1. Configure uma instância do **SQL Server** e defina a string de conexão no arquivo `ADSET.DESAFIO.API/appsettings.json` em `ConnectionStrings:MasterDbConnection`.
2. Aplique as migrations executando na pasta `ADSET.DESAFIO`:
   ```bash
   dotnet ef database update --project ADSET.DESAFIO.INFRASTRUCTURE/ADSET.DESAFIO.INFRASTRUCTURE.csproj --startup-project ADSET.DESAFIO.API
   ```
3. Inicie a API:
   ```bash
   dotnet run --project ADSET.DESAFIO.API
   ```
   O perfil `https` utilizará a porta **7096** por padrão.
4. Para o front‑end, navegue até `ADSET.DESAFIO/ADSET-DESAFIO-WEBUI` e instale as dependências:
   ```bash
   npm install
   npm start
   ```
   A aplicação Angular abrirá em `http://localhost:4200` e utilizará o proxy definido em `proxy.conf.json` para encaminhar chamadas `/api` para a API.
   
## Executando o projeto
1. Compile a solução:
   ```bash
   cd ADSET.DESAFIO
   dotnet build
   ```
2. Aplique as migrações do Entity Framework:
   ```bash
   dotnet ef database update
   ```
3. Execute os testes:
   ```bash
   dotnet test
   ```