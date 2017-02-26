using System;
using System.Collections.Generic;
using UnityEngine;

namespace Net
{
    public class NetManager
    {
        public const string MainUrl = "http://h105051.s22.test-hf.su/";

        public static List<NetConnection> Connections = new List<NetConnection>();


        public static NetConnection LoadText(string url, URLVariables urlParams = null, Action<string> onComplete = null, Action<object> onError = null)
        {
            if(urlParams != null)
                url = url + urlParams.getString;

            NetConnection connection = new NetConnection();
            connection.LoadText(url, onComplete, onError);
            Connections.Add(connection);

            return connection;
        }

        public static NetConnection LoadImage(string url, URLVariables urlParams = null, Action<Texture2D> onComplete = null, Action<object> onError = null)
        {
            if(urlParams != null)
                url = url + urlParams.getString;

            NetConnection connection = new NetConnection();
            connection.LoadImage(url, onComplete, onError);
            Connections.Add(connection);

            return connection;
        }
    }
}