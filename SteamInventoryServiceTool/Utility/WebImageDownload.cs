using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SteamInventoryServiceTool.Utility;

public static class WebImageDownload
{
    private static Dictionary<string, BitmapImage?> _cachedImageDictionary = new Dictionary<string, BitmapImage?>();
    
    public static async Task<BitmapImage> Get(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return null;

        if (_cachedImageDictionary.TryGetValue(imageUrl, out var image) && image != null)
        {
            return image;
        }
        
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
                _cachedImageDictionary[imageUrl] = bitmapImage;
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