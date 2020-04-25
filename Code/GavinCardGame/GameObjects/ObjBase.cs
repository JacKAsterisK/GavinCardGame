using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GavinCardGame.GameObjects
{
    public class ObjBase
    {
        private static int CURRENT_ID = 0;
        public int Id { get; private set; }

        public ObjBase Parent;
        public List<ObjBase> Children { get; private set; }

        public ObjBase(ObjBase parent, int? id)
        {
            if (id == null)
            {
                Id = CURRENT_ID;
                CURRENT_ID++;
            }
            else
                Id = id.Value;

            Parent = parent;
            Children = new List<ObjBase>();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var _child in Children)
                _child.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (var _child in Children)
                _child.Draw(gameTime, sb);
        }
    }
}
