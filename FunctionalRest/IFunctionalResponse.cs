using System.Net;

namespace FunctionalRest
{
    public interface IFunctionalResponse<TData>
    {
        public bool GetSuccess();
        public HttpStatusCode GetCode();
        public TData GetData();        
    }
}
