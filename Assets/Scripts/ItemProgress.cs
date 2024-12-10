using UnityEngine;
using UnityEngine.UI;

public class ItemProgress : MonoBehaviour
{
	public GameObject Progress_Background;

	public GameObject Progress_Bar;

	public GameObject Vegetable_Image;

	public GameObject Sucess_Image;

	public float Vegetable_Progress;

	public bool isFinished;

	public string vegetable_name;

	private void Start()
	{
		foreach (Vegetable vegetable in UnityEngine.Object.FindObjectOfType<VegetablesList>().Vegetables)
		{
			if (vegetable.vegetable.name == vegetable_name)
			{
				Progress_Bar.GetComponent<Image>().color = vegetable.Color;
				break;
			}
		}
	}

	public void UpdateProgress()
	{
		if (Vegetable_Progress >= 100f)
		{
			Sucess();
			return;
		}
		Vegetable_Progress += 2.7f;
		Progress_Bar.GetComponent<RectTransform>().sizeDelta = new Vector2(Vegetable_Progress, 100f);
	}

	private void Sucess()
	{
		isFinished = true;
		Progress_Background.SetActive(value: false);
		Progress_Bar.SetActive(value: false);
		Vegetable_Image.SetActive(value: false);
		Sucess_Image.SetActive(value: true);
	}
}
