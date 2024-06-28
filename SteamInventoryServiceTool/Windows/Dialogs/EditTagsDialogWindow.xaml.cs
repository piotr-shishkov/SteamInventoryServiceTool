using System.Linq;
using System.Windows;
using SteamInventoryServiceTool.Windows.ListViews;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.Dialogs;

public partial class EditTagsDialogWindow : Window
{
    private WorkspaceManager _workspaceManager;
    private TagsListViewHandler _tagsListHandler;
    
    public EditTagsDialogWindow()
    {
        InitializeComponent();
        _workspaceManager = WorkspaceManager.Instance;
        
        _tagsListHandler = new TagsListViewHandler(TagsListView);
        _tagsListHandler.SetWorkspace(_workspaceManager.ActiveWorkspace);
        
        AddTagButton.Click += AddTag;
        RemoveTagButton.Click += RemoveTag;
        CloseButton.Click += CloseDialog;
    }

    private void AddTag(object sender, RoutedEventArgs e)
    {
        var tag = NewTagTextBox.Text;
        var tagValue = NewTagValueTextBox.Text;
        if (string.IsNullOrWhiteSpace(tag))
        {
            MessageBox.Show("Can't add empty tag", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (_workspaceManager.ActiveWorkspace.Tags.Any(x => x.Key == tag && x.Value.Contains(tagValue)))
        {
            MessageBox.Show("This tag already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        _workspaceManager.ActiveWorkspace.AddTag(tag, tagValue);
        NewTagValueTextBox.Text = string.Empty;
    }

    private void RemoveTag(object sender, RoutedEventArgs e)
    {
        var tag = NewTagTextBox.Text;
        var tagValue = NewTagValueTextBox.Text;
        if (string.IsNullOrWhiteSpace(tag))
        {
            MessageBox.Show("Can't remove empty tag", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        if (!_workspaceManager.ActiveWorkspace.Tags.Any(x => x.Key == tag && x.Value.Contains(tagValue)))
        {
            MessageBox.Show("This tag doesn't exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        _workspaceManager.ActiveWorkspace.RemoveTag(tag, tagValue);
        NewTagValueTextBox.Text = string.Empty;
    }

    private void CloseDialog(object sender, RoutedEventArgs e)
    {
        Close();
    }
}