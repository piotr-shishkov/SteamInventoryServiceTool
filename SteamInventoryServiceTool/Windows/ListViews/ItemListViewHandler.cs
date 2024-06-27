using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.ListViews;

public class ItemListViewHandler
{
    private ListView _listView;
    private Workspace _workspace;

    public ItemListViewHandler(ListView listView)
    {
        _listView = listView;
        _listView.Loaded += OnListViewLoaded;
        _listView.MouseDoubleClick += OnListViewDoubleClick;
        _listView.KeyDown += OnListKeyDown;
    }

    private void SetupColumns()
    {
        if(_listView.View is not GridView gridView) return;

        var totalWidth = _listView.ActualWidth;
        gridView.AllowsColumnReorder = true;
        gridView.Columns.Clear();
        gridView.Columns.Add(new GridViewColumn()
        {
            Header = "ID",
            DisplayMemberBinding = new Binding("Id"),
            Width = totalWidth * 0.1
        });
        gridView.Columns.Add(new GridViewColumn()
        {
            Header = "Name",
            DisplayMemberBinding = new Binding("Name"),
            Width = totalWidth * 0.7,
        });
        gridView.Columns.Add(new GridViewColumn()
        {
            Header = "Type",
            DisplayMemberBinding = new Binding("Type"),
            Width = totalWidth * 0.2
        });
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

    private void OnUpdated()
    {
        FillWorkspaceItems();       
    }

    private void FillWorkspaceItems()
    {
        _listView.ItemsSource = null;
        _listView.ItemsSource = _workspace.Items;
    }
    
    private void OnListViewLoaded(object sender, RoutedEventArgs e)
    {
        _listView.Loaded -= OnListViewLoaded;
        SetupColumns();
    }
    
    private void OnListViewDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is ListView listView && listView.SelectedItem is Item item)
        {
            var itemWindow = new ItemWindow();
            itemWindow.ShowAsEditItem(item);
        }
    }

    private void OnListKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete)
        {
            var selectedItems = _listView.SelectedItems.Cast<Item>().ToList();
            if (selectedItems.Any())
            {
                var result = MessageBox.Show(
                    $"Are you sure want to delete {selectedItems.Count} items?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var item in selectedItems)
                    {
                        _workspace.RemoveItem(item);
                    }
                }
            }
        }
    }
}