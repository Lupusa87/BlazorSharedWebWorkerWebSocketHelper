using Microsoft.JSInterop;
using Mono.WebAssembly.Interop;
using System.Threading.Tasks;


namespace BlazorSharedWebWorkerWebSocketHelper
{
    public class BSwwWsJsInterop
    {

        private IJSRuntime _JSRuntime;
        public BSwwWsJsInterop(IJSRuntime jsRuntime) => _JSRuntime = jsRuntime;
        public static MonoWebAssemblyJSRuntime monoWebAssemblyJSRuntime = new MonoWebAssemblyJSRuntime();

        public ValueTask<string> Alert(string message)
        {
            return _JSRuntime.InvokeAsync<string>(
                "BSwwWsJsFunctions.alert",
                message);
        }

        public ValueTask<bool> SwwCreate(string SwwUrl, string SwwName, DotNetObjectReference<SharedWebWorkerWebSocketHelper> dotnethelper)
        {
           return _JSRuntime.InvokeAsync<bool>("BSwwWsJsFunctions.SwwCreate", new { SwwUrl, SwwName, dotnethelper }); 
        }

        public bool SwwSend(byte[] Message)
        {
            return monoWebAssemblyJSRuntime.InvokeUnmarshalled<byte[], bool>(
                        "BSwwWsJsFunctions.SwwSend",Message);
        }


        public ValueTask<bool> SwwRemove()
        {
            return _JSRuntime.InvokeAsync<bool>("BSwwWsJsFunctions.SwwRemove");
        }

    }
}
