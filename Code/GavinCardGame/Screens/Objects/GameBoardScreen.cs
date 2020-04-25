using GavinCardGame.GameObjects;
using GavinCardGame.Menus.Objects;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GavinCardGame.GSystems;

namespace GavinCardGame.Screens.Objects
{
    public class GameBoardScreen : ScreenBase
    {
        bool _Started = false;

        MLabel MyNameLabel, OpponentNameLabel;

        Player Player1, Player2;

        public GameBoardScreen() : base("GameBoardScreen")
        {
            GNet.OnGotMessage += GNet_OnGotMessage;

            // Hail the opponent
            GNet.SendMessage(Systems.MessageType.Hail, GSettings.Name);

            MyNameLabel = GetMenuItem<MLabel>("MyName");
            OpponentNameLabel = GetMenuItem<MLabel>("OpponentName");
        }

        private void GNet_OnGotMessage(Systems.IncomingMessage message)
        {
            switch (message.Type)
            {
                case Systems.MessageType.Hail:
                    GNet.OpponentName = message.Message.ReadString();

                    if (GNet.IsServer)
                        StartServerGame();

                    break;
                case Systems.MessageType.CreateObject:
                    {
                        var _objType = (Systems.ObjectType)message.Message.ReadByte();

                        switch (_objType)
                        {
                            case Systems.ObjectType.Player:
                                var _createSpec = Player.CreateSpec.Deserialize(message.Message);

                                Player _player;

                                if (_createSpec.IsServer)
                                    _player = Player1 = GScene.Create<Player>(null, _createSpec.Id);
                                else
                                    _player = Player2 = GScene.Create<Player>(null, _createSpec.Id);

                                _createSpec.Apply(_player);

                                break;
                        }
                    }

                    break;
                case Systems.MessageType.UpdateObject:
                    {
                        var _objType = (Systems.ObjectType)message.Message.ReadByte();

                        switch (_objType)
                        {
                            case Systems.ObjectType.Player:
                                var _updateSpec = Player.UpdateSpec.Deserialize(message.Message);

                                Player _player = GScene.GetObject<Player>(_updateSpec.Id);

                                _updateSpec.Apply(_player);

                                break;
                        }
                    }

                    break;
                case Systems.MessageType.StartGame:
                    _Started = true;
                    break;
            }
        }

        void StartServerGame()
        {
            NetOutgoingMessage _message;

            Player1 = GScene.Create<Player>();
            Player1.IsServer = true;
            _message = GNet.CreateMessage();
            _message.Write((byte)Systems.ObjectType.Player);
            Player.CreateSpec.Serialize(Player1, _message);
            GNet.SendMessage(Systems.MessageType.CreateObject, _message);

            Player2 = GScene.Create<Player>();
            Player2.IsServer = false;
            _message = GNet.CreateMessage();
            _message.Write((byte)Systems.ObjectType.Player);
            Player.CreateSpec.Serialize(Player2, _message);
            GNet.SendMessage(Systems.MessageType.CreateObject, _message);

            _Started = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GInput.CursorVisible = true;

            MyNameLabel.Text = GSettings.Name;
            OpponentNameLabel.Text = GNet.OpponentName ?? "";
        }
    }
}
