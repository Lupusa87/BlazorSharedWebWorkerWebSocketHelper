const SwwState = {

    Undefined: 0,
    Open: 1,
    Close: 2,
    Error: 3,

};

var SharedWebWorker1;

function SwwOnError(e, dotnethelper) {
    dotnethelper.invokeMethodAsync('InvokeOnError', e.message);
}


function SwwOnMessage(e) {

        var allocateArrayMethod = Blazor.platform.findMethod(
            'BlazorSharedWebWorkerWebSocketHelper',
            'BlazorSharedWebWorkerWebSocketHelper',
            'StaticClass',
            'AllocateArray'
        );

        var dotNetArray = Blazor.platform.callMethod(allocateArrayMethod,
            null,
            [Blazor.platform.toDotNetString(e.data.byteLength.toString())]);

        var arr = Blazor.platform.toUint8Array(dotNetArray);


        arr.set(new Uint8Array(e.data));

        var receiveResponseMethod = Blazor.platform.findMethod(
            'BlazorSharedWebWorkerWebSocketHelper',
            'BlazorSharedWebWorkerWebSocketHelper',
            'StaticClass',
            'HandleMessageBinary'
        );

        Blazor.platform.callMethod(receiveResponseMethod,
            null,
            [dotNetArray]);
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
};
