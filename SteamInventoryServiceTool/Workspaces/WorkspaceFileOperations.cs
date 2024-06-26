using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;
using SteamInventoryServiceTool.Utility;

namespace SteamInventoryServiceTool.Workspaces;

public static class WorkspaceFileOperations
{
    public static void SaveWorkspace(Workspace workspace, string filePath = null)
    {
        try
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                workspace.FilePath = filePath;
            }
            var json = JsonConvert.SerializeObject(workspace, Formatting.Indented);
            File.WriteAllText(workspace.FilePath, json);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public static void SaveWorkspaceWithDialog(Workspace workspace)
    {
        try
        {
            var extension = Constants.EXTENSION_WORKSPACE;
            var dialog = new SaveFileDialog
            {
                Filter = $"Workspace files (*{extension})|*{extension}|All files (*.*)|*.*",
                DefaultExt = extension,
                Title = "Save Workspace"
            };

            if (dialog.ShowDialog() == true)
            {
                var filePath = dialog.FileName;
                SaveWorkspace(workspace, filePath);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"{e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public static Workspace LoadWorkspace(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found", filePath);

            var json = File.ReadAllText(filePath);
            var workspace = JsonConvert.DeserializeObject<Workspace>(json);
            workspace.FilePath = filePath;
            return workspace;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        return null;
    }

    public static Workspace OpenWorkspace()
    {
        try
        {
            var extension = Constants.EXTENSION_WORKSPACE;
            var dialog = new OpenFileDialog()
            {
                Filter = $"Workspace files (*{extension})|*{extension}|All files (*.*)|*.*",
                DefaultExt = extension,
                Title = "Open Workspace"
            };

            if (dialog.ShowDialog() == true)
            {
                var filePath = dialog.FileName;
                return LoadWorkspace(filePath);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"{e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        return null;
    }
}