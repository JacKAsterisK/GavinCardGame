using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GavinCardGame.GameObjects;
using GavinCardGame.GameObjects.SceneObjects;
using GavinCardGame.Menus;
using GavinCardGame.Menus.Objects;
using Microsoft.Xna.Framework;

using static GavinCardGame.GSystems;

namespace GavinCardGame.Screens.Objects
{
    public class MainScreen : ScreenBase
    {
        MListItem 
            _PlayServerButton
            ,_PlayClientButton
            ,_DeckBuilderButton
            ,_SettingsButton
        //,_GameBoardButton
        ;

        public MainScreen() : base("MainScreen")
        {
            _PlayServerButton = GetMenuItem<MListItem>("PlayServer");
            _PlayClientButton = GetMenuItem<MListItem>("PlayClient");
            _DeckBuilderButton = GetMenuItem<MListItem>("DeckBuilder");
            _SettingsButton = GetMenuItem<MListItem>("Settings");
            //_GameBoardButton = GetMenuItem<MListItem>("GameBoard");

            _PlayServerButton.OnClicked += _PlayServerButton_OnClicked;
            _PlayClientButton.OnClicked += _PlayClientButton_OnClicked;
            _DeckBuilderButton.OnClicked += _DeckBuilderButton_OnClicked;
            _SettingsButton.OnClicked += _SettingsButton_OnClicked;
            //_GameBoardButton.OnClicked += _GameBoardButton_OnClicked;
        }

        public override void Opened()
        {
            base.Opened();

            if (!string.IsNullOrWhiteSpace(Program.PlayAs))
            {
                if (Program.PlayAs.ToLower() == "server")
                    GScreens.OpenScreen<ServerConnectScreen>();
                else if (Program.PlayAs.ToLower() == "client")
                {
                    var _screen = GScreens.OpenScreen<ClientConnectScreen>();
                    _screen.Connect();
                }
            }
        }

        private void _PlayServerButton_OnClicked(MenuBase mBase)
        {
            GScreens.OpenScreen<ServerConnectScreen>();
        }
        private void _PlayClientButton_OnClicked(MenuBase mBase)
        {
            GScreens.OpenScreen<ClientConnectScreen>();
        }
        private void _DeckBuilderButton_OnClicked(MenuBase mBase)
        {
            GScreens.OpenScreen<DeckBuilderScreen>();
        }
        private void _SettingsButton_OnClicked(MenuBase mBase)
        {
            GScreens.OpenScreen<SettingsScreen>();
        }
        //private void _GameBoardButton_OnClicked(MenuBase mBase)
        //{
        //    GScreens.OpenScreen<GameBoardScreen>();
        //}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GInput.CursorVisible = true;
        }
    }
}
