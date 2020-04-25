using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GavinCardGame.Systems
{
    public class GGraphics : GSystemBase
    {
        public GraphicsDeviceManager GDM { get { return MainGame.Game.Graphics; } }

        public int Width { get { return GDM.PreferredBackBufferWidth; } }
        public int Height { get { return GDM.PreferredBackBufferHeight; } }

        public Texture2D RectTex { get; private set; }

        public GGraphics()
        {
            RectTex = GSystems.GContent.Load<Texture2D>("Textures/rect");
        }

        public void DrawLine(Point start, int length, int thickness, bool horizontal, Color c, float depth, SpriteBatch sb)
        {
            int _halfThickness = thickness / 2;

            if (horizontal)
            {
                sb.Draw(
                    RectTex,
                    new Rectangle(
                        start.X,
                        start.Y - _halfThickness,
                        length,
                        thickness
                    ),
                    null,
                    c,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    depth
                );
            }
            else
            {
                sb.Draw(
                    RectTex,
                    new Rectangle(
                        start.X - _halfThickness,
                        start.Y,
                        thickness,
                        length
                    ),
                    null,
                    c,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    depth
                );
            }
        }

        public void FillRectangle(Rectangle r, Color c, float depth, SpriteBatch sb)
        {
            sb.Draw(RectTex, r, null, c, 0, Vector2.Zero, SpriteEffects.None, depth);
        }
        public void DrawRectangle(Rectangle r, Color c, int thickness, float depth, SpriteBatch sb)
        {
            int _halfT = thickness / 2;

            // Top left to top right
            sb.Draw(
                RectTex,
                new Rectangle(
                    r.X - _halfT,
                    r.Y - _halfT,
                    r.Width + thickness,
                    thickness
                ),
                null,
                c,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                depth
            );

            // Top right to bottom right
            sb.Draw(
                RectTex,
                new Rectangle(
                    r.X + r.Width - _halfT,
                    r.Y - _halfT,
                    thickness,
                    r.Height + thickness
                ),
                null,
                c,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                depth
            );

            // Bottom left to bottom right
            sb.Draw(
                RectTex,
                new Rectangle(
                    r.X - _halfT,
                    r.Y + r.Height - _halfT,
                    r.Width + thickness,
                    thickness
                ),
                null,
                c,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                depth
            );

            // Top left to bottom left
            sb.Draw(
                RectTex,
                new Rectangle(
                    r.X - _halfT,
                    r.Y - _halfT,
                    thickness,
                    r.Height + thickness
                ),
                null,
                c,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                depth
            );
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
        }
    }
}
