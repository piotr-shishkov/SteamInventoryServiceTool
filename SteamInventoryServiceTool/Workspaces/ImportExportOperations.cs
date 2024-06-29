using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Utility;

namespace SteamInventoryServiceTool.Workspaces;

public static class ImportExportOperations
{
    public static void ExportItems(Workspace workspace)
    {
        try
        {
            var json = JsonConvert.SerializeObject(new ItemDefinitions(workspace), JsonUtility.GetJsonSettings());
            var extension = Constants.EXTENSION_JSON;
            var fileName = $"SteamItems-{workspace.AppId}-{workspace.Name}{extension}";
            var dialog = new SaveFileDialog
            {
                Filter = $"JSON files (*{extension})|*{extension}|All files (*.*)|*.*",
                DefaultExt = extension,
                Title = "Export Item Definitions",
                FileName = fileName
            };

            if (dialog.ShowDialog() == true)
            {
                var filePath = dialog.FileName;
                File.WriteAllText(filePath, json);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    public static void ImportItems(Workspace workspace)
    {
        try
        {
            var extension = Constants.EXTENSION_JSON;
            var dialog = new OpenFileDialog()
            {
                Filter = $"JSON files (*{extension})|*{extension}|All files (*.*)|*.*",
                DefaultExt = extension,
                Title = "Import Item Definitions"
            };

            if (dialog.ShowDialog() == true)
            {
                var filePath = dialog.FileName;
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("File not found", filePath);

                var json = File.ReadAllText(filePath);
                var itemDefinitions = JsonConvert.DeserializeObject<ItemDefinitions>(json, JsonUtility.GetJsonSettings());
                workspace.Items = itemDefinitions.Items.ToList();
                workspace.Save();
                workspace.Update();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}