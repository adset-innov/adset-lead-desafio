
using AdSetLead.Core.Interfaces.IBac;
using AdSetLead.Core.Interfaces.IServices;
using AdSetLead.Core.Model;
using AdSetLead.Core.Models;
using AdSetLead.Core.Responses;
using AdSetLead.Core.Services;
using AdSetLead.Core.Validators;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace AdSetLead.Core.Bac
{
    public class CarroBac : ICarroBac
    {
        private readonly IValidator<Carro> _carroValidator;
        private readonly IFotosService _fotosService;

        public CarroBac()
        {
            _carroValidator = new CarroValidator();
            _fotosService = new FotosService();
        }

        /// <summary>
        /// BAC para Busca de carro por Request
        /// </summary>
        /// <param name="carros"></param>
        /// <returns></returns>
        public CarroResponse BuscarCarrosPorRequestBac(CarroResponse response)
        {
            response.ResponseData.ForEach(c => _fotosService.PhotoPathBuilder(c.Imagens));;

            return response;
        }

        /// <summary>
        /// Bac para registro de veiculo
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public CarroResponse RegistrarCarroBac(HttpRequest httpRequest)
        {
            CarroResponse response = new CarroResponse();           
            NameValueCollection formData = httpRequest.Form;

            string opcionaisJSON = formData[nameof(Carro.Opcionais)];
            List<Opcional> opcionais = JsonConvert.DeserializeObject<List<Opcional>>(opcionaisJSON);

            Carro carro = new Carro
            {
                Ano = int.TryParse(formData[nameof(Carro.Ano)], out int ano) ? ano : 0,
                Cor = formData[nameof(Carro.Cor)],
                MarcaId = int.TryParse(formData[nameof(Carro.MarcaId)], out int marcaId) ? marcaId : 0,
                ModeloId = int.TryParse(formData[nameof(Carro.ModeloId)], out int modeloId) ? modeloId : 0,
                Placa = formData[nameof(Carro.Placa)],
                Preco = double.TryParse(formData[nameof(Carro.Preco)], out double preco) ? preco : 0,
                Kilometragem = double.TryParse(formData[nameof(Carro.Kilometragem)], out double km) ? km : 0,
                Opcionais = opcionais
            };

            ValidationResult validationResult = _carroValidator.Validate(carro, actions => actions.IncludeRuleSets("Insert"));
            if (validationResult.IsValid)
            {
                foreach(ValidationFailure error in validationResult.Errors)
                {
                    response.AddValidationMessage(error.ErrorMessage);
                }
            }

            response.ResponseData.Add(carro);

            return response;
        }


        /// <summary>
        /// Bac para Atualizar o veiculo
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public CarroResponse AtualizarCarroBac(HttpRequest httpRequest)
        {
            CarroResponse response = new CarroResponse();
            NameValueCollection formData = httpRequest.Form;

            string opcionaisJSON = formData[nameof(Carro.Opcionais)];
            List<Opcional> opcionais = JsonConvert.DeserializeObject<List<Opcional>>(opcionaisJSON);

            Carro carro = new Carro
            {
                Id = int.TryParse(formData[nameof(Carro.Id)], out int id) ? id : 0,
                Ano = int.TryParse(formData[nameof(Carro.Ano)], out int ano) ? ano : 0,
                Cor = formData[nameof(Carro.Cor)],
                MarcaId = int.TryParse(formData[nameof(Carro.MarcaId)], out int marcaId) ? marcaId : 0,
                ModeloId = int.TryParse(formData[nameof(Carro.ModeloId)], out int modeloId) ? modeloId : 0,
                Placa = formData[nameof(Carro.Placa)],
                Preco = double.TryParse(formData[nameof(Carro.Preco)], out double preco) ? preco : 0,
                Kilometragem = double.TryParse(formData[nameof(Carro.Kilometragem)], out double km) ? km : 0,
                Opcionais = opcionais
            };

            ValidationResult validationResult = _carroValidator.Validate(carro, actions => actions.IncludeRuleSets("Insert"));
            if (validationResult.IsValid)
            {
                foreach (ValidationFailure error in validationResult.Errors)
                {
                    response.AddValidationMessage(error.ErrorMessage);
                }
            }

            response.ResponseData.Add(carro);

            return response;
        }
    }
}
