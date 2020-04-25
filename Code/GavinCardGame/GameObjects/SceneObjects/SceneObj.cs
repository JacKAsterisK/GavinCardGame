using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GavinCardGame.GameObjects.SceneObjects
{
    public abstract class SceneObj : ObjBase
    {
        public Vector2 Position { get; set; }
        public float Scale { get; set; }
        public Texture2D Texture { get; protected set; }
        public Color Color { get; set; }

        public bool DrawAtOrigin { get; set; }

        public float Width { get { return (Texture?.Width ?? 0) * Scale; } }
        public float Height { get { return (Texture?.Height ?? 0) * Scale; } }

        public SceneObj(ObjBase parent, int? id) : base(parent, id)
        {
            Position = Vector2.Zero;
            Scale = 1.0f;
            DrawAtOrigin = true;
            Color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);

            Vector2 _drawPos = Position;
            if (DrawAtOrigin && Texture != null)
                _drawPos -= new Vector2(Texture.Width, Texture.Height) / 2 * Scale;

            if (Texture != null)
                sb.Draw(
                    Texture, 
                    new Rectangle((int)_drawPos.X, (int)_drawPos.Y, (int)Width, (int)Height),
                    null,
                    Color,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    0.5f
                );
        }
    }
}
