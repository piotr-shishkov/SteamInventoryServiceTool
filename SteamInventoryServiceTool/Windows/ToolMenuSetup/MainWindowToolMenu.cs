using System;
using System.Windows;
using SteamInventoryServiceTool.Utility;
using System.Windows.Controls;
using System.Windows.Input;
using SteamInventoryServiceTool.Windows.Dialogs;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows.ToolMenuSetup;

internal class MainWindowToolMenu
{
    private Menu _menuElement;
    private MainWindow _window;
    private ToolMenu _toolMenu;

    private WorkspaceManager _workspaceManager;
    private Workspace _activeWorkspace;
        
    public MainWindowToolMenu(MainWindow window, Menu menuElement)
    {
        _workspaceManager = WorkspaceManager.Instance;
        _menuElement = menuElement;
        _window = window;
        SetupToolMenu(menuElement);
    }

    private void SetupToolMenu(Menu menu)
    {
        _toolMenu = new ToolMenu(menu, true);
            
        AddItem("Workspace");
        AddItem("Edit", "Workspace", EditWorkspace);
        _toolMenu.AddSeparator("Workspace");
        AddItem("Open", "Workspace", OpenWorkspace);
        AddItem("New", "Workspace", _workspaceManager.NewWorkspace);
        AddItem("Duplicate", "Workspace");
        _toolMenu.AddSeparator("Workspace");
        AddItem("Save", "Workspace", SaveWorkspace);

        AddItem("Items");
        AddItem("New", "Items", AddNewItem);
        AddItem("Clear All", "Items");
        _toolMenu.AddSeparator("Items");
        AddItem("Tags Control", "Items", OpenTagEditor);
        _toolMenu.AddSeparator("Items");
        AddItem("Import", "Items");
        AddItem("JSON", "Items/Import");
        AddItem("Export", "Items");
        AddItem("JSON", "Items/Export");

        AddItem("Help");
        AddItem("Steamworks Documentation", "Help", WebUtility.OpenDocumentation);
        _toolMenu.AddSeparator("Help");
        AddItem("About S.I.S.T.", "Help", ShowAboutDialog);
        AddItem("Github Page", "Help", WebUtility.OpenGithub);
    }

    private void EditWorkspace()
    {
        var editWindow = new EditWorkspaceDialogWindow(_activeWorkspace);
        editWindow.ShowDialog();
    }

    private MenuItem AddItem(string name, string path = null, Action action = null)
    {
        var item = _toolMenu.AddItem(name, path);

        if (action != null)
        {
            item.Click += (_, _) => action();
        }
        return item;
    }

    #region Workspace Controls

    public void SetWorkspace(Workspace workspace)
    {
        _activeWorkspace = workspace;
    }

    private void OpenWorkspace()
    {
        var workspace = WorkspaceFileOperations.OpenWorkspace();
        if(workspace == null) 
            return;
        _workspaceManager.OpenWorkspace(workspace);
    }

    private void SaveWorkspace()
    {
        _activeWorkspace.Save();
    }

    private void OpenTagEditor()
    {
        var editTagsWindow = new EditTagsDialogWindow();
        editTagsWindow.ShowDialog();
    }

    #endregion

    #region Item Controls

    private void AddNewItem()
    {
        var itemWindow = new ItemWindow();
        itemWindow.ShowAsNewItem();
    }

    #endregion

    #region Help Controls

    private void ShowAboutDialog()
    {
        new AboutDialogWindow().ShowDialog();
    }

    #endregion
}