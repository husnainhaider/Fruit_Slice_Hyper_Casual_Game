using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopUI
{
	public GameObject MainCanvas;

	public Text Coins;

	public void Open()
	{
		MainCanvas.SetActive(value: true);
		Coins.text = GameSystem.Sytem.PLAYER.coins.ToString();
	}

	public void UpdateKnifeCollection()
	{
		foreach (int unlockedKnife in GameSystem.Sytem.PLAYER.UnlockedKnives)
		{
			GameSystem.Sytem.SHOP.KnivesItemList[unlockedKnife - 1].Unlock();
		}
		GameSystem.Sytem.SHOP.KnivesItemList[GameSystem.Sytem.PLAYER.currentKnife - 1].Select();
	}

	public void Close()
	{
		MainCanvas.SetActive(value: false);
	}

	public void UpdateCoinsUI()
	{
		Coins.text = GameSystem.Sytem.PLAYER.coins.ToString();
	}
}
