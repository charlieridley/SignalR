using System;
using System.IO;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace Microsoft.AspNet.SignalR.Client.Sample
{
    public class CommonClient
    {
        private TextWriter _traceWriter;

        public CommonClient(TextWriter traceWriter)
        {
            _traceWriter = traceWriter;
        }

        public async void RunAsync()
        {
            string url = "http://signalr01.cloudapp.net:81/";
            var hubConnection = new HubConnection(url);
            hubConnection.TraceWriter = _traceWriter;
            hubConnection.TraceLevel = TraceLevels.All;
            hubConnection.Closed += () => hubConnection.TraceWriter.WriteLine("[Closed]");
            hubConnection.ConnectionSlow += () => hubConnection.TraceWriter.WriteLine("[ConnectionSlow]");
            hubConnection.Error += (error) => hubConnection.TraceWriter.WriteLine(string.Format("[Error] {0}", error.ToString()));
            hubConnection.Received += (payload) => hubConnection.TraceWriter.WriteLine(string.Format("[Received] {0}", payload));
            hubConnection.Reconnected += () => hubConnection.TraceWriter.WriteLine("[Reconnected]");
            hubConnection.Reconnecting += () => hubConnection.TraceWriter.WriteLine("[Reconnecting]");
            hubConnection.StateChanged += (change) => hubConnection.TraceWriter.WriteLine(string.Format("[StateChanged] {0} {1}", change.OldState, change.NewState));

            var hubProxy = hubConnection.CreateHubProxy("TestHub");
            hubProxy.On<string>("received", (data) => hubConnection.TraceWriter.WriteLine(data)); 

            await hubConnection.Start();
            await hubProxy.Invoke("SendToCaller", "Hello!");
        }
    }
}

