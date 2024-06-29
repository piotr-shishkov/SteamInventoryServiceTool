using System;
using SteamInventoryServiceTool.Windows.ToolMenuSetup;
using System.Windows;
using System.Windows.Controls;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Utility;
using SteamInventoryServiceTool.Windows.Dialogs;
using SteamInventoryServiceTool.Windows.ListViews;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private MainWindowToolMenu _toolMenu;
	private ItemPreviewPage _previewPage;
	private ItemListViewHandler _itemListHandler;
	private WorkspaceManager _workspaceManager;

	public MainWindow()
	{
		InitializeComponent();

		CreateItemPreview();
		SetupToolMenu();
		_itemListHandler = new ItemListViewHandler(ItemsListView);
		ItemsListView.SelectionChanged += OnItemListSelectionChanged;

		_workspaceManager = WorkspaceManager.Instance;
		_workspaceManager.WorkspaceChanged += OnWorkspaceChanged;
		OnWorkspaceChanged(_workspaceManager.ActiveWorkspace);

		Application.Current.MainWindow.Closed += MainWindowOnClosed;

		new WelcomeDialogWindow().ShowDialog();
	}

	private void MainWindowOnClosed(object? sender, EventArgs e)
	{
		_workspaceManager.WorkspaceChanged -= OnWorkspaceChanged;
	}

	private void CreateItemPreview()
	{
		_previewPage = new ItemPreviewPage();
		this.PreviewFrame.Navigate(_previewPage);
	}

	private void SetupToolMenu()
	{
		_toolMenu = new MainWindowToolMenu(this, ToolMenu);
	}

	private void OnItemListSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is ListView listView && listView.SelectedItem is Item item)
		{
			_previewPage.UpdateItem(item);
		}
	}

	private void OnWorkspaceChanged(Workspace workspace)
	{
		Title = $"{Constants.APP_NAME} | {workspace.Name} | APPID: {workspace.AppId}";
		_toolMenu.SetWorkspace(_workspaceManager.ActiveWorkspace);
		_itemListHandler.SetWorkspace(_workspaceManager.ActiveWorkspace);
	}
}