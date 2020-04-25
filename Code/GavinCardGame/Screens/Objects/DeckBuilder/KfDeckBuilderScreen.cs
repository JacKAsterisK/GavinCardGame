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
    public class KfDeckBuilderScreen : ScreenBase
    {
        MButton _DoneButton;

        public KfDeckBuilderScreen() : base("DeckBuilder/KfDeckBuilderScreen")
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
