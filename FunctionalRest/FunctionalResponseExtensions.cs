using System;

namespace FunctionalRest
{
    public static class FunctionalResponseExtensions
    {
        public static IFunctionalResponse<T> OnSuccess<T>(this IFunctionalResponse<T> response, Action<IFunctionalResponse<T>> action)
        {
            if (response.GetSuccess())
            {
                action(response);
            }
            return response;
        }

        public static IFunctionalResponse<T> OnError<T>(this IFunctionalResponse<T> response, Action<IFunctionalResponse<T>> action)
        {
            if (!response.GetSuccess())
            {
                action(response);
            }
            return response;
        }

    }
}
