using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace MSWSChat {
    public class MyWSHandler : WebSocketHandler {

        public override void OnOpen() {
            clients.Add(this);
            this.Send("Welcom from " + this.WebSocketContext.UserHostAddress);
        }

        private static WebSocketCollection clients = new WebSocketCollection();

        public override void OnMessage(string message) {
            //SocketIO socket = new SocketIO();
            //System.Web.UI.DataBinder.Eval(socket, message);
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            //SocketIO socket = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SocketIO>(message);
            dynamic socket = json.Deserialize<object>(message);

            //string msgBack = string.Format("You have sent {0} at {1}", message, DateTime.Now.ToLongTimeString());
            //string msgBack = string.Format("You have sent {0} at {1}", socket.action, socket.data);

            Dictionary<string, object> dic = socket["data"];

            //string msgBack = string.Format("You have sent {0} at {1}", socket["action"], socket["data"]["username"]);
            string msgBack = string.Format("{0} - {1}", dic["username"], dic["message"]);

            dic.Clear();
            dic["action"] = "new message";
            dic["data"] = msgBack;

            clients.Broadcast(json.Serialize(dic));
        }        

        public override void OnClose() {
            base.OnClose();
        }

        public override void OnError() {
            base.OnError();
        }
    }
    public class SocketIO{
        /*
        public int action { get; set; }
        public int data { get; set; } //*/
       

        public string action { get; set; }
        public string data { get; set; }
    }
}