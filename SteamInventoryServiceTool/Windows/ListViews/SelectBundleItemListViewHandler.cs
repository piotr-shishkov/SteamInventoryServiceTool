using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Data.Steam.Misc;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.ListViews;

public class SelectBundleItemListViewHandler
{
    private ListView _listView;
    private Item _item;
    private readonly Workspace _workspace;

    public SelectBundleItemListViewHandler(ListView listView, Item item, Workspace workspace)
    {
        _listView = listView;
        _item = item;
        _workspace = workspace;

        _listView.KeyDown += OnListKeyDown;
        EnsureItemName();
        Update();
    }

    private void EnsureItemName()
    {
        //
    }

    private void OnListKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete)
        {
            var selectedItems = _listView.SelectedItems.Cast<KeyValuePair<KeyValuePair<int, string>, int>>().ToList();
            if (selectedItems.Any())
            {
                var result = MessageBox.Show(
                    $"Are you sure want to delete {selectedItems.Count} tag(s)?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var item in selectedItems)
                    {
                        _item.Bundle.Items.Remove(item.Key.Key);
                    }
                }
                Update();
            }
        }
    }

    public void Update()
    {
        FillItems();
    }

    private void FillItems()
    {
        _listView.Items.Clear();
        foreach (var item in _item.Bundle.Items)
        {
            var itemName = _workspace.Items.FirstOrDefault(x => x.Id == item.Key)?.Name ?? item.Key.ToString();
            var pair = new KeyValuePair<KeyValuePair<int ,string>, int>(new KeyValuePair<int, string>(item.Key, itemName), item.Value.Value);
            _listView.Items.Add(pair);
        }
    }
}