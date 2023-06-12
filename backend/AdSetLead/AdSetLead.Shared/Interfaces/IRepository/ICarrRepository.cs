
using System.Collections.Generic;

namespace AdSetLead.Shared.Interfaces.IRepository
{
    public interface ICarroRepository
    {
        List<string> BuscarTodosCarrosAsync();
    }
}
