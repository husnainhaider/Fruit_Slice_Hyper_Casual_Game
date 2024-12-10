using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressSceneLoader : MonoBehaviour
{
	[SerializeField]
	public Image progress_bar;

	private AsyncOperation operation;

	private Canvas canvas;

	private float timer = 0f;

	private void Awake()
	{
		canvas = GetComponentInChildren<Canvas>(includeInactive: true);
		Object.DontDestroyOnLoad(base.gameObject);
		Invoke("LoadScene", 2f);
	}

	public void LoadScene()
	{
		UpdateProgressUI(0f);
		canvas.gameObject.SetActive(value: true);
		StartCoroutine(BeginLoad("Game"));
	}

	private IEnumerator BeginLoad(string sceneName)
	{
		operation = SceneManager.LoadSceneAsync(sceneName);
		operation.allowSceneActivation = false;
		while (!operation.isDone)
		{
			UpdateProgressUI(operation.progress);
			timer += Time.deltaTime;
			UpdateProgressUI(operation.progress);
			if (timer > 1f)
			{
				operation.allowSceneActivation = true;
			}
			yield return null;
		}
		yield return null;
	}

	private void UpdateProgressUI(float progress)
	{
		if (progress >= 0.9f)
		{
			progress = 1f;
		}
		progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2(progress * 100f, 100f);
	}
}
