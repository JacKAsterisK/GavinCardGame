using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GavinCardGame.Systems
{
    public class GContent : GSystemBase
    {
        public SpriteFont MenuFont { get; private set; }

        public GContent()
        {
            MenuFont = Load<SpriteFont>("Fonts/menu");
        }

        public T Load<T>(string assetName)
        {
            return MainGame.Game.Content.Load<T>(assetName);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
        }
    }
}
