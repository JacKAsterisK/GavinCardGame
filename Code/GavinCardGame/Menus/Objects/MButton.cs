﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static GavinCardGame.GSystems;

namespace GavinCardGame.Menus.Objects
{
    public class MButton : MenuBase
    {
        public MButton(MenuData data, MenuBase parent) : base(data, parent)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                Vector2 _pos = ActualPosition;

                Vector2 _tSize = GContent.MenuFont.MeasureString(Text);
                Vector2 _tPos = _pos;

                _tPos.X += Size.X / 2 - _tSize.X / 2;
                _tPos.Y += Size.Y / 2 - _tSize.Y / 2;

                sb.DrawString(GContent.MenuFont, Text, _tPos, Color.White);
            }
        }
    }
}
