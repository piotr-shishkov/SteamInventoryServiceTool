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
        OpenUrl(Constants.Links.GITHUB_PAGE);
    }
    
    public static void OpenDocumentation()
    {
        OpenUrl(Constants.Links.STEAMWORKS_DOCUMENTATION);
    }
    
    public static void OpenDropTimeDocumentation()
    {
        OpenUrl(Constants.Links.STEAMWORKS_DROPTIME_HElP);
    }
}