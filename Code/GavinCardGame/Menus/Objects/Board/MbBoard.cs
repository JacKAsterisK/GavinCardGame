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
    public class MbBoard : MenuBase
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public MbBoard(MenuData data, MenuBase parent) : base(data, parent)
        {
            Rows = int.Parse(data.GetDataProperty("Rows").ToString());
            Cols = int.Parse(data.GetDataProperty("Cols").ToString());
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);

            float _width = Size.X / Cols;
            for (int _index = 1; _index < Cols; _index++)
            {
                GGraphics.DrawLine(
                    ActualPosition.ToPoint() + new Point((int)_width * _index, 0),
                    (int)Size.Y,
                    2,
                    false,
                    Color.White,
                    Depth - StringBorderDepthAdd,
                    sb
                );
            }

            float _height = Size.Y / Rows;
            for (int _index = 1; _index < Rows; _index++)
            {
                GGraphics.DrawLine(
                    ActualPosition.ToPoint() + new Point(0, (int)_height * _index),
                    (int)Size.X,
                    2,
                    true,
                    Color.White,
                    Depth - StringBorderDepthAdd,
                    sb
                );
            }
        }
    }
}
