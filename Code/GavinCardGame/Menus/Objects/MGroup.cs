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
    public class MGroup : MenuBase
    {
        public MGroup(MenuData data, MenuBase parent) : base(data, parent)
        {
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
