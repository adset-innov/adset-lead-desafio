using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adsetdesafio.Shared.Utils.AppServiceBase
{
    public class DefaultReturn
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Message { get; set; } = new List<string>();
        public object Data { get; set; }
        public IActionResult Return(ControllerBase controller)
        {
            return controller.StatusCode((int)StatusCode, new
            {
                message = Message,
                data = Data
            });
        }
    }
}
