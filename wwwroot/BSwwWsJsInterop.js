const SwwState = {

    Undefined: 0,
    Open: 1,
    Close: 2,
    Error: 3,

};

var SharedWebWorker1;

var tmpValue1;


function SwwOnError(e, dotnethelper) {
    dotnethelper.invokeMethodAsync('InvokeOnError', e.message);
}


function SwwOnMessage(e) {

    tmpValue1 = e.data;

    Module.mono_call_static_method('[BlazorSharedWebWorkerWebSocketHelper] BlazorSharedWebWorkerWebSocketHelper.StaticClass:AllocateArray',
        [tmpValue1.byteLength]);
}



window.BSwwWsJsFunctions = {
    alert: function (message) {
        return alert(message);
    },
    SwwCreate: function (obj) {


        SharedWebWorker1 = new SharedWorker(obj.swwUrl, obj.swwName);
       

        obj.dotnethelper.invokeMethodAsync('InvokeStateChanged', SwwState.Open);


        SharedWebWorker1.port.onmessage = function (e) { SwwOnMessage(e); };

        SharedWebWorker1.addEventListener('error', function (e) { SwwOnError(e, obj.dotnethelper); }, false);

        return true;
    },
    SwwSend: function (data) {
         
        //it is cloning arraybuffer, direct without cloning was giving error!
        var buffer = new Uint8Array(Array.from(Blazor.platform.toUint8Array(data))).buffer;

        SharedWebWorker1.port.postMessage(buffer, [buffer]);

        return true;

    },
    SwwRemove: function () {

        SharedWebWorker1 = null;

        return true;
    },
    GetBinaryData: function (d) {

        var destinationUint8Array = Blazor.platform.toUint8Array(d);
        destinationUint8Array.set(new Uint8Array(tmpValue1));

        tmpValue1 = null;
        return true;
    },
};
