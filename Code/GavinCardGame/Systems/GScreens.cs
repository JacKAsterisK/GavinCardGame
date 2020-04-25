using GavinCardGame.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GavinCardGame.Systems
{
    public class GScreens : GSystemBase
    {
        public List<ScreenBase> Screens { get; private set; }
        public ScreenBase ActiveScreen { get; private set; }

        public GScreens()
        {
            Screens = new List<ScreenBase>();
        }

        public T OpenScreen<T>() where T : ScreenBase
        {
            foreach (var _screen in Screens)
            {
                if (_screen.GetType() == typeof(T))
                {
                    ActiveScreen = _screen;
                    _screen.Opened();
                    return (T)_screen;
                }
            }

            var _outScreen = (T)Activator.CreateInstance(typeof(T));
            Screens.Add(_outScreen);
            ActiveScreen = _outScreen;
            _outScreen.Opened();

            return _outScreen;
        }

        public override void Update(GameTime gameTime)
        {
            if (ActiveScreen != null)
                ActiveScreen.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (ActiveScreen != null)
                ActiveScreen.Draw(gameTime, sb);
        }
    }
}
