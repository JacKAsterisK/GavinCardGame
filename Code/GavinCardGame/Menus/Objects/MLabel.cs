using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static GavinCardGame.GSystems;

namespace GavinCardGame.Menus.Objects
{
    public class MLabel : MenuBase
    {
        public MLabel(MenuData data, MenuBase parent) : base(data, parent)
        {
            ShowText = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }
    }
}
