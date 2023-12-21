using Microsoft.JSInterop;

namespace QHSE.Client.Utilidades
{
    public static class Devices
    {
        public static async Task GenerarArchivo(this IJSRuntime js, string nombre, byte[] arrayBytes)
        {
            await js.InvokeAsync<object>("DescargarArchivo", nombre, Convert.ToBase64String(arrayBytes));
        }

        public static async Task EsCelular(this IJSRuntime js)
        {
            await js.InvokeAsync<bool>("isDevice");
        }

    }
}
