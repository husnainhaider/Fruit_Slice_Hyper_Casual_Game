using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
	private static GameSystem _system;

	public Level LEVEL;

	public Player PLAYER;

	public Shop SHOP;

	public Knife CurrentKnife;

	public List<GameObject> KnifeModels;

	public GAME_STATE GAME_STATE;

	public static GameSystem Sytem
	{
		get
		{
			if (_system == null)
			{
				_system = UnityEngine.Object.FindObjectOfType<GameSystem>();
				if (_system == null)
				{
					GameObject gameObject = new GameObject("GameSystem");
					_system = gameObject.AddComponent<GameSystem>();
				}
			}
			return _system;
		}
	}

	public void setGameState(string state)
	{
		Array values = Enum.GetValues(typeof(GAME_STATE));
		foreach (GAME_STATE item in values)
		{
			if (item.ToString() == state)
			{
				GAME_STATE = item;
			}
		}
	}

	public void setGameState(GAME_STATE state)
	{
		GAME_STATE = state;
	}

	private void Start()
	{
	}

	public void SetKnife(int index)
	{
		Sytem.CurrentKnife = KnifeModels[index - 1].GetComponent<Knife>();
	}

	private void Awake()
	{
		Screen.sleepTimeout = -1;
		if (PLAYER == null)
		{
			PLAYER = new Player();
			PLAYER.Awake();
			for (int i = 0; i < KnifeModels.Count; i++)
			{
				KnifeModels[i].SetActive(value: false);
				if (i + 1 == PLAYER.currentKnife)
				{
					KnifeModels[i].SetActive(value: true);
				}
			}
		}
		if (!Application.isEditor)
		{
		}
	}
}
