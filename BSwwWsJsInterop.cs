using Microsoft.JSInterop;
using Mono.WebAssembly.Interop;
using System;
using System.Reflection;
using System.Threading.Tasks;


namespace BlazorSharedWebWorkerWebSocketHelper
{
    public class BSwwWsJsInterop
    {
        public static Task<string> Alert(string message)
        {
            return JSRuntime.Current.InvokeAsync<string>(
                "BSwwWsJsFunctions.alert",
                message);
        }

        public static Task<bool> SwwCreate(string SwwUrl, string SwwName, DotNetObjectRef dotnethelper)
        {
           return JSRuntime.Current.InvokeAsync<bool>("BSwwWsJsFunctions.SwwCreate", new { SwwUrl, SwwName, dotnethelper }); 
        }

        public static bool SwwSend(byte[] Message)
        {
         
                if (JSRuntime.Current is MonoWebAssemblyJSRuntime mono)
                {

                    return mono.InvokeUnmarshalled<byte[], bool>(
                        "BSwwWsJsFunctions.SwwSend",Message);
                }

            return false;

        }


        public static Task<bool> SwwRemove()
        {
            return JSRuntime.Current.InvokeAsync<bool>("BSwwWsJsFunctions.SwwRemove");
        }

    }
}
