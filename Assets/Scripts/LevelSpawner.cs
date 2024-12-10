using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
	public ObjectRandomizer LevelRandomizer;

	public GameObject Pillow;

	public float TimeInterval;

	private float AdditionalInterval;

	public int LevelLength = 20;

	public VegetablesList vegetables;

	public int vegetablesSpawned;

	private int currentIndex;

	private int variationIndex;

	public bool isOnLastSpawn;

	public bool isSpawning;

	private GameObject[] currentVariation;

	private Transform t;

	public void Start()
	{
		if (!isSpawning)
		{
			isSpawning = true;
			variationIndex = 0;
			vegetablesSpawned = 0;
			isOnLastSpawn = false;
			currentIndex = 0;
			LevelRandomizer.FillVegetablesList();
			LevelRandomizer.GenerateNewLevel(LevelLength);
			GenerateVariation();
			SpawnObject();
		}
	}

	private void GenerateVariation()
	{
		if (LevelRandomizer.Full_Level[currentIndex] == 0)
		{
			currentVariation = LevelRandomizer.GenerateVegetableVariation(UnityEngine.Random.Range(1, 4));
		}
		else
		{
			currentVariation = LevelRandomizer.GenerateObstacleVariation(UnityEngine.Random.Range(3, 4));
			if (LevelRandomizer.Full_Level[Mathf.Clamp(currentIndex, 0, LevelRandomizer.Full_Level.Length)] == 1)
			{
				currentVariation[0] = LevelRandomizer.GenerateRandomVegetable();
			}
		}
		if (isOnLastSpawn)
		{
			currentVariation[currentVariation.Length - 1] = Pillow;
			return;
		}
		currentIndex++;
		if (currentIndex == LevelLength)
		{
			LevelRandomizer.GenerateNewLevel(LevelLength);
			currentIndex = 0;
		}
	}

	public void SpawnObject()
	{
		if (variationIndex > currentVariation.Length - 1)
		{
			variationIndex = 0;
			GenerateVariation();
		}
		if (currentVariation[variationIndex].tag != "Obstacle")
		{
			vegetablesSpawned++;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(currentVariation[variationIndex], base.transform);
		gameObject.name += vegetablesSpawned;
		if (gameObject.gameObject.name.Contains("Pillow"))
		{
			isSpawning = false;
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + 600f, gameObject.transform.position.y, gameObject.transform.position.z);
			vegetablesSpawned--;
		}
		variationIndex++;
	}

	public void ResetAll()
	{
		isSpawning = false;
		foreach (Transform item in base.transform)
		{
			if (item.name != "Pillow")
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}
	}
}
