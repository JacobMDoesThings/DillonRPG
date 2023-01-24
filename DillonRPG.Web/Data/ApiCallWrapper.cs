
using System.Runtime.CompilerServices;

namespace DillonRPG.Web.Data;

/// <summary>
/// Extension methods for handling the Api for this web application.
/// </summary>
public static class ApiExtensionMethods
{
    /// <summary>
    /// Handles the result of an Api call, pass delegates as parameters for successful and failed Api calls.
    /// </summary>
    /// <typeparam name="T">The type returned from the Api.</typeparam>
    /// <param name="apiResponse">The response from the api.</param>
    /// <param name="success">Action to be performed in the event of success.</param>
    /// <param name="failure">Action to be performed in the event of failure.</param>
    /// <returns></returns>
    public static async Task HandleResults<T>(this Task<ApiResponse<T>> apiResponse,
          Action<ApiResponse<T>>? success = null,
          Action<ApiResponse<T>>? failure = null)
        where T : class
    {
        var result = await apiResponse;

        if (result.IsSuccessStatusCode)
        {
            success?.Invoke(result);
        }
        else
        {
            failure?.Invoke(result);
        }
    }
}
