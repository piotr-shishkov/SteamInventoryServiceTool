using Newtonsoft.Json;
using SteamInventoryServiceTool.Data.Steam.Misc;
using System;
using System.Collections.Generic;

namespace SteamInventoryServiceTool.Data.Steam.Fields;

public class Promo
{
	public List<PromoRequirement> PromoRequirements { get; set; } = new List<PromoRequirement> { };
	
	public Promo() { }

	public Promo(string sourceString)
	{
		if (string.IsNullOrWhiteSpace(sourceString))
		{
			return;
		}

		// Separating all promos
		var separatingPromos = sourceString.Split(';');
		for (int i = 0; i < separatingPromos.Length; i++)
		{
			// Separate promo requirements by requirement and value
			var promo = separatingPromos[i];
			var split = promo.Split(':');

			// Check if promo manual
			if (split.Length == 1 && split[0] == "manual")
			{
				PromoRequirements = new List<PromoRequirement>()
				{
					new ManualPromoRequirement()
				};
				break;
			}

			var promoType = split[0];
			var promoValue = split[1];

			if(promoType == "owns")
			{
				PromoRequirements.Add(new OwnAppPromoRequirement(int.Parse(promoValue)));
			}
			else if(promoType == "ach")
			{
				PromoRequirements.Add(new OwnAchievementPromoRequirement(promoValue));
			}
			else if(promoType == "played")
			{
				var playedSplit = promoValue.Split("/");
				PromoRequirements.Add(new PlayerTimePromoRequirement(int.Parse(playedSplit[0]), int.Parse(playedSplit[1])));
			}
		}
	}

	public override string ToString()
	{
		var str = string.Empty;
		for (var i = 0; i < PromoRequirements.Count; i++)
		{
			var requirement = PromoRequirements[i];
			switch (requirement)
			{
				case OwnAppPromoRequirement ownAppPromo:
					str += $"owns:{ownAppPromo.AppId}";
					break;
				case OwnAchievementPromoRequirement ownAchievementPromo:
					str += $"ach:{ownAchievementPromo.AchievementId}";
					break;
				case PlayerTimePromoRequirement playerTimePromo:
					str += $"played:{playerTimePromo.AppId}/{playerTimePromo.MinutesRequired}";
					break;
				case ManualPromoRequirement manualPromo:
					str = "manual";
					break;
			}

			// Check if not last item
			if (i < PromoRequirements.Count - 1)
			{
				str += ";";
			}
		}
		return str;
	}
}

public class PromoJsonConverter : JsonConverter<Promo>
{
	public override Promo? ReadJson(JsonReader reader, Type objectType, Promo? existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		var jsonString = reader.Value as string;
		return new Promo(jsonString);
	}

	public override void WriteJson(JsonWriter writer, Promo? value, JsonSerializer serializer)
	{
		if (value == null)
		{
			writer.WriteNull();
			return;
		}

		var jsonString = value.ToString();
		writer.WriteValue(jsonString);
	}
}