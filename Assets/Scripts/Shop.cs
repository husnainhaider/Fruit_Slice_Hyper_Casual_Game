using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Shop
{
	public List<ShopItem> KnivesItemList;

	public void InitKnivesItemList()
	{
		if (KnivesItemList.Count > 0)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("ShopRow");
		int num = 1;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				KnivesItemList.Add(array[i].transform.GetChild(j).GetComponent<ShopItem>());
				array[i].transform.GetChild(j).GetComponent<ShopItem>().index = num;
				num++;
			}
		}
	}

	public void UnlockRandom()
	{
		Player pLAYER = GameSystem.Sytem.PLAYER;
		if (pLAYER.UnlockedKnives.Count != 9 && HaveCoins(500))
		{
			SoundManager.SOUND.UnlockRandom.Play();
			SoundManager.SOUND.Purchase.Play();
			int num = 0;
			do
			{
				num = UnityEngine.Random.Range(1, 10);
			}
			while (GameSystem.Sytem.PLAYER.UnlockedKnives.Contains(num));
			KnivesItemList[num - 1].Unlock();
			KnivesItemList[num - 1].Select();
			GameSystem.Sytem.SetKnife(num);
			pLAYER.AddUnlockedItemToList(num);
		}
	}

	private bool HaveCoins(int price)
	{
		if (GameSystem.Sytem.PLAYER.coins >= price)
		{
			GameSystem.Sytem.PLAYER.SetCoins(-price);
			UIManager.UI.SHOP.UpdateCoinsUI();
			return true;
		}
		return false;
	}
}
