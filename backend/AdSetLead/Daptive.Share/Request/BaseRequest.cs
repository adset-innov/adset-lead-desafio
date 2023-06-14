
using Daptive.Share.Request.Interface;

namespace Daptive.Share.Request
{
    public class BaseRequest : IBaseRequest
    {
        /// <summary>
        /// Usuario que fez o request
        /// </summary>
        public string User { get; set; } = "System Default";

        /// <summary>
        /// Metodo Http
        /// </summary>
        public string HttpMethod { get; set; }
    }
}
