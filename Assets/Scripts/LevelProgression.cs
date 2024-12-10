using System.Collections.Generic;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
	private static LevelProgression _system;

	public int currentVegetableProgression;

	public int currentObstacleProgression;

	private int VegetableProgressionBase = 3;

	private int ObstacleProgressionBase = 1;

	public List<int> ObstacleProgressionLevels;

	public float ObstacleSpawnRate = 0f;

	public static LevelProgression System
	{
		get
		{
			if (_system == null)
			{
				_system = UnityEngine.Object.FindObjectOfType<LevelProgression>();
				if (_system == null)
				{
					GameObject gameObject = new GameObject("LevelProgression");
					_system = gameObject.AddComponent<LevelProgression>();
				}
			}
			return _system;
		}
	}

	private void Awake()
	{
		GameSystem.Sytem.LEVEL.LevelNumber = ((PlayerPrefs.GetInt("Level") == 0) ? 1 : PlayerPrefs.GetInt("Level"));
		UIManager.UI.SCORE.SetLevel();
		CalculateProgression();
	}

	public void CalculateProgression()
	{
		int levelNumber = GameSystem.Sytem.LEVEL.LevelNumber;
		currentVegetableProgression = Mathf.FloorToInt(levelNumber / 4) + VegetableProgressionBase;
		ObstacleSpawnRate = (float)Mathf.FloorToInt(levelNumber / 10) * -0.05f;
		currentObstacleProgression = 0;
		foreach (int obstacleProgressionLevel in ObstacleProgressionLevels)
		{
			if (levelNumber >= obstacleProgressionLevel)
			{
				currentObstacleProgression++;
			}
		}
		if (currentVegetableProgression > UnityEngine.Object.FindObjectOfType<VegetablesList>().Vegetables.Count)
		{
			currentVegetableProgression = UnityEngine.Object.FindObjectOfType<VegetablesList>().Vegetables.Count;
		}
	}
}
