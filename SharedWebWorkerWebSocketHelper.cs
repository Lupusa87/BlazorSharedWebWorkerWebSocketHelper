using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace BlazorSharedWebWorkerWebSocketHelper
{
    public class SharedWebWorkerWebSocketHelper:IDisposable
    {
       

        public BSwwWsState bSwwWsState = BSwwWsState.Undefined;

        public string _SharedWebWorkerName { get; private set; }
        private string _url = string.Empty;

        public Action<short> OnStateChange { get; set; }
        public Action<byte[]> OnMessage { get; set; }
        public Action<string> OnError { get; set; }


        public SharedWebWorkerWebSocketHelper(string Par_URL, string Par_SharedWebWorkerName)
        {

            _initialize(Par_URL, Par_SharedWebWorkerName);
        }


        private void _initialize(string Par_URL, string Par_SharedWebWorkerName)
        {
            if (!string.IsNullOrEmpty(Par_URL))
            {
                StaticClass.SharedWebWorkerWebSocketHelper1 = this;
                _url = Par_URL;
                _SharedWebWorkerName = Par_SharedWebWorkerName;
                BSwwWsJsInterop.SwwCreate(_url, _SharedWebWorkerName, new DotNetObjectRef(this));
            }
            else
            {
              OnError?.Invoke("Url is not provided!");
            }
        }
      

        public void Send(byte[] Par_Message)
        {
            string result = string.Empty;

            if (Par_Message.Length > 0)
            {
                BSwwWsJsInterop.SwwSend(Par_Message);
            }
            else
            {
                OnError?.Invoke("Message is empty");
            }

        }

        
        [JSInvokable]
        public void InvokeOnError(string par_error)
        {
            InvokeStateChanged(3);
            OnError?.Invoke(par_error);
        }

        [JSInvokable]
        public void InvokeStateChanged(short par_state)
        {

            bSwwWsState = (BSwwWsState)par_state;
            OnStateChange?.Invoke(par_state);
        }


        public void Dispose()
        {
          
            InvokeStateChanged(2);

            BSwwWsJsInterop.SwwRemove();

            GC.SuppressFinalize(this);
        }

    }

    public enum BSwwWsState
    {
        Undefined,
        Open,
        Close,
        Error,
    }
}
