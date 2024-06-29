using System;
using System.Diagnostics;
using System.Windows;

namespace SteamInventoryServiceTool.Utility;

public static class WebUtility
{
    public static void OpenUrl(string url)
    {
        try
        {
            var processInfo = new ProcessStartInfo(url);
            processInfo.UseShellExecute = true;
            Process.Start(processInfo);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Unable to open link: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }   
    }

    public static void OpenGithub()
    {
        OpenUrl(Constants.GITHUB_PAGE_LINK);
    }
    
    public static void OpenDocumentation()
    {
        OpenUrl(Constants.STEAMWORKS_DOCUMENTATION_LINK);
    }
}