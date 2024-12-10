using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager _ui;

	public AudioSource Knife_Cut;

	public AudioSource Wood_Hit;

	public AudioSource Metal_Hit;

	public AudioSource Knife_Flip;

	public AudioSource LevelComplete;

	public AudioSource CookingSound;

	public AudioSource ShowStatsSound;

	public AudioSource UnlockRandom;

	public AudioSource ClaimButton;

	public AudioSource Purchase;

	public static SoundManager SOUND
	{
		get
		{
			if (_ui == null)
			{
				_ui = UnityEngine.Object.FindObjectOfType<SoundManager>();
				if (_ui == null)
				{
					GameObject gameObject = new GameObject("SoundManager");
					_ui = gameObject.AddComponent<SoundManager>();
				}
			}
			return _ui;
		}
	}
}
