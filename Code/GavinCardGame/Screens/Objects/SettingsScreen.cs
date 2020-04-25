using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GavinCardGame.GameObjects;
using GavinCardGame.GameObjects.SceneObjects;
using GavinCardGame.Menus;
using GavinCardGame.Menus.Objects;
using Microsoft.Xna.Framework;

using static GavinCardGame.GSystems;

namespace GavinCardGame.Screens.Objects
{
    public class SettingsScreen : ScreenBase
    {
        static Regex _UnReg = new Regex(@"[a-zA-Z0-9 ]+");

        MText _UserNameText, _PortText, _DefaultHostText;

        MButton _DoneButton;

        public SettingsScreen() : base("SettingsScreen")
        {
            _UserNameText = GetMenuItem<MText>("UserNameText");
            _PortText = GetMenuItem<MText>("PortText");
            _DefaultHostText = GetMenuItem<MText>("DefaultHostText");
            _DoneButton = GetMenuItem<MButton>("DoneButton");

            _DoneButton.OnClicked += _DoneButton_OnClicked;
        }

        private void _DoneButton_OnClicked(MenuBase mBase)
        {
            string _un = _UnReg.Match(_UserNameText.Text.Trim()).Value;
            if (!string.IsNullOrWhiteSpace(_un))
                GSettings.Name = _un;

            if (int.TryParse(_PortText.Text, out int _port) && _port < 65000 && _port > 1025)
                GSettings.Port = _port;

            GSettings.DefaultHost = _DefaultHostText.Text.Trim();

            GSettings.Save();

            GScreens.OpenScreen<MainScreen>();
        }

        public override void Opened()
        {
            base.Opened();

            _UserNameText.Text = GSettings.Name;
            _PortText.Text = GSettings.Port.ToString();
            _DefaultHostText.Text = GSettings.DefaultHost;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GInput.CursorVisible = true;
        }
    }
}
