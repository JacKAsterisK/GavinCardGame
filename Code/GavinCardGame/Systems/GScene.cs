using GavinCardGame.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GavinCardGame.Systems
{
    public class GScene : GSystemBase
    {
        public ObjBase Root { get; private set; }

        public GScene()
        {
            Root = new ObjBase(null, null);
        }

        public T Create<T>(ObjBase parent = null, int? id = null) where T : ObjBase
        {
            var _obj = (T)Activator.CreateInstance(typeof(T), parent ?? Root, id);

            if (parent == null)
                Root.Children.Add(_obj);
            else
                parent.Children.Add(_obj);

            return _obj;
        }

        public T GetObject<T>(int id, ObjBase searchObj = null) where T : ObjBase
        {
            var _searchObj = searchObj ?? Root;

            foreach (var _obj in _searchObj.Children)
            {
                if (_obj.Id == id)
                    return (T)_obj;

                var _retObj = GetObject<T>(id, _obj);
                if (_retObj != null)
                    return (T)_retObj;
            }

            return null;
        }

        public override void Update(GameTime gameTime)
        {
            Root.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            Root.Draw(gameTime, sb);
        }
    }
}
