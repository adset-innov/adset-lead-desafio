
using System.Net.Http;

namespace Daptive.Share.Request.Interface
{
    public interface IBaseRequest
    {
        /// <summary>
        /// Usuario que fez o request
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// Metodo Http
        /// </summary>
        string HttpMethod {  get; set; }
    }
}
