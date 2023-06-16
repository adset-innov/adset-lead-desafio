
using System.Collections.Generic;

namespace Daptive.Share.Response.IResponse
{
    public interface IInquiryResponse<T>
    {
        List<T> ResponseData { get; set; }
    }
}
