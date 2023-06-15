
using AdSetLead.Core.Responses;
using System.Web;

namespace AdSetLead.Core.Interfaces.IBac
{
    public interface ICarroBac
    {
        CarroResponse AtualizarCarroBac(HttpRequest httpRequest);
        CarroResponse BuscarCarrosPorRequestBac(CarroResponse carros);
        CarroResponse RegistrarCarroBac(HttpRequest httpRequest);
    }
}
