using SteamInventoryServiceTool.Data.Steam.Misc;

namespace SteamInventoryServiceTool.Data.Common;

public struct BundleListViewElement
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemCount Count { get; set; }
    public string IconUrl { get; set; }
}