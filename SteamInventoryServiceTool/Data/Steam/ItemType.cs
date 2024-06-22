using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryServiceTool.Data.Steam
{
    public enum ItemType
    {
		[JsonProperty("item")]
        Item = 0,
		[JsonProperty("bundle")]
		Bundle = 1,
		[JsonProperty("generator")]
		Generator = 2,
		[JsonProperty("playtimegenerator")]
		PlayTimeGenerator = 3,
		[JsonProperty("tag_generator")]
		TagGenerator = 4,
    }
}
