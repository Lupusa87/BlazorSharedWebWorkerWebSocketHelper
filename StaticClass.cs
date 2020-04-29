using Microsoft.JSInterop;
using Mono.WebAssembly.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSharedWebWorkerWebSocketHelper
{
    public static class StaticClass
    {
        public static SharedWebWorkerWebSocketHelper SharedWebWorkerWebSocketHelper1;

        public static MonoWebAssemblyJSRuntime monoWebAssemblyJSRuntime = new MonoWebAssemblyJSRuntime();

        [JSInvokable]
        public static void AllocateArray(int length)
        {

            byte[] b = new byte[length];


            monoWebAssemblyJSRuntime.InvokeUnmarshalled<byte[], bool>("BSwwWsJsFunctions.GetBinaryData", b);

            HandleMessageBinary(b);

        }


        [JSInvokable]
        public static void HandleMessageBinary(byte[] data)
        {
            SharedWebWorkerWebSocketHelper1.OnMessage?.Invoke(data);    
        }
    }
    
}
