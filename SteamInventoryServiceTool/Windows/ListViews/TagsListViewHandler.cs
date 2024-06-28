using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.ListViews;

public class TagsListViewHandler
{
    private ListView _listView;
    private Workspace _workspace;

    public TagsListViewHandler(ListView listView)
    {
        _listView = listView;
        _listView.KeyDown += OnListKeyDown;
    }
    
    public void SetWorkspace(Workspace workspace)
    {
        if (_workspace != null)
        {
            _workspace.Updated -= OnUpdated;
        }
        _workspace = workspace;
        _workspace.Updated += OnUpdated;
        OnUpdated();
    }

    private void OnListKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete)
        {
            var selectedItems = _listView.SelectedItems.Cast<KeyValuePair<string, List<string>>>().ToList();
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
                        foreach (var value in item.Value.ToArray())
                        {
                            _workspace.RemoveTag(item.Key, value);
                        }
                    }
                }
            }
        }
    }

    private void OnUpdated()
    {
        FillWorkspaceTags();
    }
    
    private void FillWorkspaceTags()
    {
        _listView.ItemsSource = null;
        _listView.ItemsSource = _workspace.Tags;
    }
}