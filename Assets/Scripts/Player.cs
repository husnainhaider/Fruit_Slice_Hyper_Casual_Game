using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public int coins;

	public int bestScore;

	public int currentLevel;

	public List<int> UnlockedKnives;

	public int currentKnife;

	public bool gotcoinreward;

	public void SetCoins(int c)
	{
		coins += c;
		PlayerPrefs.SetInt("coins", coins);
		PlayerPrefs.Save();
	}

	public void Awake()
	{
		UnlockedKnives = new List<int>
		{
			1
		};
		currentKnife = PlayerPrefs.GetInt("currentKnife");
		if (currentKnife == 0)
		{
			currentKnife = 1;
		}
		GameSystem.Sytem.SetKnife(currentKnife);
		LoadPlayerKnives();
		coins = PlayerPrefs.GetInt("coins");
		bestScore = PlayerPrefs.GetInt("bestscore");
	}

	internal void setBestScore(int currentScore)
	{
		bestScore = currentScore;
		PlayerPrefs.SetInt("bestscore", coins);
		PlayerPrefs.Save();
	}

	public void setCurrentKnife(int index)
	{
		currentKnife = index;
		PlayerPrefs.SetInt("currentKnife", index);
		PlayerPrefs.Save();
	}

	public void AddUnlockedItemToList(int item)
	{
		UnlockedKnives.Add(item);
		PlayerPrefs.SetInt("Knife_" + item, 1);
		PlayerPrefs.Save();
	}

	private void LoadPlayerKnives()
	{
		for (int i = 2; i <= 9; i++)
		{
			if (PlayerPrefs.GetInt("Knife_" + i) == 1)
			{
				UnlockedKnives.Add(i);
			}
		}
	}

	public void ClearKnifeCollection()
	{
		for (int i = 2; i <= 9; i++)
		{
			PlayerPrefs.SetInt("Knife_" + i, 0);
			PlayerPrefs.Save();
		}
	}

	public void ResetData()
	{
		setCurrentKnife(1);
		ClearKnifeCollection();
		PlayerPrefs.SetInt("coins", 0);
		setBestScore(0);
		PlayerPrefs.SetInt("Level", 1);
		PlayerPrefs.Save();
	}
}
