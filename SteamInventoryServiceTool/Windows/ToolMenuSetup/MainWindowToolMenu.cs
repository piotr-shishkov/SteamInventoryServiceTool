using SteamInventoryServiceTool.Utility;
using System.Windows.Controls;

namespace SteamInventoryServiceTool.Windows.ToolMenuSetup
{
    internal class MainWindowToolMenu
    {
        private Menu _menuElement;
        private MainWindow _window;
        private ToolMenu _toolMenu;
        private Workspace.Workspace _activeWorkspace;

        public MainWindowToolMenu(MainWindow window, Menu menuElement)
        {
            _menuElement = menuElement;
            _window = window;
            SetupToolMenu(menuElement);
        }

        private void SetupToolMenu(Menu menu)
        {
            _toolMenu = new ToolMenu(menu, true);
            _toolMenu.AddItem("Workspace");
            _toolMenu.AddItem("Open", "Workspace");
            _toolMenu.AddItem("New", "Workspace");
            _toolMenu.AddItem("Create Copy", "Workspace");
            _toolMenu.AddSeparator("Workspace");
            _toolMenu.AddItem("Save", "Workspace");
            _toolMenu.AddItem("Import", "Workspace");
            _toolMenu.AddItem("JSON", "Workspace/Import");
            _toolMenu.AddItem("Export", "Workspace");
            _toolMenu.AddItem("JSON", "Workspace/Export");

            _toolMenu.AddItem("Items");
            _toolMenu.AddItem("New", "Items");
            _toolMenu.AddItem("Clear All", "Items");
            _toolMenu.AddSeparator("Items");
            _toolMenu.AddItem("Tags Control", "Items");

            _toolMenu.AddItem("Help");
            _toolMenu.AddItem("Steamworks Documentation", "Help");
            _toolMenu.AddSeparator("Help");
            _toolMenu.AddItem("About S.I.S.T.", "Help");
            _toolMenu.AddItem("Github Page", "Help");
        }

        public void SetWorkspace(Workspace.Workspace workspace)
        {
            _activeWorkspace = workspace;
        }
    }
}
