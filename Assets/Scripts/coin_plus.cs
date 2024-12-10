using UnityEngine;
using UnityEngine.UI;

public class coin_plus : MonoBehaviour
{
	public GameObject claimbonus;

	private void Start()
	{
	}

	private void Update()
	{
		GetComponent<Text>().text = "+" + GameSystem.Sytem.LEVEL.coinAdd;
		if (GameSystem.Sytem.PLAYER.gotcoinreward)
		{
			claimbonus.SetActive(value: false);
		}
		else
		{
			claimbonus.SetActive(value: true);
		}
	}
	public void reward()
	{
		Advertisements.Instance.ShowRewardedVideo(VideoComplete);
		SoundManager.SOUND.ClaimButton.Play();
		GameSystem.Sytem.LEVEL.CalculCoinsEarned();
		UIManager.UI.SCORE.UpdateCoinsUI();
		Invoke("NewLevel", 1f);
	}
	private void VideoComplete(bool completed)
    {
        
    }
}
