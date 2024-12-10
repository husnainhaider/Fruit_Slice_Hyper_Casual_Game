using UnityEngine;

public class ShopItem : MonoBehaviour
{
	public bool IsUnlocked;

	public GameObject QuestionMarkUI;

	public GameObject ItemIcon;

	public GameObject KnifeModel;

	public int index;

	public void Unlock()
	{
		IsUnlocked = true;
		QuestionMarkUI.SetActive(value: false);
		ItemIcon.SetActive(value: true);
	}

	public void Select()
	{
		if (IsUnlocked)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("ItemSelectBorder");
			gameObject.GetComponent<RectTransform>().position = base.transform.position;
			Knife[] array = UnityEngine.Object.FindObjectsOfType<Knife>();
			foreach (Knife knife in array)
			{
				knife.gameObject.SetActive(value: false);
			}
			KnifeModel.SetActive(value: true);
			GameSystem.Sytem.PLAYER.setCurrentKnife(index);
			GameSystem.Sytem.SetKnife(index);
		}
	}
}
