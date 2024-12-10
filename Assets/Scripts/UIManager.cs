using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager _ui;

	public ScoreUI SCORE;

	public ShopUI SHOP;

	public CanvasGroup claimRewardButton;

	public GameObject LEVEL_UI_CANVAS;

	public GameObject LEVEL_COMPLETE_CANVAS;

	public GameObject LEVEL_IDLE_CANVAS;

	public GameObject LEVEL_FAIL_CANVAS;

	public static UIManager UI
	{
		get
		{
			if (_ui == null)
			{
				_ui = UnityEngine.Object.FindObjectOfType<UIManager>();
				if (_ui == null)
				{
					GameObject gameObject = new GameObject("UIManager");
					_ui = gameObject.AddComponent<UIManager>();
				}
			}
			return _ui;
		}
	}
	 void Start()
    {
        Advertisements.Instance.Initialize();
		Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.Banner);
        if(Advertisements.Instance.UserConsentWasSet()==false)
        {
        }
        else
        {
            
            Advertisements.Instance.Initialize();
           
        }
    }

	private void Awake()
	{
		SCORE.Awake();
		GameObject x = GameObject.Find("LOADER_CANVAS");
		if (x != null)
		{
			GameObject.Find("LOADER_CANVAS").SetActive(value: false);
		}
	}

	private void Update()
	{
		SCORE.Update();
	}

	public void UpdateLevelCompleteUI()
	{
		// AdsManager.Manager.ShowInterstatial();
		Advertisements.Instance.ShowInterstitial();
		SoundManager.SOUND.ShowStatsSound.Play();
		SCORE.ClaimButton.SetActive(value: true);
		UI.claimRewardButton.gameObject.SetActive(value: true);
		SCORE.UpdateCoinsUI();
		SCORE.Level_C_Score.text = UnityEngine.Object.FindObjectOfType<ProgressManager>().currentPoints.ToString();
		SCORE.Level_C_BestScore.text = "BEST " + GameSystem.Sytem.PLAYER.bestScore.ToString();
		SCORE.PerformanceUI();
		GameSystem.Sytem.LEVEL.bonus = 1;
		GameSystem.Sytem.LEVEL.coinAdd = 30;
		GameSystem.Sytem.PLAYER.gotcoinreward = false;
		GameObject.Find("coins_plus_txt").GetComponent<Text>().text = "+" + 30.ToString();
		// if (AdsManager.Manager.RewardVideo.IsLoaded())
		// {
		// 	claimRewardButton.gameObject.GetComponent<Animator>().Play("reward_anim");
		// 	claimRewardButton.alpha = 1f;
		// }
		// else
		// {
		// 	claimRewardButton.gameObject.GetComponent<Animator>().Play("base_state");
		// 	claimRewardButton.alpha = 0.2f;
		// }
		// Advertisements.Instance.ShowRewardedVideo(VideoComplete);
	}

	public void UpdateLevelFailUI()
	{
		// AdsManager.Manager.ShowInterstatial();
		Advertisements.Instance.ShowRewardedVideo(VideoComplete);
		SCORE.UpdateCoinsUI();
		GameObject.Find("Score").GetComponent<Text>().text = UnityEngine.Object.FindObjectOfType<ProgressManager>().currentPoints.ToString();
		GameObject.Find("Best_Score").GetComponent<Text>().text = "BEST " + GameSystem.Sytem.PLAYER.bestScore.ToString();
	}
	private void VideoComplete(bool completed)
     {
        
     }

	public void ResetLevel()
	{
		SCORE.ResetCombo();
		LEVEL_COMPLETE_CANVAS.SetActive(value: false);
		LEVEL_FAIL_CANVAS.SetActive(value: false);
		LEVEL_UI_CANVAS.SetActive(value: false);
		LEVEL_IDLE_CANVAS.SetActive(value: true);
	}
}
