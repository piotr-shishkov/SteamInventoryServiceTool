using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamInventoryServiceTool.Data.Steam.Fields;

public class Tags
{
    public Dictionary<string, string> TagsDict { get; set; } = new Dictionary<string, string>();
    
    public Tags() { }

    public Tags(string sourceString)
    {
        if (string.IsNullOrWhiteSpace(sourceString))
            return;
        
        // Separating all tags
        var separatedTags = sourceString.Split(';');
        foreach (var tagPair in separatedTags)
        {
            // Separate single item by item and its count
            var split = tagPair.Split(':');
            var tag = split[0];
            var tagValue = split[1];

            TagsDict.Add(tag, tagValue);
        }
    }

    public override string ToString()
    {
        var str = string.Empty;
        var i = 0;
        foreach (var (tag, tagValue) in TagsDict)
        {
            str += tag;
            str += $":{tagValue}";

            // Check if not last item
            if (i++ < TagsDict.Count - 1)
            {
                str += ";";
            }
        }
        return str;
    }
}

public class TagsJsonConverter : JsonConverter<Tags>
{
    public override Tags? ReadJson(JsonReader reader, Type objectType, Tags? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonString = reader.Value as string;
        return new Tags(jsonString);
    }

    public override void WriteJson(JsonWriter writer, Tags? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var jsonString = value.ToString();
        writer.WriteValue(jsonString);
    }
}