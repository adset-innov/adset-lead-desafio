using Adsetdesafio.Shared.Utils.AppServiceBase;
using System.Net;

namespace Adsetdesafio.Shared.Utils
{
    public static class ExtensionMessages
    {

        public static void ErrorMessage(this DefaultReturn retorno, string message, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            retorno.Message.Add(message);
            retorno.StatusCode = status;
        }

    }
}
