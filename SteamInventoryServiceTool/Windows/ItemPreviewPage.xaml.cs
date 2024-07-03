using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using SteamInventoryServiceTool.Data.Steam;
using SteamInventoryServiceTool.Data.Steam.Fields;
using SteamInventoryServiceTool.Utility;
using SteamInventoryServiceTool.Workspaces;

namespace SteamInventoryServiceTool.Windows;

/// <summary>
/// Interaction logic for ItemPreviewPage.xaml
/// </summary>
public partial class ItemPreviewPage : Page
{
    public ItemPreviewPage()
    {
        InitializeComponent();
    }

    public async void UpdateItem(Item item)
    {
        ItemNameTextBlock.Text = item.Name;
        ItemTypeTextBlock.Text = item.DisplayType;
        ItemDescription.Text = item.Description;
        ItemTags.Text = FormatTags(item);
        ItemPriceLabel.Content = item.PriceCategory.ToString();

        ItemNameTextBlock.Foreground = new SolidColorBrush(item.NameColor.Color);
        ItemFrame.Stroke = new SolidColorBrush(item.NameColor.Color);
        ItemImage.Source = await WebImageDownload.Get(item.IconUrlLarge);
        GameLogoImage.Source = await SteamUtility.GetAppIcon(WorkspaceManager.Instance.ActiveWorkspace.AppId);
        GameNameTextBlock.Text = await SteamUtility.GetAppName(WorkspaceManager.Instance.ActiveWorkspace.AppId);
    }

    private string FormatTags(Item item)
    {
        var str = "";
        foreach (var tag in item.Tags.TagsDict)
        {
            str += $"{tag.Value}, ";
        }

        str += item.Tradable ? "Tradable" : "Not Tradable";
        str += ", ";
        str += item.Marketable ? "Marketable" : "Not Marketable";
        return str;
    }

#if DEBUG
    private void SetTestItem()
    {
        UpdateItem(new Item(1)
        {
            Name = "Test Item",
            DisplayType = "Common Item",
            Description = "Some interesting description about this item...",
            PriceCategory = new PriceCategory(PriceCategories.VLV1000),
            BackgroundColor = new HexColor(134, 80, 172),
            NameColor = new HexColor(134, 80, 172),
            IconUrlLarge = "https://community.akamai.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXU5A1PIYQh5hlcX0nvUOGsx8DdQBJjIAVHubSaIAlp1fb3ZjRG48u7lYuOhbmiZLmElGgIvJxzjLiYodWi3wGwrxE_MWmgI4SUJFc8Zl_R_VPqybvqm9bi6-x3KLPD/330x192?allow_animated=1",
            IconUrl = "https://community.akamai.steamstatic.com/economy/image/-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KU0Zwwo4NUX4oFJZEHLbXU5A1PIYQh5hlcX0nvUOGsx8DdQBJjIAVHubSaIAlp1fb3ZjRG48u7lYuOhbmiZLmElGgIvJxzjLiYodWi3wGwrxE_MWmgI4SUJFc8Zl_R_VPqybvqm9bi6-x3KLPD/330x192?allow_animated=1",
            Tags = new Tags()
            {
                TagsDict = new Dictionary<string, string>()
                {
                    {"Rarity", "Common"},
                    {"Stare", "Brand New"},
                    {"Origin", "Moldova"}
                }
            }
        });
    }
#endif
}