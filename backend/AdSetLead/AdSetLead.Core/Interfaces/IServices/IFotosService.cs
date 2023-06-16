
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

        /// <summary>
        /// Constroi o caminho do local da foto no server
        /// </summary>
        /// <param name="imagens"></param>
        /// <returns></returns>
        List<Imagem> PhotoPathBuilder(List<Imagem> imagens);
    }
}
