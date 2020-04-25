using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GavinCardGame.Systems
{
    public class GSettings : GSystemBase
    {
        public string Name { get; set; }
        public int Port;

        public GSettings()
        {
            string _settingsText = File.ReadAllText("settings.json");

            var _settingsObj = JObject.Parse(_settingsText);

            Name = _settingsObj["Name"].ToString();
            Port = int.Parse(_settingsObj["Port"].ToString());
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
        }
    }
}
