using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectRandomizer : MonoBehaviour
{
	public List<Vegetable> Vegetables;

	public List<GameObject> Obstacles;

	public GameObject[] vegetable_varitation;

	public GameObject[] obstacle_variation;

	public int[] Full_Level;

	private static UnityEngine.Random.State seedGenerator;

	private static int seedGeneratorSeed = 1337;

	private static bool seedGeneratorInitialized = false;

	public void FillVegetablesList()
	{
		Vegetables = UnityEngine.Object.FindObjectOfType<VegetablesList>().GenerateSpawnList();
		UnityEngine.Object.FindObjectOfType<ProgressManager>().GenerateItemProgress();
	}

	private float BiasFunction(float x, float bias)
	{
		float num = Mathf.Pow(1f - bias, 3f);
		return x * num / (x * num - x + 2f) * 2f;
	}

	public void GenerateNewLevel(int length)
	{
		Full_Level = new int[length];
		float obstacleSpawnRate = LevelProgression.System.ObstacleSpawnRate;
		int num = 0;
		for (float num2 = 0f; num2 <= 2f; num2 += 0.1f)
		{
			Full_Level[num] = Mathf.FloorToInt(BiasFunction(num2, obstacleSpawnRate));
			num++;
		}
		System.Random r = new System.Random();
		Full_Level = (from x in Full_Level
			orderby r.Next()
			select x).ToArray();
	}

	public GameObject[] GenerateVegetableVariation(int length)
	{
		vegetable_varitation = new GameObject[length];
		for (int i = 0; i < length; i++)
		{
			vegetable_varitation[i] = Vegetables[GenerateSeed(0, Vegetables.Count - 1)].vegetable.gameObject;
		}
		return vegetable_varitation;
	}

	public GameObject GenerateRandomVegetable()
	{
		return Vegetables[GenerateSeed(0, Vegetables.Count - 1)].vegetable.gameObject;
	}

	public GameObject[] GenerateObstacleVariation(int length)
	{
		obstacle_variation = new GameObject[length];
		for (int i = 0; i < length; i++)
		{
			if (i == 0 || i == length - 1)
			{
				obstacle_variation[i] = Obstacles[GenerateSeed(0, LevelProgression.System.currentObstacleProgression)];
			}
			else
			{
				obstacle_variation[i] = Vegetables[GenerateSeed(0, Vegetables.Count - 1)].vegetable.gameObject;
			}
		}
		return obstacle_variation;
	}

	public static int GenerateSeed(int minValue, int maxValue)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		if (!seedGeneratorInitialized)
		{
			UnityEngine.Random.InitState(seedGeneratorSeed);
			seedGenerator = UnityEngine.Random.state;
			seedGeneratorInitialized = true;
		}
		UnityEngine.Random.state = seedGenerator;
		int result = UnityEngine.Random.Range(minValue, maxValue + 1);
		seedGenerator = UnityEngine.Random.state;
		UnityEngine.Random.state = state;
		return result;
	}
}
