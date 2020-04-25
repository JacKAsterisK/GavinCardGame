using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GavinCardGame.Systems
{
    // Do not exceed 256 values
    public enum MessageType : byte
    {
        Hail,
        CreateObject,
        UpdateObject,
        StartGame,
    }

    // Do not exceed 256 values
    public enum ObjectType : byte
    {
        Player,
        MercCard,
        KfCard,
    }

    public class IncomingMessage
    {
        public MessageType Type { get; private set; }
        public NetIncomingMessage Message { get; private set; }

        public IncomingMessage(MessageType type, NetIncomingMessage message)
        {
            Type = type;
            Message = message;
        }
    }

    public class GNet : GSystemBase
    {
        public bool IsServer { get; private set; }
        public int Port { get; private set; }
        public string Host { get; private set; }
        public string Password { get; private set; }
        public bool Started { get; private set; }

        public string OpponentName { get; set; }

        NetPeerConfiguration _Config;

        NetServer _Server;
        NetClient _Client;

        public delegate void NewConnection();
        public event NewConnection OnNewConnection;

        public delegate void LostConnection();
        public event LostConnection OnLostConnection;

        public delegate void GotMessage(IncomingMessage message);
        public event GotMessage OnGotMessage;

        public NetConnection ClientNetConnection { get; private set; }
        public bool IsConnected
        {
            get
            {
                if (IsServer)
                {
                    if (!Started || _Server == null || _Server.ConnectionsCount <= 0)
                        return false;

                    foreach (var _connection in _Server.Connections)
                    {
                        if (_connection.Status == NetConnectionStatus.Connected)
                            return true;
                    }

                    return false;
                }
                else
                {
                    if (!Started || _Client == null)
                        return false;

                    return
                        ClientNetConnection != null
                        && ClientNetConnection.Status == NetConnectionStatus.Connected
                    ;
                }
            }
        }
        public bool WasConnected { get; set; }

        public GNet()
        {
            OpponentName = null;
            Port = GSystems.GSettings.Port;
        }

        public void StartServer()
        {
            IsServer = true;

            _Config = new NetPeerConfiguration("GavinCardGame")
            {
                Port = Port,
                AutoFlushSendQueue = false
            };

            _Server = new NetServer(_Config);
            _Server.Start();

            Started = true;
        }

        public bool StartClient(string host)
        {
            IsServer = false;

            try
            {
                _Config = new NetPeerConfiguration("GavinCardGame")
                {
                    AutoFlushSendQueue = false
                };

                _Client = new NetClient(_Config);
                _Client.Start();

                ClientNetConnection = _Client.Connect(host, Port);

                Started = true;

                return true;
            }
            catch
            {
                _Client?.Shutdown("bye");
                _Client = null;

                return false;
            }
        }

        public NetOutgoingMessage CreateMessage()
        {
            if (IsServer)
                return _Server.CreateMessage();
            else
                return _Client.CreateMessage();
        }

        public void SendMessage(MessageType mType, string message, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered)
        {
            var _msg = CreateMessage();
            _msg.Write(message);
            SendMessage(mType, _msg, method);
        }
        public void SendMessage(MessageType mType, NetBuffer message, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered)
        {
            if (IsConnected)
            {
                if (IsServer)
                {
                    NetOutgoingMessage _sendMsg = _Server.CreateMessage();
                    _sendMsg.Write((byte)mType);
                    _sendMsg.Write(message);

                    foreach (var _connection in _Server.Connections)
                        _Server.SendMessage(_sendMsg, _connection, method);

                    _Server.FlushSendQueue();
                }
                else
                {
                    NetOutgoingMessage _sendMsg = _Client.CreateMessage();
                    _sendMsg.Write((byte)mType);
                    _sendMsg.Write(message);

                    _Client.SendMessage(_sendMsg, ClientNetConnection, method);

                    _Client.FlushSendQueue();
                }
            }
        }

        public IncomingMessage ReadMessage(NetIncomingMessage message)
        {
            MessageType _mType = (MessageType)message.ReadByte();

            return new IncomingMessage(_mType, message);
        }

        public override void Update(GameTime gameTime)
        {
            WasConnected = IsConnected;

            if (Started)
            {
                if (IsServer)
                {
                    NetIncomingMessage _msg;
                    while ((_msg = _Server.ReadMessage()) != null)
                    {
                        switch (_msg.MessageType)
                        {
                            case NetIncomingMessageType.Data:
                                var _incMsg = ReadMessage(_msg);
                                OnGotMessage?.Invoke(_incMsg);
                                break;

                            case NetIncomingMessageType.VerboseDebugMessage:
                            case NetIncomingMessageType.DebugMessage:
                            case NetIncomingMessageType.WarningMessage:
                            case NetIncomingMessageType.ErrorMessage:
                                Console.WriteLine(_msg.ReadString());
                                break;

                            default:
                                Console.WriteLine("Unhandled type: " + _msg.MessageType);
                                break;
                        }
                        _Server.Recycle(_msg);
                    }
                }
                else
                {
                    NetIncomingMessage _msg;
                    while ((_msg = _Client.ReadMessage()) != null)
                    {
                        switch (_msg.MessageType)
                        {
                            case NetIncomingMessageType.Data:
                                var _incMsg = ReadMessage(_msg);
                                OnGotMessage?.Invoke(_incMsg);
                                break;

                            case NetIncomingMessageType.VerboseDebugMessage:
                            case NetIncomingMessageType.DebugMessage:
                            case NetIncomingMessageType.WarningMessage:
                            case NetIncomingMessageType.ErrorMessage:
                                Console.WriteLine(_msg.ReadString());
                                break;

                            default:
                                Console.WriteLine("Unhandled type: " + _msg.MessageType);
                                break;
                        }
                        _Client.Recycle(_msg);
                    }
                }
            }

            if (IsConnected && !WasConnected)
                OnNewConnection?.Invoke();

            if (!IsConnected && WasConnected)
                OnLostConnection?.Invoke();
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
        }
    }
}
