using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace SteamInventoryServiceTool.Workspace;

public static class WorkspaceFileOperations
{
    public static void SaveWorkspace(Workspace workspace, string filePath)
    {
        try
        {
            var json = JsonConvert.SerializeObject(workspace, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public static Workspace LoadWorkspace(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found", filePath);

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Workspace>(json);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        return null;
    }
}