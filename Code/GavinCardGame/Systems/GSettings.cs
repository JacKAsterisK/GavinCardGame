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

namespace GavinCardGame.Systems
{
    public class GSettings : GSystemBase
    {
        private string _SettingsFile;

        public string Name { get; set; }
        public int Port;
        public string DefaultHost;

        public GSettings()
        {
            var _settingsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GavinCardGame");
            if (!Directory.Exists(_settingsDir))
                Directory.CreateDirectory(_settingsDir);

            _SettingsFile = Path.Combine(_settingsDir, "settings.json");
            if (!File.Exists(_SettingsFile))
            {
                Name = "UserName";
                Port = 1565;
                DefaultHost = "";
                Save();
            }

            string _settingsText = File.ReadAllText(_SettingsFile);

            var _settingsObj = JObject.Parse(_settingsText);

            Name = _settingsObj["Name"].ToString();
            Port = int.Parse(_settingsObj["Port"].ToString());
            DefaultHost = _settingsObj["DefaultHost"].ToString();
        }

        public void Save()
        {
            using (var _sw = new StreamWriter(_SettingsFile, false))
            {
                var _settingsObj = new JObject();
                _settingsObj["Name"] = Name;
                _settingsObj["Port"] = Port;
                _settingsObj["DefaultHost"] = DefaultHost;

                _sw.Write(JsonConvert.SerializeObject(_settingsObj));
            }
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
        }
    }
}
