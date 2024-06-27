using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SteamInventoryServiceTool.Utility;

public static class WebImageDownload
{
    public static async Task<BitmapImage> Get(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return null;
        
        using (var client = new HttpClient())
        {
            try
            {
                var response = await client.GetAsync(imageUrl);
                var imageStream = await response.Content.ReadAsStreamAsync();

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = imageStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}