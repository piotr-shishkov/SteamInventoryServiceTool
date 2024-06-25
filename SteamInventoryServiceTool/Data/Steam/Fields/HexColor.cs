using Newtonsoft.Json;
using System;
using System.Drawing;

namespace SteamInventoryServiceTool.Data.Steam.Fields
{
    public class HexColor
    {
        public Color Color { get; set; }

        public HexColor(int r, int g, int b)
        {
            Color = Color.FromArgb(r, g, b);
        }
    }

    public class HexColorJsonConverter : JsonConverter<HexColor>
    {
        public override HexColor? ReadJson(JsonReader reader, Type objectType, HexColor? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonString = reader.Value as string;
            if(jsonString == null)
            {
                return new HexColor(1, 1, 1);
            }
            var color = ColorTranslator.FromHtml(jsonString);
            return new HexColor(color.R, color.G, color.B);
        }

        public override void WriteJson(JsonWriter writer, HexColor? value, JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }
            var color = value.Color;
            var hexString = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            writer.WriteValue(hexString);
        }
    }
}
