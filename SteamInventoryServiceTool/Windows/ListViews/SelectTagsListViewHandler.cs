using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.ListViews;

public class SelectTagsListViewHandler
{
    private ListView _listView;
    private Item _item;

    public SelectTagsListViewHandler(ListView listView, Item item)
    {
        _listView = listView;
        _item = item;
        
        _listView.KeyDown += OnListKeyDown;
        Update();
    }

    private void OnListKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete)
        {
            var selectedItems = _listView.SelectedItems.Cast<KeyValuePair<string, string>>().ToList();
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
                        _item.Tags.TagsDict.Remove(item.Key);
                    }
                }
                Update();
            }
        }
    }

    public void Update()
    {
        FillItemTags();
    }
    
    private void FillItemTags()
    {
        _listView.ItemsSource = null;
        _listView.ItemsSource = _item.Tags.TagsDict.ToList();
    }
}