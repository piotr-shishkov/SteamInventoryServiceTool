namespace SteamInventoryServiceTool.Data.Steam.Misc;

public enum PromoType
{
	OwnApp,
	OwnAchievement,
	PlayerTime,
	Manual
}

public abstract class PromoRequirement
{
	public abstract PromoType Type { get; }
}

/// <summary>
/// Check if player owns specified app
/// </summary>
public class OwnAppPromoRequirement : PromoRequirement
{
	public override PromoType Type => PromoType.OwnApp;
	public int AppId { get; set; }

	/// <summary>
	/// </summary>
	/// <param name="appId">Target App Id</param>
	public OwnAppPromoRequirement(int appId)
	{
		AppId = appId;
	}
}

/// <summary>
/// Check if player have specified achievemnt in current app
/// </summary>
public class OwnAchievementPromoRequirement : PromoRequirement
{
	public override PromoType Type => PromoType.OwnAchievement;
	public string AchievementId { get; set; }

	/// <summary>
	/// </summary>
	/// <param name="achievementId">Target achievement Id</param>
	public OwnAchievementPromoRequirement(string achievementId)
	{
		AchievementId = achievementId;
	}
}

/// <summary>
/// Check if player played at least specific time in specified app
/// </summary>
public class PlayerTimePromoRequirement : PromoRequirement
{
	public override PromoType Type => PromoType.PlayerTime;
	public int AppId { get; set; }
	public int MinutesRequired { get; set; }

	/// <summary>
	/// </summary>
	/// <param name="appId">Target App Id</param>
	/// <param name="minutesRequired">Minimum minutes required</param>
	public PlayerTimePromoRequirement(int appId, int minutesRequired)
	{
		AppId = appId;
		MinutesRequired = minutesRequired;
	}
}

/// <summary>
/// Used for timed drop events
/// </summary>
public class ManualPromoRequirement : PromoRequirement
{
	public override PromoType Type => PromoType.Manual;
}