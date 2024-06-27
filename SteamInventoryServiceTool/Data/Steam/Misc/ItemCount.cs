namespace SteamInventoryServiceTool.Data.Steam.Misc;

public struct ItemCount
{
	public int Value { get; set; }
	public bool PercentBased { get; set; }

	public void SetValue(int value)
	{
		PercentBased = false;
		Value = value;
	}

	public void SetPercent(int percent)
	{
		PercentBased = true;
		Value = percent;
	}
}