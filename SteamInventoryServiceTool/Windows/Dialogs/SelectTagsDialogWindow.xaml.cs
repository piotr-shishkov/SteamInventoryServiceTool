using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Windows.ListViews;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.Dialogs;

public partial class SelectTagsDialogWindow : Window
{
    public Item Item { get; private set; }
    
    private readonly Workspace _activeWorkspace;
    private SelectTagsListViewHandler _tagsListHandler;

    public SelectTagsDialogWindow(Item item)
    {
        InitializeComponent();

        Item = item;
        _activeWorkspace = WorkspaceManager.Instance.ActiveWorkspace;
        _tagsListHandler = new SelectTagsListViewHandler(TagsListView, item);
        
        AddTagButton.Click += AddTag;
        CloseButton.Click += CloseDialog;
        TagComboBox.SelectionChanged += OnTagSelectionChanged;
        
        UpdateComboBox();
    }

    private void UpdateComboBox()
    {
        if(_activeWorkspace.Tags.Count == 0)
            return;
        TagComboBox.ItemsSource = _activeWorkspace.Tags.Keys;
        TagComboBox.SelectedIndex = 0;
    }

    private void OnTagSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var key = _activeWorkspace.Tags.Keys.ElementAt(TagComboBox.SelectedIndex);
        if (string.IsNullOrWhiteSpace(key))
        {
            TagValueComboBox.ItemsSource = null;
            TagValueComboBox.Text = string.Empty;
            return;
        }
        TagValueComboBox.ItemsSource = _activeWorkspace.Tags[key];
        TagValueComboBox.SelectedIndex = 0;
    }

    private void AddTag(object sender, RoutedEventArgs e)
    {
        var tag = TagComboBox.Text;
        var tagValue = TagValueComboBox.Text;
        if (string.IsNullOrWhiteSpace(tag) || string.IsNullOrWhiteSpace(tagValue))
        {
            MessageBox.Show("Tag cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (Item.Tags.TagsDict.ContainsKey(tag))
            Item.Tags.TagsDict.Remove(tag);
        
        Item.Tags.TagsDict.Add(tag, tagValue);
        _tagsListHandler.Update();
    }

    private void CloseDialog(object sender, RoutedEventArgs e)
    {
        Close();
    }
}