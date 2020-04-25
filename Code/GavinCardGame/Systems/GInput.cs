using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GavinCardGame.Systems
{
    [Flags]
    public enum AllowKeyString : short
    {
        None =          0,

        Letters =       0b00000001,
        Numbers =       0b00000010,
        SpecialChars =  0b00000100,
        Space =         0b00001000,
        Ip =            0b00010000,

        All =           0b11111111,
    }

    public class GInput : GSystemBase
    {
        public MouseState Mouse { get; private set; }
        public MouseState LastMouse { get; private set; }

        public KeyboardState Keyboard { get; private set; }
        public KeyboardState LastKeyboard { get; private set; }

        static Regex _LetterCharReg = new Regex(@"[a-zA-Z]");
        static Regex _NumCharReg = new Regex(@"d[0-9]");
        static Regex _SpecialCharReg = new Regex(@"[^a-zA-Z0-9 ]");

        public Vector2 MousePos { get { return new Vector2(Mouse.Position.X, Mouse.Position.Y); } }
        public bool MouseDown { get { return Mouse.LeftButton == ButtonState.Pressed;  } }
        public bool LastMouseDown { get { return LastMouse.LeftButton == ButtonState.Pressed; } }
        public bool MouseNewDown { get { return MouseDown && !LastMouseDown; } }

        public bool CursorVisible { get; set; }


        public Keys[] PressedKeys { get { return Keyboard.GetPressedKeys(); } }
        public Keys[] LastPressedKeys { get { return LastKeyboard.GetPressedKeys(); } }
        public IEnumerable<Keys> NewPressedKeys
        {
            get
            {
                return
                    from k in PressedKeys
                    where !LastPressedKeys.Contains(k)
                    select k
                ;
            }
        }

        public GInput()
        {
            Mouse = Microsoft.Xna.Framework.Input.Mouse.GetState(MainGame.Game.Window);
            Keyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }

        public string KeyToString(Keys key, AllowKeyString allowKeys = AllowKeyString.Letters)
        {
            return KeyToString(key, Keyboard, allowKeys);
        }
        public string KeyToString(Keys key, KeyboardState kb, AllowKeyString allowKeys = AllowKeyString.Letters)
        {
            string _out;

            switch (key)
            {
                // -----------------------------------------

                case Keys.Space: _out = " "; break;
                case Keys.OemComma: _out = ","; break;
                case Keys.OemPeriod:
                case Keys.Decimal: _out = "."; break;

                // TODO: ADD MORE KEYS HERE

                // -----------------------------------------

                default:
                    _out = kb.IsKeyDown(Keys.LeftShift) ? key.ToString() : key.ToString().ToLower();

                    if (_out.StartsWith("numpad"))
                        _out = _out.Replace("numpad", "");
                    if (_out.Length == 2 && _out.StartsWith("d") && _NumCharReg.IsMatch(_out))
                        _out = _out.Substring(1);

                    if (_out.Length > 1)
                        _out = "";

                    break;
            }

            // Filters
            if (!string.IsNullOrWhiteSpace(_out))
            {
                if ((allowKeys & AllowKeyString.Letters) == 0 && _LetterCharReg.IsMatch(_out))
                    _out = "";

                if ((allowKeys & AllowKeyString.Numbers) == 0 && _NumCharReg.IsMatch(_out))
                    _out = "";

                if ((allowKeys & AllowKeyString.Space) == 0 && _out == " ")
                    _out = "";

                if ((allowKeys & AllowKeyString.SpecialChars) == 0 && _SpecialCharReg.IsMatch(_out))
                    _out = "";
            }

            return _out;
        }

        public override void Update(GameTime gameTime)
        {
            LastMouse = Mouse;
            Mouse = Microsoft.Xna.Framework.Input.Mouse.GetState(MainGame.Game.Window);

            LastKeyboard = Keyboard;
            Keyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (CursorVisible)
            {
                int _width = 8;
                int _hW = _width / 2;

                GSystems.GGraphics.FillRectangle(
                    new Rectangle(
                        (int)MousePos.X - _hW,
                        (int)MousePos.Y - _hW,
                        _width,
                        _width
                    ),
                    Color.White,
                    0,
                    sb
                );
            }
        }
    }
}
