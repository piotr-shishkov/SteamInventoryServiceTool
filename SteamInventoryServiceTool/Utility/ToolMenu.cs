using System.Collections.Generic;
using System.Windows.Controls;

namespace SteamInventoryServiceTool.Utility
{
    internal class ToolMenu
    {
        private Menu _menu;

        private Dictionary<string, MenuItem> _itemPaths = new();

        public ToolMenu(Menu menu, bool clearItems)
        {
            _menu = menu;
            if (clearItems)
            {
                _menu.Items.Clear();
            }
        }

        public MenuItem AddItem(string name, MenuItem parent)
        {
            var item = new MenuItem();
            item.Header = name;
            if (parent != null)
            {
                parent.Items.Add(item);
            }
            else
            {
                _menu.Items.Add(item);
            }
            var path = GetMenuItemPath(item);
            _itemPaths.Add(path, item);
            return item;
        }

        public MenuItem AddItem(string name, string parentPath = null)
        {
            if (parentPath != null && TryGetItem(parentPath, out var parentItem))
            {
                return AddItem(name, parentItem);
            }
            else
            {
                return AddItem(name, parent: null);
            }
        }

        public Separator AddSeparator(string parentPath)
        {
            if (parentPath != null && TryGetItem(parentPath, out var parentItem))
            {
                var separator = new Separator();
                parentItem.Items.Add(separator);
                return separator;
            }
            return null;
        }

        public bool TryGetItem(string path, out MenuItem item)
        {
            item = null;
            if (_itemPaths.TryGetValue(path, out item))
            {
                return true;
            }
            return false;
        }

        private string GetMenuItemPath(MenuItem menuItem)
        {
            // Base case: if the parent is null or not a MenuItem, return the current item's header
            if (menuItem.Parent is MenuItem parentMenuItem)
            {
                return GetMenuItemPath(parentMenuItem) + "/" + menuItem.Header.ToString();
            }
            else if (menuItem.Parent is ContextMenu contextMenu)
            {
                return contextMenu.PlacementTarget.GetType().Name + "/" + menuItem.Header.ToString();
            }
            else
            {
                return menuItem.Header.ToString();
            }
        }
    }
}
