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
    public class DeckBuilderScreen : ScreenBase
    {
        MListItem
            _MercDeckBuilderButton
            ,_KfDeckBuilderButton
        ;

        MButton _DoneButton;

        public DeckBuilderScreen() : base("DeckBuilder/DeckBuilderScreen")
        {
            _MercDeckBuilderButton = GetMenuItem<MListItem>("MercDeckBuilder");
            _KfDeckBuilderButton = GetMenuItem<MListItem>("KfDeckBuilder");
            _DoneButton = GetMenuItem<MButton>("DoneButton");

            _MercDeckBuilderButton.OnClicked += _MercDeckBuilderButton_OnClicked;
            _KfDeckBuilderButton.OnClicked += _KfDeckBuilderButton_OnClicked;
            _DoneButton.OnClicked += _DoneButton_OnClicked;
        }

        private void _MercDeckBuilderButton_OnClicked(Menus.MenuBase mBase)
        {
            GScreens.OpenScreen<MercDeckBuilderScreen>();
        }
        private void _KfDeckBuilderButton_OnClicked(Menus.MenuBase mBase)
        {
            GScreens.OpenScreen<KfDeckBuilderScreen>();
        }
        private void _DoneButton_OnClicked(Menus.MenuBase mBase)
        {
            GScreens.OpenScreen<MainScreen>();
        }
    }
}
