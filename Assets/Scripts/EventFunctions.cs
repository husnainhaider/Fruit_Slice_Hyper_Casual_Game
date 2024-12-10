using UnityEngine;

public class EventFunctions : MonoBehaviour
{
	public GameObject buttonn,buttonn1,buttonn2;
	public void OnClaim()
	{
		SoundManager.SOUND.ClaimButton.Play();
		GameSystem.Sytem.LEVEL.CalculCoinsEarned();
		UIManager.UI.SCORE.UpdateCoinsUI();
		Invoke("NewLevel", 1f);
	}

	public void OnRestart()
	{
		Restart();
	}
     void Start()
    {
       
        

    }
	private void NewLevel()
	{
		GameObject.Find("Final_Light").GetComponent<Light>().enabled = false;
		GameObject.Find("END_Knife").GetComponent<Animator>().Play("Normal_State");
		GameObject.Find("Cut_Plank").GetComponent<Animator>().Play("Normal_State");
		Restart();
		Camera.main.GetComponent<Animator>().Play("Game_State");
		UnityEngine.Object.Destroy(GameObject.FindGameObjectWithTag("PotParts"));
		GameSystem.Sytem.LEVEL.Pot.SetActive(value: false);
		SoundManager.SOUND.CookingSound.Stop();
	}

	private void Restart()
	{
		Object.FindObjectOfType<ProgressManager>().Reset();
		GameSystem.Sytem.LEVEL.RestartLevel();
		UIManager.UI.ResetLevel();
		if (GameSystem.Sytem.GAME_STATE == GAME_STATE.FAIL)
		{
			Object.FindObjectOfType<LevelSpawner>().ResetAll();
		}
		GameSystem.Sytem.setGameState(GAME_STATE.IDLE);
		Object.FindObjectOfType<LevelSpawner>().Start();
	}

	public void OnShop()
	{
		GameSystem.Sytem.setGameState(GAME_STATE.SHOP);
		UIManager.UI.SHOP.Open();
		GameSystem.Sytem.SHOP.InitKnivesItemList();
		UIManager.UI.SHOP.UpdateKnifeCollection();
	}

	public void OnCloseShop()
	{
		GameSystem.Sytem.setGameState(GAME_STATE.IDLE);
		UIManager.UI.SHOP.Close();
	}

	public void SetLevelUI()
	{
		UIManager.UI.SCORE.SetLevel();
		UIManager.UI.SCORE.UpdateScore(0);
	}

	public void OnUnlock()
	{
		GameSystem.Sytem.SHOP.UnlockRandom();
	}
	public void Adsestart()
	{
		Advertisements.Instance.ShowInterstitial();
		buttonn.SetActive(false);
	}
	public void AdsClaim()
	{
		Advertisements.Instance.ShowInterstitial();
		buttonn1.SetActive(false);
	}
	public void Adskiplvl()
	{
		Advertisements.Instance.ShowRewardedVideo(VideoComplete);
		buttonn2.SetActive(false);
	}
	private void VideoComplete(bool completed)
    {
        
    }
}
