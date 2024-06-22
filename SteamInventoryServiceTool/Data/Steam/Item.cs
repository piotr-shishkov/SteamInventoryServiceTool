using Newtonsoft.Json;
using SteamInventoryServiceTool.Data.Steam.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryServiceTool.Data.Steam
{
	[Serializable]
	public class Item
	{
		[JsonProperty("itemdefid")]
		public int Id { get; set; }
		[JsonProperty("type")]
		public ItemType Type { get; set; }
		[JsonProperty("bundle")]
		public Bundle Bundle { get; set; }
		[JsonProperty("promo")]
		public Promo Promo { get; set; }

		public Item(int id)
		{
			Id = id;
		}
	}
}
