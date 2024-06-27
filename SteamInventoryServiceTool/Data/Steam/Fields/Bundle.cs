using Newtonsoft.Json;
using SteamInventoryServiceTool.Data.Steam.Misc;
using System;
using System.Collections.Generic;

namespace SteamInventoryServiceTool.Data.Steam.Fields;

public class Bundle
{
	public Dictionary<Item, ItemCount> Items { get; set; } = new Dictionary<Item, ItemCount>();
}

public class BundleJsonConverter : JsonConverter<Bundle>
{
	public override Bundle? ReadJson(JsonReader reader, Type objectType, Bundle? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
			var jsonString = reader.Value as string;
			if (jsonString == null)
			{
				return new Bundle();
			}

			var itemsDict = new Dictionary<Item, ItemCount>();
			// Separating all items
			var separatedItems = jsonString.Split(';');
			foreach (var value in separatedItems)
			{
				// Separate single item by item and its count
				var split = value.Split('x');
				var itemId = int.Parse(split[0]);
				var count = int.Parse(split[1]);

				var countValue = new ItemCount();
				countValue.SetPercent(count);
				itemsDict.Add(new(itemId), countValue);
			}


			return new Bundle()
			{
				Items = itemsDict
			};
		}

	public override void WriteJson(JsonWriter writer, Bundle? value, JsonSerializer serializer)
	{
			var jsonString = string.Empty;
			if(value == null)
			{
				writer.WriteNull();
				return;
			}

			var i = 0;
			foreach (var (item, count) in value.Items)
			{
				// Appending item ID (itemdefid) first
				jsonString += item.Id;

				// Now same, for future purposes
				if (count.PercentBased)
				{
					jsonString += $"x{count.Value}";
				}
				else
				{
					jsonString += $"x{count.Value}";
				}

				// Check if not last item
				if (i++ < value.Items.Count - 1)
				{
					jsonString += ";";
				}
			}
			writer.WriteValue(jsonString);
		}
}