using GavinCardGame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GavinCardGame
{
    public static class GSystems
    {
        public static GSettings GSettings { get; private set; }
        public static GContent GContent { get; private set; }
        public static GInput GInput { get; private set; }
        public static GNet GNet { get; private set; }
        public static GGraphics GGraphics { get; private set; }
        public static GScreens GScreens { get; private set; }
        public static GScene GScene { get; private set; }
        public static GMenus GMenus { get; private set; }

        private static GSystemBase[] _SystemList;

        public static void GLoad()
        {
            _SystemList = new GSystemBase[]
            {
                GSettings = new GSettings(),
                GContent = new GContent(),
                GInput = new GInput(),
                GNet = new GNet(),
                GGraphics = new GGraphics(),
                GScreens = new GScreens(),
                GScene = new GScene(),
                GMenus = new GMenus(),
            };
        }

        public static void GUpdate(GameTime gameTime)
        {
            foreach (var _s in _SystemList)
                _s.Update(gameTime);
        }

        public static void GDraw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (var _s in _SystemList)
                _s.Draw(gameTime, sb);
        }
    }
}
