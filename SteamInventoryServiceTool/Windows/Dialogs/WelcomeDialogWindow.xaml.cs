using System.Windows;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.Dialogs;

public partial class WelcomeDialogWindow : Window
{
    private WorkspaceManager _workspaceManager;

    public WelcomeDialogWindow()
    {
        InitializeComponent();
        _workspaceManager = WorkspaceManager.Instance;

        OpenButton.Click += OpenWorkspace;
        NewButton.Click += NewWorkspace;
        ExitButton.Click += ExitTool;
    }

    private void OpenWorkspace(object sender, RoutedEventArgs e)
    {
        _workspaceManager.OpenWorkspace(WorkspaceFileOperations.OpenWorkspace());
        Close();
    }

    private void NewWorkspace(object sender, RoutedEventArgs e)
    {
        _workspaceManager.NewWorkspace();
        Close();
        new EditWorkspaceDialogWindow(_workspaceManager.ActiveWorkspace).ShowDialog();
    }

    private void ExitTool(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}