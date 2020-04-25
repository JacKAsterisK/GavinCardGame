using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GavinCardGame.Menus
{
    public class MenuData
    {
        public JObject JObject { get; private set; }

        public string Name;
        public string Type;
        public string Text;
        public string Position;
        public string Size;
        public string Align;
        public string TextAlign;
        public string TextMargin;
        public string ShowText;
        public string Bgc;
        public string HoverBgc;
        public string FocusBgc;
        public string FocusHoverBgc;
        public string BorderColor;
        public string BorderThickness;
        public string ShowType;
        public List<MenuData> Items;

        public MenuData(JObject jObj)
        {
            JObject = jObj;

            Name = JObject["Name"]?.ToString();
            Type = JObject["Type"]?.ToString();
            Text = JObject["Text"]?.ToString();
            Position = JObject["Position"]?.ToString();
            Size = JObject["Size"]?.ToString();
            Align = JObject["Align"]?.ToString();
            TextAlign = JObject["TextAlign"]?.ToString();
            TextAlign = JObject["TextAlign"]?.ToString();
            ShowText = JObject["ShowText"]?.ToString();
            Bgc = JObject["Bgc"]?.ToString();
            HoverBgc = JObject["HoverBgc"]?.ToString();
            FocusBgc = JObject["FocusBgc"]?.ToString();
            FocusHoverBgc = JObject["FocusHoverBgc"]?.ToString();
            BorderColor = JObject["BorderColor"]?.ToString();
            BorderThickness = JObject["BorderThickness"]?.ToString();
            ShowType = JObject["ShowType"]?.ToString();

            Items = new List<MenuData>();
            var _itemsArr = (JArray)JObject["Items"];
            if (_itemsArr != null)
            {
                foreach (var _item in _itemsArr)
                    Items.Add(new MenuData((JObject)_item));
            }
        }

        public JToken GetDataProperty(string property)
        {
            return JObject[property];
        }
    }
}
