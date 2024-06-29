﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;
using SteamInventoryServiceTool.Utility;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.Dialogs;

public partial class AboutDialogWindow : Window
{
    public string AssemblyVersion { get; }
    
    private WorkspaceManager _workspaceManager;

    public AboutDialogWindow()
    {
        InitializeComponent();
        _workspaceManager = WorkspaceManager.Instance;
        
        CloseButton.Click += CloseWindow;

        GithubHyperLink.NavigateUri = new Uri(Constants.GITHUB_PAGE_LINK);
        GithubHyperLink.RequestNavigate += RequestNavigate;
        
        AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        DataContext = this;
    }
    
    private void CloseWindow(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        WebUtility.OpenUrl(e.Uri.AbsoluteUri);
    }
}