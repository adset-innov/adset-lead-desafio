
using AdSetLead.Core.Models;
using AdSetLead.Core.Request;
using System;
using System.Collections.Specialized;

namespace AdSetLead.Core.Extentions
{
    public static class FilterExtension
    {
        /// <summary>
        /// Build the filter request
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static FilterRequest BuildFilterRequest(this NameValueCollection queryString)
        {
            FilterRequest filtro = new FilterRequest();

            filtro.Placa = queryString[nameof(Carro.Placa)];
            filtro.MarcaId = Convert.ToInt32(queryString[nameof(FilterRequest.MarcaId)]);
            filtro.ModeloId = Convert.ToInt32(queryString[nameof(FilterRequest.ModeloId)]);
            filtro.AnoMin = Convert.ToInt32(queryString[nameof(FilterRequest.AnoMin)]);
            filtro.AnoMax = Convert.ToInt32(queryString[nameof(FilterRequest.AnoMax)]);
            filtro.TemFoto = Convert.ToBoolean(queryString[nameof(FilterRequest.TemFoto)]);
            filtro.TemOpcionais = Convert.ToBoolean(queryString[nameof(FilterRequest.TemOpcionais)]);
            filtro.PrecoMin = Convert.ToDouble(queryString[nameof(FilterRequest.PrecoMin)]);
            filtro.PrecoMax = Convert.ToDouble(queryString[nameof(FilterRequest.PrecoMax)]);
            filtro.Cor = queryString[nameof(Carro.Cor)];
            filtro.CurrentPage = Convert.ToInt32(queryString[nameof(FilterRequest.CurrentPage)]);
            filtro.PageSize = Convert.ToInt32(queryString[nameof(FilterRequest.PageSize)]);

            return filtro;
        }
    }
}
