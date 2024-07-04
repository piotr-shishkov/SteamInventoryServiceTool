using Newtonsoft.Json;
using System;
using System.Linq;

namespace SteamInventoryServiceTool.Data.Steam.Fields;

public class PriceCategory
{
    public PriceCategories Category { get; set; }

    public PriceCategory(PriceCategories category) 
    {
        Category = category;
    }

    public override string ToString()
    {
        return PriceToString(Category);
    }

    public static string PriceToString(PriceCategories value)
    {
        return value switch
        {
            PriceCategories.None => "",
            PriceCategories.VLV25 => "$0.25",
            PriceCategories.VLV50 => "$0.49",
            PriceCategories.VLV75 => "$0.75",
            PriceCategories.VLV100 => "$0.99",
            PriceCategories.VLV150 => "$1.49",
            PriceCategories.VLV200 => "$1.99",
            PriceCategories.VLV250 => "$2.49",
            PriceCategories.VLV300 => "$2.99",
            PriceCategories.VLV350 => "$3.49",
            PriceCategories.VLV400 => "$3.99",
            PriceCategories.VLV450 => "$4.49",
            PriceCategories.VLV500 => "$4.99",
            PriceCategories.VLV550 => "$5.49",
            PriceCategories.VLV600 => "$5.99",
            PriceCategories.VLV650 => "$6.49",
            PriceCategories.VLV700 => "$6.99",
            PriceCategories.VLV750 => "$7.49",
            PriceCategories.VLV800 => "$7.99",
            PriceCategories.VLV850 => "$8.49",
            PriceCategories.VLV900 => "$8.99",
            PriceCategories.VLV950 => "$9.49",
            PriceCategories.VLV1000 => "$9.99",
            PriceCategories.VLV1100 => "$10.09",
            PriceCategories.VLV1200 => "$11.99",
            PriceCategories.VLV1300 => "$12.99",
            PriceCategories.VLV1400 => "$13.99",
            PriceCategories.VLV1500 => "$14.99",
            PriceCategories.VLV1600 => "$15.99",
            PriceCategories.VLV1700 => "$16.99",
            PriceCategories.VLV1800 => "$17.99",
            PriceCategories.VLV1900 => "$18.99",
            PriceCategories.VLV2000 => "$19.99",
            PriceCategories.VLV2500 => "$24.99",
            PriceCategories.VLV3000 => "$29.99",
            PriceCategories.VLV3500 => "$34.99",
            PriceCategories.VLV4000 => "$39.99",
            PriceCategories.VLV4500 => "$44.99",
            PriceCategories.VLV5000 => "$49.99",
            PriceCategories.VLV6000 => "$59.99",
            PriceCategories.VLV7000 => "$69.99",
            PriceCategories.VLV8000 => "$79.99",
            PriceCategories.VLV9000 => "$89.99",
            PriceCategories.VLV10000 => "$99.99",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public class PriceCategoryJsonConverter : JsonConverter<PriceCategory>
{
    public override PriceCategory? ReadJson(JsonReader reader, Type objectType, PriceCategory? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonString = reader.Value as string;
        if (jsonString == null)
            return new PriceCategory(PriceCategories.None);

        var split = jsonString.Split(';');
        if (split.Length > 0)
        {
            var price = split.LastOrDefault(x => x.Contains("VLV"));
            if(price != null)
                return new PriceCategory((PriceCategories)Enum.Parse(typeof(PriceCategories), price));
        }
        return new PriceCategory(PriceCategories.None);
    }

    public override void WriteJson(JsonWriter writer, PriceCategory? value, JsonSerializer serializer)
    {
        if(value == null)
        {
            writer.WriteNull();
            return;
        }
        var enumValue = value.Category;
        if (enumValue == PriceCategories.None)
            writer.WriteNull();
        else
            writer.WriteValue($"1;{enumValue.ToString()}");
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