using GavinCardGame.Menus;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GavinCardGame.Screens
{
    public class ScreenData
    {
        public JObject JObject { get; private set; }

        public string Background;
        public List<MenuData> Menus;

        public ScreenData(JObject jObject)
        {
            JObject = jObject;

            Background = JObject["Background"]?.ToString();

            Menus = new List<MenuData>();
            var _menus = (JArray)JObject["Menus"];
            if (_menus != null && _menus.Count > 0)
            {
                foreach (var _menu in _menus)
                {
                    Menus.Add(new MenuData((JObject)_menu));
                }
            }
        }
    }
}
