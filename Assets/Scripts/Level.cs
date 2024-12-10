using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
	public float CurrentSpeed = 350f;

	public int LevelNumber;

	private float initialSpeed = 350f;

	public List<Speed> LevelSpeeds;

	public GameObject[] speedParticles;

	public bool isFinished;

	public int performance;

	public int CoinsEarned;

	public GameObject prefabPotCuts;

	public GameObject Pot;

	public int bonus;

	public int coinAdd;

	public event Action OnLevelFinished;

	public void UpdateLevelSpeed(Speed speed)
	{
		CurrentSpeed = speed.speed;
		SoundManager.SOUND.Knife_Cut.pitch = speed.sound_speed;
		UnityEngine.Object.FindObjectOfType<Knife>().KnifeAnimation.SetFloat("speed", speed.knife_speed);
		GameObject[] array = speedParticles;
		foreach (GameObject gameObject in array)
		{
			gameObject.GetComponent<ParticleSystem>().Play();
			//gameObject.GetComponent<ParticleSystem>().main.simulationSpeed = speed.sp_speed;
		}
	}

	public void SetSpeed(int currentScore)
	{
		Speed speed = null;
		foreach (Speed levelSpeed in LevelSpeeds)
		{
			if ((float)currentScore >= levelSpeed.score)
			{
				speed = levelSpeed;
			}
		}
		if (speed.speed != CurrentSpeed)
		{
			UpdateLevelSpeed(speed);
		}
	}

	public void OnFinished()
	{
		int currentPoints = UnityEngine.Object.FindObjectOfType<ProgressManager>().currentPoints;
		if (currentPoints > GameSystem.Sytem.PLAYER.bestScore)
		{
			GameSystem.Sytem.PLAYER.setBestScore(currentPoints);
		}
		CurrentSpeed = 0f;
		isFinished = true;
		GameSystem.Sytem.setGameState(GAME_STATE.COMPLETE);
		GameObject[] array = speedParticles;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(value: false);
		}
		UnityEngine.Object.FindObjectOfType<Knife>().KnifeAnimation.Play("Knife_Flip");
		CalculPerformance();
		this.OnLevelFinished?.Invoke();
	}

	public void OnFail()
	{
		int currentPoints = UnityEngine.Object.FindObjectOfType<ProgressManager>().currentPoints;
		CurrentSpeed = 0f;
		isFinished = true;
		GameSystem.Sytem.setGameState(GAME_STATE.FAIL);
		GameObject[] array = speedParticles;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(value: false);
		}
	}

	private void CalculPerformance()
	{
		int num = UnityEngine.Object.FindObjectOfType<LevelSpawner>().vegetablesSpawned / 3;
		int numberOfVegetablesCut = UnityEngine.Object.FindObjectOfType<ProgressManager>().numberOfVegetablesCut;
		if (numberOfVegetablesCut >= num)
		{
			performance = 1;
		}
		if (numberOfVegetablesCut >= num * 2)
		{
			performance = 2;
		}
		if (numberOfVegetablesCut >= num * 3)
		{
			performance = 3;
		}
	}

	public void CalculCoinsEarned()
	{
		CoinsEarned = 30 * bonus;
		GameSystem.Sytem.PLAYER.SetCoins(CoinsEarned);
	}

	public void NextLevel()
	{
		LevelNumber++;
		PlayerPrefs.SetInt("Level", LevelNumber);
		PlayerPrefs.Save();
		isFinished = false;
		SetSpeed(0);
		GameObject[] array = speedParticles;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(value: true);
		}
		UnityEngine.Object.FindObjectOfType<Knife>().Reset();
		UnityEngine.Object.FindObjectOfType<Knife>().KnifeAnimation.Play("Base_State");
		LevelProgression.System.CalculateProgression();
		SoundManager.SOUND.Knife_Cut.Play();
		SoundManager.SOUND.Knife_Cut.mute = true;
	}

	public void RewardNextLevel()
	{
		LevelNumber++;
		PlayerPrefs.SetInt("Level", LevelNumber);
		PlayerPrefs.Save();
	}

	public void RestartLevel()
	{
		isFinished = false;
		SetSpeed(0);
		GameObject[] array = speedParticles;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(value: true);
		}
		UnityEngine.Object.FindObjectOfType<Knife>().Reset();
		UnityEngine.Object.FindObjectOfType<Knife>().KnifeAnimation.Play("Base_State");
		SoundManager.SOUND.Knife_Cut.Play();
		SoundManager.SOUND.Knife_Cut.mute = true;
	}
}
