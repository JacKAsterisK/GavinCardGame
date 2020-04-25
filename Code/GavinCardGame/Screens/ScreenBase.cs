using GavinCardGame.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GavinCardGame.GSystems;

namespace GavinCardGame.Screens
{
    public class ScreenBase
    {
        private ScreenData Data;

        public Texture2D BackgroundTex { get; private set; }
        public List<MenuBase> Menus { get; private set; }

        public ScreenBase(string name)
        {
            string _dataString = File.ReadAllText($@"Screens\Data\{name}.json");

            try
            {
                Data = new ScreenData(JObject.Parse(_dataString));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error parsing JSON:\r\n\r\n" + ex.Message);

                throw ex;
            }

            try
            {
                BackgroundTex = GContent.Load<Texture2D>(Data.Background);
            }
            catch
            {
                // Load color instead?
            }

            Menus = new List<MenuBase>();
            if (Data.Menus != null)
            {
                foreach (var _mData in Data.Menus)
                {
                    Menus.Add(GMenus.Create(_mData, null));
                }
            }
        }

        public T GetMenuItem<T>(string name, MenuBase search = null) where T : MenuBase
        {
            List<MenuBase> _searchItem = search?.Items ?? Menus;

            foreach(var _item in _searchItem)
            {
                if (_item.Name == name && _item is T)
                    return (T)_item;

                var _retItem = GetMenuItem<T>(name, _item);
                if (_retItem != null)
                    return _retItem;
            }

            return null;
        }

        public virtual void Opened()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var _menu in Menus)
                _menu.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (BackgroundTex != null)
                sb.Draw(
                    BackgroundTex, 
                    new Rectangle(0, 0, GGraphics.Width, GGraphics.Height), 
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    1f
                );

            foreach (var _menu in Menus)
                _menu.Draw(gameTime, sb);
        }
    }
}
