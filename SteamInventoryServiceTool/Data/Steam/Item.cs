using Newtonsoft.Json;
using SteamInventoryServiceTool.Data.Steam.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryServiceTool.Data.Steam
{
    [Serializable]
    public class Item
    {
        [JsonProperty("itemdefid")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("display_type")]
        public string DisplayType { get; set; }

        [JsonProperty("type")]
        public ItemType Type { get; set; }
        
        [JsonProperty("bundle")]
        public Bundle Bundle { get; set; }
        
        [JsonProperty("promo")]
        public Promo Promo { get; set; }

        // TODO: drop-start
        // TODO: exchange
        // TODO: price

        [JsonProperty("price_category")]
        public PriceCategory PriceCategory { get; set; }
        
        [JsonProperty("background_color")]
        public HexColor BackgroundColor { get; set; }
        
        [JsonProperty("name_color")]
        public HexColor NameColor { get; set; }
        
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }
        
        [JsonProperty("icon_url_large")]
        public string IconUrlLarge { get; set; }
        
        [JsonProperty("marketable")]
        public bool Marketable { get; set; }
        
        [JsonProperty("tradable")]
        public bool Tradable { get; set; }
        
        [JsonProperty("tags")]
        public Tags Tags { get; set; }

        // TODO: tag_generators
        // TODO: tag_generator_name
        // TODO: tag_generator_values
        // TODO: store_tags
        // TODO: store_images

        [JsonProperty("game_only")]
        public bool GameOnly { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("store_hidden")]
        public bool StoreHidden { get; set; }

        [JsonProperty("use_drop_limit")]
        public bool UseDropLimit { get; set; }

        [JsonProperty("drop_interval")]
        public int DropInterval { get; set; }

        [JsonProperty("use_drop_window")]
        public bool UseDropWindow { get; set; }

        [JsonProperty("drop_window")]
        public int DropWindow { get; set; }

        [JsonProperty("drop_max_per_window")]
        public int DropMaxPerWindow { get; set; }

        [JsonProperty("granted_manually")]
        public bool GrantedManually { get; set; }

        [JsonProperty("use_bundle_price")]
        public bool UseBundlePrice { get; set; }

        [JsonProperty("auto_stack")]
        public bool AutoStack { get; set; }

        public Item(int id)
        {
            Id = id;
        }
    }
}
