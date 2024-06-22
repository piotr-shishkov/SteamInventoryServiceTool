using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
