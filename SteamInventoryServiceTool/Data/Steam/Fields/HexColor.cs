using Newtonsoft.Json;
using System;
using System.Windows.Media;

namespace SteamInventoryServiceTool.Data.Steam.Fields;

public class HexColor
{
    public Color Color { get; set; } = Color.FromArgb(255, 255, 255, 255);

    public HexColor() { }

    public HexColor(byte a, byte r, byte g, byte b)
    {
        Color = Color.FromArgb(a, r, g, b);
    }
}

public class HexColorJsonConverter : JsonConverter<HexColor>
{
    public override HexColor? ReadJson(JsonReader reader, Type objectType, HexColor? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonString = reader.Value as string;
        if (jsonString == null)
        {
            return new HexColor();
        }

        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return new HexColor(0, 255, 255, 255);
        }
        var color = (Color)ColorConverter.ConvertFromString(jsonString);
        return new HexColor(color.A, color.R, color.G, color.B);
    }

    public override void WriteJson(JsonWriter writer, HexColor? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }
        var color = value.Color;
        var hexString = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        if (color.A == 0)
            hexString = "";
        writer.WriteValue(hexString);
    }
}