using System;
using System.Net;

namespace FunctionalRest
{
    public interface IFunctionalResponse<TData>
    {
        bool GetSuccess();
        HttpStatusCode GetCode();
        TData GetData();
        Exception GetException();
    }
}
