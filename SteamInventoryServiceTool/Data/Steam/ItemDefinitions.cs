using Newtonsoft.Json;
using System;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Data.Steam;

[Serializable]
public class ItemDefinitions
{
	[JsonProperty("appid")]
	public int AppId { get; set; } = 0;

	[JsonProperty("items")]
	public Item[] Items { get; set; } = Array.Empty<Item>();

	public ItemDefinitions() { }
	
	public ItemDefinitions(Workspace workspace)
	{
		AppId = workspace.AppId;
		Items = workspace.Items.ToArray();
	}
}