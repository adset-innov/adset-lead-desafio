
using AdSetLead.Core.Models;
using System.Collections.Generic;
using System.Web;

namespace AdSetLead.Core.Interfaces.IServices
{
    public interface IFotosService
    {
        /// <summary>
        /// Faz upload the fotos na pasta Upload
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        List<Imagem> UploadFotos(HttpRequest httpRequest);
    }
}
