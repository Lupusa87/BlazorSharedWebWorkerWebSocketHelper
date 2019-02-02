using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSharedWebWorkerWebSocketHelper
{
    public static class StaticClass
    {
        public static SharedWebWorkerWebSocketHelper SharedWebWorkerWebSocketHelper1;

        [JSInvokable]
        public static byte[] AllocateArray(string length)
        {
            return new byte[int.Parse(length)];
        }


        [JSInvokable]
        public static void HandleMessageBinary(byte[] data)
        {

            SharedWebWorkerWebSocketHelper1.OnMessage?.Invoke(data);
               
        }
    }
    
}
