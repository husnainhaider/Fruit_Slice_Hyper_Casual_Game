using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ScoreUI
{
	public GameObject ScoreUI_obj;

	public Text ComboText;

	public Text CurrentLevel;

	public Text CurrentScore;

	public Text Level_C_Score;

	public Text Level_C_BestScore;

	public GameObject ClaimButton;

	public Text Coins;

	public Image Combo_Progress_Bar;

	private Color BaseColor;

	public List<PlayerPerfomance> playerPerformancesList;

	public void Awake()
	{
		BaseColor = Combo_Progress_Bar.color;
	}

	public void Update()
	{
		if (GameSystem.Sytem.LEVEL.isFinished)
		{
			return;
		}
		if (GameSystem.Sytem.CurrentKnife.isCutting && GameSystem.Sytem.CurrentKnife.vegContact)
		{
			if (Combo_Progress_Bar.fillAmount < 0.64f)
			{
				Combo_Progress_Bar.fillAmount += 0.0018f;
				Combo_Progress_Bar.color = new Color(Combo_Progress_Bar.color.r + 0.007f, Combo_Progress_Bar.color.g - 0.001f, Combo_Progress_Bar.color.b);
				ComboText.color = new Color(ComboText.color.r + 0.007f, ComboText.color.g - 0.001f, ComboText.color.b);
			}
		}
		else if (Combo_Progress_Bar.fillAmount > 0f)
		{
			Combo_Progress_Bar.fillAmount -= (GameSystem.Sytem.CurrentKnife.isHit ? 0.004f : 0.001f);
			Combo_Progress_Bar.color = new Color(Combo_Progress_Bar.color.r - 0.007f, Combo_Progress_Bar.color.g + 0.001f, Combo_Progress_Bar.color.b);
			ComboText.color = new Color(ComboText.color.r - 0.007f, ComboText.color.g + 0.001f, ComboText.color.b);
		}
		if (Combo_Progress_Bar.fillAmount >= 0f && Combo_Progress_Bar.fillAmount <= 0.148f)
		{
			ComboText.text = "x1";
		}
		else if (Combo_Progress_Bar.fillAmount > 0.148f && Combo_Progress_Bar.fillAmount <= 0.285f)
		{
			ComboText.text = "x2";
		}
		else if (Combo_Progress_Bar.fillAmount > 0.285f && Combo_Progress_Bar.fillAmount <= 0.422f)
		{
			ComboText.text = "x3";
		}
		else if (Combo_Progress_Bar.fillAmount > 0.422f && Combo_Progress_Bar.fillAmount <= 0.504f)
		{
			ComboText.text = "x4";
		}
		else if (Combo_Progress_Bar.fillAmount > 0.504f && Combo_Progress_Bar.fillAmount <= 0.65f)
		{
			ComboText.text = "x5";
		}
	}

	public void UpdateScore(int score)
	{
		CurrentScore.text = score.ToString();
	}

	public void ResetCombo()
	{
		Combo_Progress_Bar.color = BaseColor;
		ComboText.color = BaseColor;
		Combo_Progress_Bar.fillAmount = 0f;
		ComboText.text = "x1";
	}

	public void PerformanceUI()
	{
		int performance = GameSystem.Sytem.LEVEL.performance;
		for (int i = 0; i < performance; i++)
		{
			playerPerformancesList[i].isGood(b: true);
		}
	}

	public void UpdateCoinsUI()
	{
		GameObject.Find("Coins_Txt").GetComponent<Text>().text = GameSystem.Sytem.PLAYER.coins.ToString();
	}

	public void SetLevel()
	{
		CurrentLevel.text = "Level " + GameSystem.Sytem.LEVEL.LevelNumber;
	}
}
