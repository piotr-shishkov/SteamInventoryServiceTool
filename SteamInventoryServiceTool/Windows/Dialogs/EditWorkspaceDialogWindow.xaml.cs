using System.Windows;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.Dialogs;

public partial class EditWorkspaceDialogWindow : Window
{
    private Workspace _targetWorkspace;
    
    public EditWorkspaceDialogWindow(Workspace workspace)
    {
        InitializeComponent();
        EditWorkspace(workspace);
    }

    private void EditWorkspace(Workspace workspace)
    {
        _targetWorkspace = workspace;
        AppIdTextBox.Text = _targetWorkspace.AppId.ToString();
        WorkspaceNameTextBox.Text = _targetWorkspace.Name;
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(AppIdTextBox.Text, out var id))
        {
            MessageBox.Show("App ID must be numeral value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        var workspaceName = WorkspaceNameTextBox.Text;
        _targetWorkspace.AppId = id;
        _targetWorkspace.Name = workspaceName;
        _targetWorkspace.Update();
        DialogResult = true;
        Close();
    }
}