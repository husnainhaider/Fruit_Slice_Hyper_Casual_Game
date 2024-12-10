using UnityEngine;

public class Knife : MonoBehaviour
{
	public float SignleClickTimeFrame;

	public Animator KnifeAnimation;

	public bool isCutting;

	public bool isHit;

	public bool vegContact;

	public float hitTimeFrame;

	private Rect bounds;

	public float TimeElapsed = 0f;

	private void Start()
	{
		bounds = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	private void FixedUpdate()
	{
		if (isHit || GameSystem.Sytem.GAME_STATE != GAME_STATE.GAME)
		{
			return;
		}
		if (Input.GetMouseButton(0) && bounds.Contains(UnityEngine.Input.mousePosition) && !isCutting)
		{
			TimeElapsed = 0f;
			SetCutting(state: true);
			return;
		}
		TimeElapsed += Time.fixedDeltaTime;
		if (TimeElapsed >= SignleClickTimeFrame && isCutting)
		{
			SetCutting(state: false);
			TimeElapsed = 0f;
		}
	}

	public void SetCutting(bool state)
	{
		SoundManager.SOUND.Knife_Cut.mute = !state;
		isCutting = state;
		KnifeAnimation.SetBool("isCutting", state);
	}

	private void BackNormalState()
	{
		if (isHit)
		{
			isHit = false;
			isCutting = false;
			KnifeAnimation.SetBool("isHit", value: false);
		}
	}

	public void OnHit()
	{
		if (!isHit)
		{
			SoundManager.SOUND.Wood_Hit.Play();
			isHit = true;
			SetCutting(state: false);
			vegContact = false;
			KnifeAnimation.SetBool("isHit", value: true);
			Invoke("BackNormalState", hitTimeFrame);
		}
	}

	public void Reset()
	{
		KnifeAnimation.SetBool("isFailed", value: false);
		SetCutting(state: false);
		isHit = false;
		BackNormalState();
		vegContact = false;
	}
}
