using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static GavinCardGame.GSystems;

namespace GavinCardGame.Menus
{
    public enum MenuHAlign
    {
        Left,
        Right,
        Center,
    }

    public enum MenuVAlign
    {
        Top,
        Bottom,
        Middle,
    }

    public class MenuBase
    {
        private MenuData Data;

        static Regex _PosNumReg = new Regex(@"[0-9]+");

        public delegate void Clicked(MenuBase mBase);
        public event Clicked OnClicked;

        public delegate void LostFocus(MenuBase mBase);
        public event LostFocus OnLostFocus;

        public bool Hovering { get { return Bounds.Contains(GInput.MousePos); } }
        public bool Focused { get; private set; }

        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Text { get; set; }
        public Vector2 Position { get; protected set; }
        public Vector2 Size { get; protected set; }
        public float Depth { get; protected set; }
        public MenuHAlign HAlign { get; protected set; }
        public MenuVAlign VAlign { get; protected set; }
        public MenuHAlign TextHAlign { get; protected set; }
        public MenuVAlign TextVAlign { get; protected set; }
        public bool ShowText { get; protected set; }
        public Color? Bgc { get; private set; }
        public Color? HoverBgc { get; private set; }
        public Color? FocusBgc { get; private set; }
        public Color? FocusHoverBgc { get; private set; }
        public Color? BorderColor { get; set; }
        public int BorderThickness;
        public bool ShowType { get; private set; }
        public MenuBase Parent { get; private set; }
        public List<MenuBase> Items { get; private set; }

        public float StringBorderDepthAdd = 0.01f;

        public Vector2 ActualPosition
        {
            get
            {
                Vector2 _outPos = Vector2.Zero;
                Vector2 _refSize = new Vector2(GGraphics.Width, GGraphics.Height);

                if (Parent != null)
                {
                    _outPos = Parent.ActualPosition;
                    _refSize = Parent.ActualSize;
                }

                switch (HAlign)
                {
                    case MenuHAlign.Left: _outPos.X += Position.X; break;
                    case MenuHAlign.Right: _outPos.X += _refSize.X - Size.X - Position.X; break;
                    case MenuHAlign.Center: _outPos.X += _refSize.X / 2 - Size.X / 2; break;
                }

                switch (VAlign)
                {
                    case MenuVAlign.Top: _outPos.Y += Position.Y; break;
                    case MenuVAlign.Bottom: _outPos.Y += _refSize.Y - Size.Y - Position.Y; break;
                    case MenuVAlign.Middle: _outPos.Y += _refSize.Y / 2 - Size.Y / 2; break;
                }

                return _outPos;
            }
        }

        public Vector2 ActualSize { get { return Size; } }

        public Rectangle Bounds
        {
            get
            {
                Vector2 _pos = ActualPosition;
                Vector2 _size = ActualSize;

                return new Rectangle(
                    (int)_pos.X,
                    (int)_pos.Y,
                    (int)_size.X,
                    (int)_size.Y
                );
            }
        }

        public MenuBase(MenuData data, MenuBase parent)
        {
            Parent = parent;
            Data = data;

            Name = Data.Name;
            Type = Data.Type;
            Text = Data.Text;

            if (!string.IsNullOrWhiteSpace(Data.Position))
                Position = Vec2FromString(Data.Position);

            if (!string.IsNullOrWhiteSpace(Data.Size))
                Size = Vec2FromString(Data.Size);

            if (!string.IsNullOrWhiteSpace(Data.Depth))
                Depth = float.Parse(Data.Depth);
            else
                Depth = 0.99f;

            if (!string.IsNullOrWhiteSpace(Data.Align))
            {
                string[] _alignSplit = Data.Align.Split(',');

                foreach (var _ha in Enum.GetValues(typeof(MenuHAlign)))
                {
                    if (_alignSplit[0].Trim().ToLower() == _ha.ToString().ToLower())
                        HAlign = (MenuHAlign)_ha;
                }

                foreach (var _va in Enum.GetValues(typeof(MenuVAlign)))
                {
                    if (_alignSplit[1].Trim().ToLower() == _va.ToString().ToLower())
                        VAlign = (MenuVAlign)_va;
                }
            }
            else
            {
                HAlign = MenuHAlign.Left;
                VAlign = MenuVAlign.Top;
            }

            if (!string.IsNullOrWhiteSpace(Data.TextAlign))
            {
                string[] _alignSplit = Data.TextAlign.Split(',');

                foreach (var _ha in Enum.GetValues(typeof(MenuHAlign)))
                {
                    if (_alignSplit[0].Trim().ToLower() == _ha.ToString().ToLower())
                        TextHAlign = (MenuHAlign)_ha;
                }

                foreach (var _va in Enum.GetValues(typeof(MenuVAlign)))
                {
                    if (_alignSplit[1].Trim().ToLower() == _va.ToString().ToLower())
                        TextVAlign = (MenuVAlign)_va;
                }
            }
            else
            {
                TextHAlign = MenuHAlign.Center;
                TextVAlign = MenuVAlign.Middle;
            }

            if ((Data.ShowText ?? "").ToLower() == "yes")
                ShowText = true;

            if (!string.IsNullOrWhiteSpace(Data.Bgc))
                Bgc = ColorFromString(Data.Bgc);
            else
                Bgc = null;
            if (!string.IsNullOrWhiteSpace(Data.HoverBgc))
                HoverBgc = ColorFromString(Data.HoverBgc);
            else
                HoverBgc = null;

            if (!string.IsNullOrWhiteSpace(Data.FocusBgc))
                FocusBgc = ColorFromString(Data.FocusBgc);
            else
                FocusBgc = null;
            if (!string.IsNullOrWhiteSpace(Data.FocusHoverBgc))
                FocusHoverBgc = ColorFromString(Data.FocusHoverBgc);
            else
                FocusHoverBgc = null;

            if (!string.IsNullOrWhiteSpace(Data.BorderColor))
                BorderColor = ColorFromString(Data.BorderColor);
            else
                BorderColor = null;

            BorderThickness = 1;
            int.TryParse(Data.BorderThickness, out BorderThickness);

            if ((Data.ShowType ?? "").ToLower() == "yes")
                ShowType = true;

            Items = new List<MenuBase>();
            if (Data.Items != null && Data.Items.Count > 0)
            {
                foreach (var _itemData in Data.Items)
                {
                    var _sItem = GMenus.Create(_itemData, this);
                    Items.Add(_sItem);
                }
            }
        }

        public Vector2 Vec2FromString(string vec2)
        {
            string[] _posSplit = vec2.Split(',');
            if (_posSplit.Length == 2)
            {
                string _xS = _posSplit[0].Trim();
                string _yS = _posSplit[1].Trim();

                return new Vector2(FloatFromString(_xS, true), FloatFromString(_yS, false));
            }

            return Vector2.Zero;
        }

        public float FloatFromString(string fl, bool isX)
        {
            float _out = float.Parse(_PosNumReg.Match(fl).Value);

            Vector2 _refSize = Parent != null ? Parent.ActualSize : new Vector2(GGraphics.Width, GGraphics.Height);

            if (fl.Contains("%"))
                _out = _out / 100f * (isX ? _refSize.X : _refSize.Y);

            return _out;
        }

        public Color ColorFromString(string c)
        {
            var _cSplit = c.Split(',');

            if (_cSplit.Length != 4)
                return Color.White;

            return new Color(
                byte.Parse(_cSplit[0].Trim()),
                byte.Parse(_cSplit[1].Trim()),
                byte.Parse(_cSplit[2].Trim()),
                byte.Parse(_cSplit[3].Trim())
            );
        }

        public JToken GetDataProperty(string property)
        {
            return Data.GetDataProperty(property);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Hovering && GInput.MouseNewDown)
            {
                Focused = true;

                OnClicked?.Invoke(this);
            }
            else if (!Hovering && GInput.MouseNewDown)
            {
                Focused = false;

                OnLostFocus?.Invoke(this);
            }

            foreach (var _item in Items)
                _item.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (!Focused || FocusBgc == null)
            {
                if (Bgc != null)
                    GGraphics.FillRectangle(
                        Bounds, 
                        Hovering ? HoverBgc ?? Bgc.Value : Bgc.Value,
                        Depth, 
                        sb
                    );
            }
            else if (Focused && FocusBgc != null)
            {
                if (FocusBgc != null)
                    GGraphics.FillRectangle(
                        Bounds, 
                        Hovering ? FocusHoverBgc ?? FocusBgc.Value : FocusBgc.Value, 
                        Depth, 
                        sb
                    );
            }

            if (BorderColor != null)
                GGraphics.DrawRectangle(
                    Bounds, 
                    BorderColor.Value, 
                    BorderThickness,
                    Depth - StringBorderDepthAdd, 
                    sb
                );

            foreach (var _item in Items)
                _item.Draw(gameTime, sb);


            // Show type
            if (ShowType && !string.IsNullOrWhiteSpace(Type))
            {
                Vector2 _pos = ActualPosition;

                Vector2 _tSize = GContent.MenuFont.MeasureString(Type);
                Vector2 _tPos = _pos;

                _tPos.X += Size.X / 2 - _tSize.X / 2;
                _tPos.Y += Size.Y / 2 - _tSize.Y / 2;

                sb.DrawString(
                    GContent.MenuFont, 
                    Type, 
                    _tPos, 
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    Depth - StringBorderDepthAdd
                );
            }

            // Show text
            if (ShowText)
            {
                if (!string.IsNullOrWhiteSpace(Text))
                {
                    Vector2 _pos = ActualPosition;

                    Vector2 _tSize = GContent.MenuFont.MeasureString(Text);
                    Vector2 _tPos = _pos;

                    switch (TextHAlign)
                    {
                        case MenuHAlign.Left: break;
                        case MenuHAlign.Right: _tPos.X += ActualSize.X - _tSize.X;  break;
                        case MenuHAlign.Center:  _tPos.X += Size.X / 2 - _tSize.X / 2; break;
                    }
                    switch (TextVAlign)
                    {
                        case MenuVAlign.Top: break;
                        case MenuVAlign.Bottom: _tPos.Y += ActualSize.Y - _tSize.Y; break;
                        case MenuVAlign.Middle: _tPos.Y += Size.Y / 2 - _tSize.Y / 2; break;
                    }

                    sb.DrawString(
                        GContent.MenuFont, 
                        Text, 
                        _tPos, 
                        Color.White, 
                        0f, 
                        Vector2.Zero, 
                        1, 
                        SpriteEffects.None,
                        Depth - StringBorderDepthAdd
                    );
                }
            }
        }
    }
}
