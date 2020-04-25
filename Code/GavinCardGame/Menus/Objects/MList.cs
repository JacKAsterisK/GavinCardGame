using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GavinCardGame.GSystems;

namespace GavinCardGame.Menus.Objects
{
    public class MListItem : MenuBase
    {
        public MList List { get; private set; }
        public int Index { get; private set; }

        public MListItem(MenuData data, MenuBase parent) : base(data, parent)
        {
        }

        public void SetList(MList list, int index)
        {
            List = list;
            Index = index;

            Position = Vector2.UnitY * Index * (List.ItemHeight + List.ItemSpacing);
            Size = new Vector2(List.Size.X, List.ItemHeight);
            HAlign = MenuHAlign.Left;
            VAlign = MenuVAlign.Top;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);

            Vector2 _pos = ActualPosition;

            Vector2 _tSize = GContent.MenuFont.MeasureString(Text);
            Vector2 _tPos = _pos;
            
            _tPos.X += Size.X / 2 - _tSize.X / 2;
            _tPos.Y += Size.Y / 2 - _tSize.Y / 2;

            sb.DrawString(GContent.MenuFont, Text, _tPos, Color.White);
        }
    }

    public class MList : MenuBase
    {
        public float ItemHeight { get; private set; }
        public float ItemSpacing { get; private set; }
        public List<MListItem> ListItems { get; private set; }

        public MList(MenuData data, MenuBase parent) : base(data, parent)
        {
            ItemHeight = FloatFromString(data.GetDataProperty("ItemHeight").ToString(), false);
            ItemSpacing = FloatFromString(data.GetDataProperty("ItemSpacing").ToString(), false);

            ListItems = new List<MListItem>();
            for (int _index = 0; _index < Items.Count; _index++)
            {
                var _item = Items[_index];

                if (_item is MListItem)
                {
                    var _listItem = (MListItem)_item;
                    _listItem.SetList(this, _index);
                    ListItems.Add(_listItem);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var _item in ListItems)
                _item.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (var _item in ListItems)
                _item.Draw(gameTime, sb);

            base.Draw(gameTime, sb);
        }
    }
}
