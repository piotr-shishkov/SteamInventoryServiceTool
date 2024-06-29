using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
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
        GithubHyperLink.RequestNavigate += RequestNavigate;
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

    private void RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        try
        {
            var url = e.Uri.AbsoluteUri;
            var processInfo = new ProcessStartInfo(url);
            processInfo.UseShellExecute = true;
            Process.Start(processInfo);
            e.Handled = true; 
        }
        catch (Exception ex)
        {
            MessageBox.Show("Unable to open link: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }   
    }
}