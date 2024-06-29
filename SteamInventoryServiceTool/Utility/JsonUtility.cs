using System.Collections.Generic;
using Newtonsoft.Json;
using SteamInventoryServiceTool.Data.Steam.Fields;

namespace SteamInventoryServiceTool.Utility;

public static class JsonUtility
{
    public static JsonSerializerSettings GetJsonSettings()
    {
        var settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;
        settings.Converters = new List<JsonConverter>()
        {
            new BundleJsonConverter(),
            new HexColorJsonConverter(),
            new PriceCategoryJsonConverter(),
            new PromoJsonConverter(),
            new TagsJsonConverter()
        };
        return settings;
    }
}