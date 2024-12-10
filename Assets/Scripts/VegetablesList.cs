using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VegetablesList : MonoBehaviour
{
	public List<Vegetable> Vegetables;

	public List<Vegetable> CurrentVegetablesInLevel;

	public List<Vegetable> ItemProgressGoalList;

	public List<Vegetable> VegetableSpawnList;

	public Vegetable returnOverlapItem(string VegeName)
	{
		foreach (Vegetable vegetable in Vegetables)
		{
			if (vegetable.vegetable.name == VegeName)
			{
				return vegetable;
			}
		}
		return null;
	}

	private void GenerateItemProgressionList()
	{
		ItemProgressGoalList = new List<Vegetable>();
		for (int i = 0; i < LevelProgression.System.currentVegetableProgression; i++)
		{
			ItemProgressGoalList.Add(Vegetables[i]);
		}
		System.Random r = new System.Random();
		ItemProgressGoalList = (from x in ItemProgressGoalList
			orderby r.Next()
			select x).ToList();
		if (ItemProgressGoalList.Count > 3)
		{
			ItemProgressGoalList.RemoveRange(2, ItemProgressGoalList.Count - 3);
		}
	}

	public List<Vegetable> GenerateSpawnList()
	{
		GenerateItemProgressionList();
		List<Vegetable> list = new List<Vegetable>();
		list = ItemProgressGoalList;
		if (Vegetables.Count == 3)
		{
			return list;
		}
		Vegetable item = randomVegetable();
		list.Add(item);
		return list;
	}

	private Vegetable randomVegetable()
	{
		bool flag = true;
		Vegetable vegetable;
		do
		{
			vegetable = Vegetables[UnityEngine.Random.Range(0, Vegetables.Count - 1)];
		}
		while (ItemProgressGoalList.Contains(vegetable));
		return vegetable;
	}
}
