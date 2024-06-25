using Newtonsoft.Json;
using System;

namespace SteamInventoryServiceTool.Data.Steam
{
	[Serializable]
	public class ItemDefinitions
	{
		[JsonProperty("appid")]
		public int AppId { get; set; }

		[JsonProperty("items")]
		public Item[] Items { get; set; }
	}
}
