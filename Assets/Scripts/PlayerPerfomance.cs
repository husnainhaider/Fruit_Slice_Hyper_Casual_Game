using System;
using UnityEngine.UI;

[Serializable]
public class PlayerPerfomance
{
	public Image OnImage;

	public Image OffImage;

	public void isGood(bool b)
	{
		if (b)
		{
			OnImage.gameObject.SetActive(value: true);
			OffImage.gameObject.SetActive(value: false);
		}
		else
		{
			OnImage.gameObject.SetActive(value: false);
			OffImage.gameObject.SetActive(value: true);
		}
	}
}
