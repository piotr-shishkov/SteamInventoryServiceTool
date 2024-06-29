using Newtonsoft.Json;
using SteamInventoryServiceTool.Data.Steam.Misc;
using System;
using System.Collections.Generic;

namespace SteamInventoryServiceTool.Data.Steam.Fields;

public class Bundle
{
	public Dictionary<int, ItemCount> Items { get; set; } = new Dictionary<int, ItemCount>();

	public Bundle() { }

	public Bundle(string sourceString)
	{
		if (string.IsNullOrWhiteSpace(sourceString))
		{
			return;
		}

		// Separating all items
		var separatedItems = sourceString.Split(';');
		foreach (var value in separatedItems)
		{
			// Separate single item by item and its count
			var split = value.Split('x');
			var itemId = int.Parse(split[0]);
			var count = int.Parse(split[1]);

			var countValue = new ItemCount();
			countValue.SetPercent(count);
			Items.Add(itemId, countValue);
		}
	}

	public override string ToString()
	{
		var str = string.Empty;
		var i = 0;
		foreach (var (id, count) in Items)
		{
			// Appending item ID (itemdefid) first
			str += id;

			// Now same, for future purposes
			if (count.PercentBased)
			{
				str += $"x{count.Value}";
			}
			else
			{
				str += $"x{count.Value}";
			}

			// Check if not last item
			if (i++ < Items.Count - 1)
			{
				str += ";";
			}
		}
		return str;
	}
}

public class BundleJsonConverter : JsonConverter<Bundle>
{
	public override Bundle? ReadJson(JsonReader reader, Type objectType, Bundle? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		var jsonString = reader.Value as string;
		return new Bundle(jsonString);
	}

	public override void WriteJson(JsonWriter writer, Bundle? value, JsonSerializer serializer)
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