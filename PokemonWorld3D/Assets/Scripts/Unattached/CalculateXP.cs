using UnityEngine;
using System.Collections;

public class CalculateXP 
{
	public int CalculateCurrentXP(int level, Pokemon.LevelingRates lvlrate)
	{
		if(lvlrate == Pokemon.LevelingRates.ERRATIC)
		{
			return CalculateErraticCurrentXP(level);
		}
		if(lvlrate == Pokemon.LevelingRates.FAST)
		{
			return CalculateFastCurrentXP(level);
		}
		if(lvlrate == Pokemon.LevelingRates.FLUCTUATING)
		{
			return CalculateFluctuatingCurrentXP(level);
		}
		if(lvlrate == Pokemon.LevelingRates.MEDIUM_FAST)
		{
			return CalculateMediumFastCurrentXP(level);
		}
		if(lvlrate == Pokemon.LevelingRates.MEDIUM_SLOW)
		{
			return CalculateMediumSlowCurrentXP(level);
		}
		if(lvlrate == Pokemon.LevelingRates.SLOW)
		{
			return CalculateSlowCurrentXP(level);
		}
		return 0;
	}

	public int CalculateRequiredXP(int level, Pokemon.LevelingRates lvlrate)
	{
		if(lvlrate == Pokemon.LevelingRates.ERRATIC)
		{
			return CalculateErraticRequiredXP(level);
		}
		if(lvlrate == Pokemon.LevelingRates.FAST)
		{
			return CalculateFastRequiredXP(level);
		}
		if(lvlrate == Pokemon.LevelingRates.FLUCTUATING)
		{
			return CalculateFluctuatingRequiredXP(level);
		}
		if(lvlrate == Pokemon.LevelingRates.MEDIUM_FAST)
		{
			return CalculateMediumFastRequiredXP(level);
		}
		if(lvlrate == Pokemon.LevelingRates.MEDIUM_SLOW)
		{
			return CalculateMediumSlowRequiredXP(level);
		}
		if(lvlrate == Pokemon.LevelingRates.SLOW)
		{
			return CalculateSlowRequiredXP(level);
		}
		return 0;
	}
	

	public int CalculateErraticCurrentXP(int level)
	{
		if(level < 50)
		{
			return (int)((Mathf.Pow(level, 3.0f) * (100 - level)) / 50);
		}
		else if(level >= 50 && level < 68)
		{
			return (int)((Mathf.Pow(level, 3.0f) * (150 - level)) / 100);
		}
		else if(level >= 68 && level < 98)
		{
			return (int)((Mathf.Pow(level, 3.0f) * (1911 - (10 * level)) / 3) / 500);
		}
		else
		{
			return (int)((Mathf.Pow(level, 3.0f) * (160 - level)) / 100);
		}
	}

	public int CalculateErraticRequiredXP(int level)
	{
		if(level < 50)
		{
			return (int)((Mathf.Pow((level + 1), 3.0f) * (100 - (level + 1))) / 50);
		}
		else if(level >= 50 && level < 68)
		{
			return (int)((Mathf.Pow((level + 1), 3.0f) * (150 - (level + 1))) / 100);
		}
		else if(level >= 68 && level < 98)
		{
			return (int)((Mathf.Pow((level + 1), 3.0f) * (1911 - (10 * (level + 1))) / 3) / 500);
		}
		else
		{
			return (int)((Mathf.Pow((level + 1), 3.0f) * (160 - (level + 1))) / 100);
		}
	}

	public int CalculateFastCurrentXP(int level)
	{
		return (int)((4 * Mathf.Pow(level, 3.0f)) / 5);
	}

	public int CalculateFastRequiredXP(int level)
	{
		return (int)((4 * Mathf.Pow((level +1), 3.0f)) / 5);
	}

	public int CalculateMediumFastCurrentXP(int level)
	{
		return (int)(Mathf.Pow(level, 3.0f));
	}

	public int CalculateMediumFastRequiredXP(int level)
	{
		return (int)(Mathf.Pow((level + 1), 3.0f));
	}

	public int CalculateMediumSlowCurrentXP(int level)
	{	
		return (int)(1.2 * Mathf.Pow(level, 3.0f) - 15 * Mathf.Pow(level, 2.0f) + 100 * level - 140);
	}
	
	public int CalculateMediumSlowRequiredXP(int level)
	{	
		return (int)(1.2 * Mathf.Pow((level + 1), 3.0f) - 15 * Mathf.Pow((level + 1), 2.0f) + 100 * (level + 1) - 140);
	}

	public int CalculateSlowCurrentXP(int level)
	{
		return (int)((5 * Mathf.Pow(level, 3.0f)) / 4);
	}

	public int CalculateSlowRequiredXP(int level)
	{
		return (int)((5 * Mathf.Pow((level + 1), 3.0f)) / 4);
	}

	public int CalculateFluctuatingCurrentXP(int level)
	{
		if(level < 15)
		{
			return (int)(Mathf.Pow(level, 3.0f) * ((((level + 1) / 3) + 24) / 50));
		}
		else if(level >= 15 && level < 36)
		{
			return (int)(Mathf.Pow(level, 3.0f) * (((level + 14) / 50)));
		}
		else
		{
			return (int)(Mathf.Pow(level, 3.0f) * ((level / 2) + 32) / 50);
		}
	}

	public int CalculateFluctuatingRequiredXP(int level)
	{
		if(level < 15)
		{
			return (int)(Mathf.Pow((level + 1), 3.0f) * (((((level + 1) + 1) / 3)) + 24) / 50);
		}
		else if(level >= 15 && level < 36)
		{
			return (int)(Mathf.Pow((level + 1), 3.0f) * ((((level + 1) + 14) / 50)));
		}
		else
		{
			return (int)(Mathf.Pow((level + 1), 3.0f) * ((((level + 1) / 2) + 32) / 50));
		}
	}
}
