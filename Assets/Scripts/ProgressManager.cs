using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
	public List<ItemProgress> VegetablesProgress;

	public int currentPoints;

	public int numberOfVegetablesCut;

	public bool allItemsFilled;

	public void OnVegetableCut(string vegetable_name)
	{
		IncreasePoints();
		if (!allItemsFilled)
		{
			allItemsFilled = true;
			foreach (ItemProgress item in VegetablesProgress)
			{
				if (vegetable_name.Contains(item.vegetable_name))
				{
					item.UpdateProgress();
				}
				if (!item.isFinished)
				{
					allItemsFilled = false;
				}
			}
			if (allItemsFilled)
			{
				Object.FindObjectOfType<LevelSpawner>().isOnLastSpawn = true;
			}
		}
	}

	private void IncreasePoints()
	{
		currentPoints += 2 * int.Parse(UIManager.UI.SCORE.ComboText.text[1].ToString());
		UIManager.UI.SCORE.UpdateScore(currentPoints);
		GameSystem.Sytem.LEVEL.SetSpeed(currentPoints);
	}

	public void Reset()
	{
		allItemsFilled = false;
		currentPoints = 0;
		numberOfVegetablesCut = 0;
	}

	public void GenerateItemProgress()
	{
		List<Vegetable> itemProgressGoalList = UnityEngine.Object.FindObjectOfType<VegetablesList>().ItemProgressGoalList;
		for (int i = 0; i < 3; i++)
		{
			VegetablesProgress[i].vegetable_name = itemProgressGoalList[i].vegetable.name;
			VegetablesProgress[i].Progress_Bar.GetComponent<Image>().color = itemProgressGoalList[i].Color;
			VegetablesProgress[i].Progress_Bar.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 100f);
			VegetablesProgress[i].Vegetable_Image.GetComponent<Image>().sprite = itemProgressGoalList[i].icon;
			VegetablesProgress[i].isFinished = false;
			VegetablesProgress[i].Sucess_Image.SetActive(value: false);
			VegetablesProgress[i].Progress_Bar.SetActive(value: true);
			VegetablesProgress[i].Progress_Background.SetActive(value: true);
			VegetablesProgress[i].Vegetable_Image.SetActive(value: true);
			VegetablesProgress[i].Vegetable_Progress = 0f;
		}
	}
}
