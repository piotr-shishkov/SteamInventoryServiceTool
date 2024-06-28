using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamInventoryServiceTool.Data.Steam.Fields;

public class Tags
{
    public Dictionary<string, string> TagsDict { get; set; } = new Dictionary<string, string>();
}

public class TagsJsonConverter : JsonConverter<Tags>
{
    public override Tags? ReadJson(JsonReader reader, Type objectType, Tags? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonString = reader.Value as string;
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return new Tags();
        }

        var tagsDict = new Dictionary<string, string>();
        // Separating all tags
        var separatedTags = jsonString.Split(';');
        foreach (var tagPair in separatedTags)
        {
            // Separate single item by item and its count
            var split = tagPair.Split(':');
            var tag = split[0];
            var tagValue = split[1];

            tagsDict.Add(tag, tagValue);
        }

        return new Tags()
        {
            TagsDict = tagsDict
        };
    }

    public override void WriteJson(JsonWriter writer, Tags? value, JsonSerializer serializer)
    {
        var jsonString = string.Empty;
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var i = 0;
        foreach (var (tag, tagValue) in value.TagsDict)
        {
            jsonString += tag;
            jsonString += $":{tagValue}";

            // Check if not last item
            if (i++ < value.TagsDict.Count - 1)
            {
                jsonString += ";";
            }
        }
        writer.WriteValue(jsonString);
    }
}