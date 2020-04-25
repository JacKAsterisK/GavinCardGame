using GavinCardGame.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GavinCardGame.Systems
{
    public class GMenus : GSystemBase
    {
        static Regex _TypeReg = new Regex(@"[^\.]+$");

        public GMenus()
        {
        }

        public MenuBase Create(MenuData data, MenuBase parent)
        {
            var _mTypes = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => {
                    return t != typeof(MenuBase) && typeof(MenuBase).IsAssignableFrom(t);
                }
            );

            foreach (var _mType in _mTypes)
            {
                if (_TypeReg.Match(_mType.ToString()).Value == "M" + data.Type.ToString())
                    return (MenuBase)Activator.CreateInstance(_mType, data, parent);
            }

            return null;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
        }
    }
}
