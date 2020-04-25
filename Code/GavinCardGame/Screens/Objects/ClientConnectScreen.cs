using GavinCardGame.Menus.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GavinCardGame.GSystems;

namespace GavinCardGame.Screens.Objects
{
    public class ClientConnectScreen : ScreenBase
    {
        MText _IpTextBox;
        MButton _ConnectButton;

        public ClientConnectScreen() : base("ClientConnectScreen")
        {
            _IpTextBox = GetMenuItem<MText>("IpTextBox");
            _ConnectButton = GetMenuItem<MButton>("ConnectButton");

            _IpTextBox.Filters = Systems.AllowKeyString.All | ~Systems.AllowKeyString.Space;
            _IpTextBox.Text = GSettings.DefaultHost;

            _IpTextBox.OnEnterPressed += _IpTextBox_OnEnterPressed;
            _ConnectButton.OnClicked += _ConnectButton_OnClicked;

            GNet.OnGotMessage += GNet_OnGotMessage;
        }

        private void _IpTextBox_OnEnterPressed(MText textBox)
        {
            Connect();
        }
        private void _ConnectButton_OnClicked(Menus.MenuBase mBase)
        {
            Connect();
        }
        public void Connect()
        {
            if (GNet.StartClient(_IpTextBox.Text))
                _ConnectButton.BorderColor = Color.Aqua;
            else
                _ConnectButton.BorderColor = Color.Red;
        }

        private void GNet_OnGotMessage(Systems.IncomingMessage message)
        {
            if (message.Type == Systems.MessageType.Hail)
            {
                GNet.OpponentName = message.Message.ReadString();

                GNet.OnGotMessage -= GNet_OnGotMessage;

                GScreens.OpenScreen<GameBoardScreen>();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GInput.CursorVisible = true;
        }
    }
}
