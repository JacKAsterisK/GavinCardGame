using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GavinCardGame.GSystems;

namespace GavinCardGame.GameObjects.SceneObjects
{
    public class KfCard : Card
    {
        public KfCard(ObjBase parent, int? id) : base(parent, id)
        {
            Texture = GContent.Load<Texture2D>("Textures/card");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
