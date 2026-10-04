using Microsoft.JSInterop;

namespace FoodHub.Services.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static async Task ToastrSuccess(this IJSRuntime js, string message)
        { 
            await js.InvokeVoidAsync("ShowToaster", "success", message);
        }

        public static async Task ToastrError(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("ShowToaster", "error", message);
        }
    }
}
