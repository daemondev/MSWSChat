using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace MSWSChat
{
    /// <summary>
    /// Summary description for MSWSChatHandler
    /// </summary>
    public class MSWSChatHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest || context.IsWebSocketRequestUpgrading)
            {
                context.AcceptWebSocketRequest(new MyWSHandler());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}