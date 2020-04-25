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
    public class MercDeckBuilderScreen : ScreenBase
    {
        MButton _DoneButton;

        public MercDeckBuilderScreen() : base("DeckBuilder/MercDeckBuilderScreen")
        {
            _DoneButton = GetMenuItem<MButton>("DoneButton");

            _DoneButton.OnClicked += _DoneButton_OnClicked;
        }

        private void _DoneButton_OnClicked(Menus.MenuBase mBase)
        {
            GScreens.OpenScreen<MainScreen>();
        }
    }
}
