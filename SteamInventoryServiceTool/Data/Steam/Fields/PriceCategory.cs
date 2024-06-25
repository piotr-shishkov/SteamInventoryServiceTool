using Newtonsoft.Json;
using System;

namespace SteamInventoryServiceTool.Data.Steam.Fields
{
    public class PriceCategory
    {
        public PriceCategories Category { get; set; }

        public PriceCategory(PriceCategories category) 
        {
            Category = category;
        }
    }

    public class PriceCategoryJsonConverter : JsonConverter<PriceCategory>
    {
        public override PriceCategory? ReadJson(JsonReader reader, Type objectType, PriceCategory? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonString = reader.Value as string;
            if (jsonString == null)
                return new PriceCategory(PriceCategories.None);
            else
                return new PriceCategory((PriceCategories)Enum.Parse(typeof(PriceCategories), jsonString));
        }

        public override void WriteJson(JsonWriter writer, PriceCategory? value, JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }
            var enumValue = value.Category;
            if(enumValue == PriceCategories.None) 
                writer.WriteNull();
            else
                writer.WriteValue(enumValue.ToString());
        }
    }

    public enum PriceCategories
    {
        None,
        VLV25,
        VLV50,
        VLV75,
        VLV100,
        VLV150,
        VLV200,
        VLV250,
        VLV300,
        VLV350,
        VLV400,
        VLV450,
        VLV500,
        VLV550,
        VLV600,
        VLV650,
        VLV700,
        VLV750,
        VLV800,
        VLV850,
        VLV900,
        VLV950,
        VLV1000,
        VLV1100,
        VLV1200,
        VLV1300,
        VLV1400,
        VLV1500,
        VLV1600,
        VLV1700,
        VLV1800,
        VLV1900,
        VLV2000,
        VLV2500,
        VLV3000,
        VLV3500,
        VLV4000,
        VLV4500,
        VLV5000,
        VLV6000,
        VLV7000,
        VLV8000,
        VLV9000,
        VLV10000
    }
}
