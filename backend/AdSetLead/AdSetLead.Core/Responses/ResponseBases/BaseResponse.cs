
using System.Collections.Generic;

namespace AdSetLead.Core.Responses.ResponseBases
{
    public class BaseResponse<T> : BaseMessage
    {
        /// <summary>
        /// Construtor vazio
        /// </summary>
        public BaseResponse() : base()
        {
        }

        /// <summary>
        /// Construtor para atribuir um objeto a lista de resposta.
        /// </summary>
        /// <param name="item"></param>
        public BaseResponse(List<T> item) : base()
        {
            this.ResponseData = item;
        }

        /// <summary>
        /// Construtor para adicionar um objeto para lista de resposta
        /// </summary>
        /// <param name="item"></param>
        public BaseResponse(T item) : base()
        {
            this.ResponseData.Add(item);
        }

        public List<T> ResponseData { get; set; } = new List<T>();

        public int TotalAvailableItems {  get; set; }
    }
}
