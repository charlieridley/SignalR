using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microsoft.AspNet.SignalR.Samples.Hubs.Test
{
    public class SimpleEchoHub : Hub
    {
        public void AsyncEcho(string str)
        {
            Clients.Caller.AsyncEcho(str);
        }
    }
}