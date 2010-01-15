using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace MyXMPPLib.Transport
{
    public class XMPPSocket
    {
        private TcpClient clientSocket = null;
        private IPAddress[] ipAddrs = null;
        private string hostName = null;
        private int port = 5222;
        private bool connectionEstablished = false;

        public bool ConnectionEstablished
        {
            get { return this.connectionEstablished; }
        }

        private XMPPSocket(bool isSecure)
        {
            int port = isSecure ? 5223 : 5222;
            clientSocket = new TcpClient();
        }

        public XMPPSocket(string host , bool isSecure) : this(isSecure)
        {
            this.hostName = host;
            this.ipAddrs = Dns.GetHostAddresses(host);        
        }

        public XMPPSocket(string host,bool allowAsync, bool isSecure): this(isSecure)
        {
            this.hostName = host;
            if (allowAsync)
            {
                this.OpenAsyncSocketConnection();
            }
            else
            {
                this.connectionEstablished = this.OpenSocketConnection();
            }
        }

        private void OpenAsyncSocketConnection()
        {
            try
            {
                this.clientSocket.BeginConnect(this.ipAddrs, this.port, new AsyncCallback(AsyncSocketConnResult), null);
            }
            catch (Exception e)
            {
                throw new Exceptions.UnableToEstablishConnectionException(e);
            }
        }

        private void AsyncSocketConnResult(IAsyncResult result)
        {
            this.connectionEstablished = result.IsCompleted;
        }

        private bool OpenSocketConnection()
        {
            try
            {
                foreach (IPAddress ipAddress in this.ipAddrs)
                {
                    IPEndPoint endPoint = new IPEndPoint(ipAddress, this.port);
                    clientSocket.Connect(endPoint);
                    if (clientSocket.Connected)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exceptions.UnableToEstablishConnectionException(e);
            }
            
        }

        public void SendPacket(string packet)
        {
            NetworkStream nwStream = this.clientSocket.GetStream();

        }

        public string ReceivePacket()
        {
            int buffLen = (int)nwStream.Length;
            byte[] buffer = new byte[buffLen];
            nwStream.Read(buffer, 0, buffLen);
            if (buffer.Length > 0)
            {
                return System.Text.Encoding.UTF8.GetString(buffer);
            }
            else
            {
                return string.Empty;
            }
            
        }
    }
}
