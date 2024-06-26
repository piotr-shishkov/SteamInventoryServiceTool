using System;
using System.Collections.Generic;
using System.Linq;
using SteamInventoryServiceTool.Data.Steam;

namespace SteamInventoryServiceTool.Workspace
{
	[Serializable]
	public class Workspace
	{
		public int AppId { get; private set; }
		public string Name { get; set; }
		public List<Item> Items { get; set; } = new();

		public Workspace(int appId, string name = "EmptyWorkspace")
		{
			AppId = appId;
			Name = name;
		}

		public void AddItem(Item item)
		{
			
		}

		public int GetNextItemId()
		{
			if (Items.Count == 0)
				return 1;

			// Extract IDs and sort them
			var ids = Items.Select(item => item.Id).OrderBy(id => id).ToList();

			// Check for the first missing ID
			for (var i = 0; i < ids.Count; i++)
			{
				if (ids[i] != i + 1)
					return i + 1;
			}

			// If all IDs are sequential, return the next ID
			return ids.Count + 1;
		}
	}
}
