using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;

namespace SteamInventoryServiceTool.Utility;

public static class SteamUtility
{
    private static KeyValuePair<int, string?> _cachedAppName = new KeyValuePair<int, string?>();
    private static KeyValuePair<int, BitmapImage?> _cachedAppIcon = new KeyValuePair<int, BitmapImage?>();
    
    public static async Task<BitmapImage> GetAppIcon(int appId)
    {
        if (_cachedAppIcon.Key == appId && _cachedAppIcon.Value != null)
        {
            return _cachedAppIcon.Value;
        }
        var url = $"https://shared.cloudflare.steamstatic.com/store_item_assets/steam/apps/{appId}/hero_capsule.jpg";
        var bitmapImage = await WebImageDownload.Get(url);
        _cachedAppIcon = new KeyValuePair<int, BitmapImage?>(appId, bitmapImage);
        return bitmapImage;
    }

    public static async Task<string> GetAppName(int appId)
    {
        if (_cachedAppName.Key == appId && _cachedAppName.Value != null)
        {
            return _cachedAppName.Value;
        }
        
        var url = $"https://store.steampowered.com/api/appdetails?appids={appId}";

        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON response
                    var data = JObject.Parse(jsonResponse);
                    var success = (bool)data[appId.ToString()]["success"];

                    if (success)
                    {
                        var gameName = (string)data[appId.ToString()]["data"]["name"];
                        _cachedAppName = new KeyValuePair<int, string?>(appId, gameName);
                        return gameName;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
        }
        return null;
    }
}