using Newtonsoft.Json;
using SteamInventoryServiceTool.Data.Steam.Misc;
using System;

namespace SteamInventoryServiceTool.Data.Steam.Fields;

public class Promo
{
	public PromoRequirement[] PromoRequirements { get; set; } = new PromoRequirement[] { };
}

public class PromoJsonConverter : JsonConverter<Promo>
{
	public override Promo? ReadJson(JsonReader reader, Type objectType, Promo? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		var jsonString = reader.Value as string;
		if (jsonString == null)
		{
			return new Promo();
		}

		// Separating all promos
		var separatingPromos = jsonString.Split(';');
		var promos = new PromoRequirement[separatingPromos.Length];
		for (int i = 0; i < separatingPromos.Length; i++)
		{
			// Separate promo requirements by requiremnt and value
			var promo = separatingPromos[i];
			var split = promo.Split(':');

			// Check if promo manual
			if (split.Length == 1 && split[0] == "manual")
			{
				promos = new PromoRequirement[]
				{
					new ManualPromoRequirement()
				};
				break;
			}

			var promoType = split[0];
			var promoValue = split[1];

			if(promoType == "owns")
			{
				promos[i] = new OwnAppPromoRequirement(int.Parse(promoValue));
			}
			else if(promoType == "ach")
			{
				promos[i] = new OwnAchievementPromoRequirement(promoValue);
			}
			else if(promoType == "played")
			{
				var playedSplit = promoValue.Split("/");
				promos[i] = new PlayerTimePromoRequirement(int.Parse(playedSplit[0]), int.Parse(playedSplit[1]));
			}
		}


		return new Promo()
		{
			PromoRequirements = promos
		};
	}

	public override void WriteJson(JsonWriter writer, Promo? value, JsonSerializer serializer)
	{
		var jsonString = string.Empty;
		if (value == null)
		{
			writer.WriteNull();
			return;
		}

		for (var i = 0; i < value.PromoRequirements.Length; i++)
		{
			var requirement = value.PromoRequirements[i];
			switch (requirement)
			{
				case OwnAppPromoRequirement ownAppPromo:
					jsonString += $"owns:{ownAppPromo.AppId}";
					break;
				case OwnAchievementPromoRequirement ownAchievementPromo:
					jsonString += $"ach:{ownAchievementPromo.AchievementId}";
					break;
				case PlayerTimePromoRequirement playerTimePromo:
					jsonString += $"played:{playerTimePromo.AppId}/{playerTimePromo.MinutesRequired}";
					break;
				case ManualPromoRequirement manualPromo:
					jsonString = "manual";
					break;
			}

			// Check if not last item
			if (i < value.PromoRequirements.Length - 1)
			{
				jsonString += ";";
			}
		}
		writer.WriteValue(jsonString);
	}
}