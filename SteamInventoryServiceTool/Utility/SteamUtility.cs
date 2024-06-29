using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;

namespace SteamInventoryServiceTool.Utility;

public static class SteamUtility
{
    public static async Task<BitmapImage> GetAppIcon(int appId)
    {
        var url = $"https://shared.cloudflare.steamstatic.com/store_item_assets/steam/apps/{appId}/hero_capsule.jpg";
        return await WebImageDownload.Get(url);
    }

    public static async Task<string> GetAppName(int appId)
    {
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