
using Daptive.Share.Response.IResponse;
using System.Collections.Generic;

namespace Daptive.Share.Response
{
    public class InquiryResponse<T> : BaseResponse, IInquiryResponse<T>
    {
        /// <summary>
        /// Construtor Vazio
        /// </summary>
        public InquiryResponse() : base()
        {
        }

        /// <summary>
        /// Contrutor que adiciona T list no response data.
        /// </summary>
        /// <param name="item"></param>
        public InquiryResponse(List<T> item) : base()
        {
            this.ResponseData = item;
        }

        /// <summary>
        /// Construtor que adiciona um objeto na lista do response data
        /// </summary>
        /// <param name="item"></param>
        public InquiryResponse(T item) : base()
        {
            this.ResponseData.Add(item);
        }
        public List<T> ResponseData { get; set; } = new List<T>();
    }
}