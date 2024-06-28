using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SteamInventoryServiceTool.Data.Steam;

namespace SteamInventoryServiceTool.Workspaces;

[Serializable]
public class Workspace
{
	public int AppId { get; set; }
	public string Name { get; set; }
	public List<string> Tags { get; set; } = new();
	public List<Item> Items { get; set; } = new();
		
	[JsonIgnore]
	public string FilePath { get; set; }

	public event Action Updated;

	public Workspace(int appId, string name = "EmptyWorkspace")
	{
		AppId = appId;
		Name = name;
	}

	#region Tags Control

	public void AddTag(string tag)
	{
		if(string.IsNullOrWhiteSpace(tag))
			return;
		
		Tags.Add(tag);
		Update();
		Save();
	}

	public void RemoveTag(string tag)
	{
		if(!Tags.Remove(tag))
			return;
		Update();
		Save();
	}

	#endregion

	#region Item Control
	
	public void AddItem(Item item)
	{
		var existingItem = Items.FirstOrDefault(x => x.Id == item.Id);
		if (existingItem != null)
		{
			Items.Remove(existingItem);
		}
		Items.Add(item);
		SortItems();
		Update();
		Save();
	}

	public void RemoveItem(Item item)
	{
		if(!Items.Remove(item))
			return;
		SortItems();
		Update();
		Save();
	}

	private void SortItems()
	{
		Items.Sort((x, y) => x.Id.CompareTo(y.Id));
	}
	
	#endregion

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

	public void Save()
	{
		if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
		{
			WorkspaceFileOperations.SaveWorkspace(this);
		}
		else
		{
			WorkspaceFileOperations.SaveWorkspaceWithDialog(this);
		}
	}

	public void Update()
	{
		Updated?.Invoke();
	}
}