
using Daptive.Share.Model.IModel;

namespace Daptive.Share.Request
{
    public class ModelOperationRequest<T> : BaseRequest
    {
        public ModelOperationRequest()
        {
        }

        public ModelOperationRequest(T model)
        {
            this.Model = model;
        }

        public T Model { get; }
    }
}
