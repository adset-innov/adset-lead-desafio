
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
            return new FilterRequest
            {
                Placa = queryString[nameof(Carro.Placa)],
                MarcaId = Convert.ToInt32(queryString[nameof(FilterRequest.MarcaId)]),
                ModeloId = Convert.ToInt32(queryString[nameof(FilterRequest.ModeloId)]),
                AnoMin = Convert.ToInt32(queryString[nameof(FilterRequest.AnoMin)]),
                AnoMax = Convert.ToInt32(queryString[nameof(FilterRequest.AnoMax)]),
                TemFoto = Convert.ToBoolean(queryString[nameof(FilterRequest.TemFoto)]),
                OptionalId = Convert.ToInt32(queryString[nameof(FilterRequest.OptionalId)]),
                PrecoMin = Convert.ToDouble(queryString[nameof(FilterRequest.PrecoMin)]),
                PrecoMax = Convert.ToDouble(queryString[nameof(FilterRequest.PrecoMax)]),
                Cor = queryString[nameof(Carro.Cor)],
                CurrentPage = Convert.ToInt32(queryString[nameof(FilterRequest.CurrentPage)]),
                PageSize = Convert.ToInt32(queryString[nameof(FilterRequest.PageSize)]),
                SortAction = queryString[nameof(FilterRequest.SortAction)],
                SortBy = queryString[nameof(FilterRequest.SortBy)],
            };
        }
    }
}
