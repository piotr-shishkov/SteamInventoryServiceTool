using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamInventoryServiceTool.Data.Steam.Fields;

public class Tags
{
    public Dictionary<string, string> TagsDict { get; set; } = new Dictionary<string, string>();


    public Tags()
    {
        
    }

    public Tags(string sourceString)
    {
        if (string.IsNullOrWhiteSpace(sourceString))
            return;
        
        var tagsDict = new Dictionary<string, string>();
        // Separating all tags
        var separatedTags = sourceString.Split(';');
        foreach (var tagPair in separatedTags)
        {
            // Separate single item by item and its count
            var split = tagPair.Split(':');
            var tag = split[0];
            var tagValue = split[1];

            tagsDict.Add(tag, tagValue);
        }
        TagsDict = tagsDict;
    }
    
    public string GetString()
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
        var jsonString = string.Empty;
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        jsonString = value.GetString();
        writer.WriteValue(jsonString);
    }
}