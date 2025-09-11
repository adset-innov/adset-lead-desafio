//using AdSet.Domain.Entities;

//namespace AdSet.Application.ViewModels
//{
//    internal class VehiclesMapper
//    {
//        public static VehiclesViewModel ToViewModel(Vehicle entity)
//        {
//            if (entity == null) return null;

//            return new VehiclesViewModel
//            {
//                Make = entity.Make,
//                Model = entity.Model,
//                Year = entity.Year,
//                Plate = entity.Plate,
//                Km = entity.Km,
//                Color = entity.Color,
//                Price = entity.Price,
//                Optionais = entity.VehicleOptionals?.Select(o => o.Optional.Name).ToList() ?? new List<string>(),
//                Images = entity.VehicleImages?.Select(i => i.FilePath).ToList() ?? new List<string>(),
//                VechiclePortalPackages = entity.VechiclePortalPackages?.Select(p => p.VehicleId).ToList()
//            };
//        }

//        public static Carro ToEntity(CarroViewModel vm)
//        {
//            if (vm == null) return null;

//            return new Carro
//            {
//                Id = Guid.NewGuid(),
//                Marca = vm.Marca,
//                Modelo = vm.Modelo,
//                Ano = vm.Ano,
//                Placa = vm.Placa,
//                Km = vm.Km ?? 0,
//                Cor = vm.Cor,
//                Preco = vm.Preco,
//                Opcionais = vm.Opcionais?.Select(o => new Opcional { Id = Guid.NewGuid(), Nome = o }).ToList() ?? new List<Opcional>(),
//                Fotos = vm.Fotos?.Select(f => new Foto { Id = Guid.NewGuid(), Url = f }).ToList() ?? new List<Foto>(),
//                PacoteICarros = new PacotePacotePortal { Id = Guid.NewGuid(), Pacote = vm.PacoteICarros },
//                PacoteWebMotors = new PacotePacotePortal { Id = Guid.NewGuid(), Pacote = vm.PacoteWebMotors }
//            };
//        }

//        public static void UpdateEntity(Carro entity, CarroViewModel vm)
//        {
//            entity.Marca = vm.Marca;
//            entity.Modelo = vm.Modelo;
//            entity.Ano = vm.Ano;
//            entity.Placa = vm.Placa;
//            entity.Km = vm.Km ?? entity.Km;
//            entity.Cor = vm.Cor;
//            entity.Preco = vm.Preco;
//            entity.Opcionais = vm.Opcionais?.Select(o => new Opcional { Id = Guid.NewGuid(), Nome = o }).ToList() ?? new List<Opcional>();
//            entity.Fotos = vm.Fotos?.Select(f => new Foto { Id = Guid.NewGuid(), Url = f }).ToList() ?? new List<Foto>();
//            entity.PacoteICarros.Pacote = vm.PacoteICarros;
//            entity.PacoteWebMotors.Pacote = vm.PacoteWebMotors;
//        }

//    }
//}
