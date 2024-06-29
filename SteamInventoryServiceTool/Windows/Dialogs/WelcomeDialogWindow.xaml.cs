using System;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;
using SteamInventoryServiceTool.Utility;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.Dialogs;

public partial class WelcomeDialogWindow : Window
{
    public string AssemblyVersion { get; }

    private WorkspaceManager _workspaceManager;

    public WelcomeDialogWindow()
    {
        InitializeComponent();
        _workspaceManager = WorkspaceManager.Instance;

        OpenButton.Click += OpenWorkspace;
        NewButton.Click += NewWorkspace;
        ExitButton.Click += ExitTool;

        GithubHyperLink.NavigateUri = new Uri(Constants.GITHUB_PAGE_LINK);
        GithubHyperLink.RequestNavigate += RequestNavigate;
        
        AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        DataContext = this;
    }

    private void OpenWorkspace(object sender, RoutedEventArgs e)
    {
        var workspace = WorkspaceFileOperations.OpenWorkspace();
        if (workspace == null)
            return;
        _workspaceManager.OpenWorkspace(workspace);
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
        WebUtility.OpenUrl(e.Uri.AbsoluteUri);
    }
}