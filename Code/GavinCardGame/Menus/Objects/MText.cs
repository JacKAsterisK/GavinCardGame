using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static GavinCardGame.GSystems;

namespace GavinCardGame.Menus.Objects
{
    public class MText : MenuBase
    {
        public Systems.AllowKeyString Filters { get; set; }

        public delegate void EnterPressed(MText textBox);
        public event EnterPressed OnEnterPressed;

        public MText(MenuData data, MenuBase parent) : base(data, parent)
        {
            ShowText = true;
            Filters = Systems.AllowKeyString.All;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Focused)
            {
                foreach (var _key in GInput.NewPressedKeys)
                {
                    switch (_key)
                    {
                        case Keys.Back:
                            if (Text.Length > 0)
                                Text = Text.Substring(0, Text.Length - 1);
                            break;

                        case Keys.Enter:
                            OnEnterPressed?.Invoke(this);
                            break;

                        default:
                            Text += GInput.KeyToString(_key, Filters);
                            break;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }
    }
}
