using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GavinCardGame.GSystems;

namespace GavinCardGame.Screens.Objects
{
    public class ServerConnectScreen : ScreenBase
    {
        public ServerConnectScreen() : base("ServerConnectScreen")
        {
            GNet.StartServer();

            GNet.OnNewConnection += GNet_OnNewConnection;
        }

        private void GNet_OnNewConnection()
        {
            GNet.OnNewConnection -= GNet_OnNewConnection;

            GScreens.OpenScreen<GameBoardScreen>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GInput.CursorVisible = true;
        }
    }
}
