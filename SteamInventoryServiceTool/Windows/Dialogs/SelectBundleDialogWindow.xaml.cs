using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Data.Steam.Misc;
using SteamInventoryServiceTool.Windows.ListViews;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.Dialogs;

public partial class SelectBundleDialogWindow : Window
{
    public Item Item { get; }

    private readonly Workspace _activeWorkspace;
    private SelectBundleItemListViewHandler _bundleListHandler;

    public SelectBundleDialogWindow(Item item)
    {
        InitializeComponent();

        Item = item;
        _activeWorkspace = WorkspaceManager.Instance.ActiveWorkspace;
        _bundleListHandler = new SelectBundleItemListViewHandler(ItemsListView, Item, _activeWorkspace);
        
        ItemsComboBox.SelectionChanged += ItemComboBoxSelectionChanged;
        AddItemButton.Click += AddItem;
        CloseButton.Click += CloseDialog;
        
        UpdateComboBox();
        ItemComboBoxSelectionChanged(null, null);
    }

    private void UpdateComboBox()
    {
        if(_activeWorkspace.Items.Count == 0)
            return;
        
        ItemsComboBox.ItemsSource = _activeWorkspace.Items;
        ItemsComboBox.DisplayMemberPath = "Name";
        ItemsComboBox.SelectedIndex = 0;
    }

    private void ItemComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        AddItemButton.IsEnabled = ItemsComboBox.Items.Count > 0 && ItemsComboBox.SelectedIndex >= 0;
    }

    private void AddItem(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(ItemCountTextBox.Text, out var itemCount))
        {
            MessageBox.Show("Item count value not valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        var bundleItem = _activeWorkspace.Items[ItemsComboBox.SelectedIndex];
        if (string.IsNullOrWhiteSpace(ItemsComboBox.Text) || bundleItem == null)
        {
            MessageBox.Show("Item cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (bundleItem.Id == Item.Id)
        {
            MessageBox.Show("Can't add self to bundle", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        if (Item.Bundle.Items.ContainsKey(bundleItem.Id))
            Item.Bundle.Items.Remove(bundleItem.Id);
        
        Item.Bundle.Items.Add(bundleItem.Id, new ItemCount(itemCount));
        _bundleListHandler.Update();
    }

    private void CloseDialog(object sender, RoutedEventArgs e)
    {
        Close();
    }
}