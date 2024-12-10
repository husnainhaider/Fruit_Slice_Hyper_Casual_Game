using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	public void PlayCameraTransition()
	{
		Camera.main.GetComponent<Animator>().Play("Camera_Transition");
		UIManager.UI.LEVEL_UI_CANVAS.SetActive(value: false);
	}

	public void TurnLight()
	{
		GameObject.Find("Final_Light").GetComponent<Light>().enabled = true;
	}

	public void Cooking()
	{
		SoundManager.SOUND.CookingSound.Play();
		GameObject.Find("END_Knife").GetComponent<Animator>().Play("End_Knife_Animation");
		GameObject.Find("Cut_Plank").GetComponent<Animator>().Play("Cut_Plank");
	}

	public void ActivateParts()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Final_Slices");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	public void ShowLevelCompleteUI()
	{
		UIManager.UI.LEVEL_COMPLETE_CANVAS.SetActive(value: true);
		UIManager.UI.UpdateLevelCompleteUI();
	}

	public void ShowLevelFailedUI()
	{
		UIManager.UI.LEVEL_UI_CANVAS.SetActive(value: false);
		UIManager.UI.LEVEL_FAIL_CANVAS.SetActive(value: true);
		UIManager.UI.UpdateLevelFailUI();
	}

	public void ReSpawn()
	{
		UnityEngine.Object.Destroy(GameObject.FindGameObjectWithTag("Pillow"));
		GameSystem.Sytem.LEVEL.NextLevel();
		Object.FindObjectOfType<LevelSpawner>().Start();
	}

	public void Knife_Flip_Sound()
	{
		SoundManager.SOUND.Knife_Flip.Play();
	}

	public void LevelCompleteSound()
	{
		SoundManager.SOUND.LevelComplete.Play();
	}

	public void SpawnPot()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(GameSystem.Sytem.LEVEL.prefabPotCuts, base.transform);
		gameObject.transform.parent = null;
		gameObject.transform.position = GameObject.Find("Pot_Cut_T").transform.position;
		gameObject.transform.rotation = default(Quaternion);
		gameObject.tag = "PotParts";
		gameObject.SetActive(value: true);
		GameSystem.Sytem.LEVEL.Pot.SetActive(value: true);
	}
}
