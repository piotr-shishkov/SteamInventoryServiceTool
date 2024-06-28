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
        CloseButton.Click += CloseDialog;
    }

    private void AddTag(object sender, RoutedEventArgs e)
    {
        var value = NewTagTextBox.Text;
        if (string.IsNullOrWhiteSpace(value))
        {
            MessageBox.Show("Can't add empty tag", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (_workspaceManager.ActiveWorkspace.Tags.Contains(value))
        {
            MessageBox.Show("This tag already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        _workspaceManager.ActiveWorkspace.AddTag(value);
        NewTagTextBox.Text = string.Empty;
    }

    private void CloseDialog(object sender, RoutedEventArgs e)
    {
        Close();
    }
}