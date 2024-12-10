using UnityEngine;

public class Cutter : MonoBehaviour
{
	private Vector3 randomAngle;

	public string LastItemCut;

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Pillow")
		{
			SoundManager.SOUND.Knife_Cut.Stop();
			GameSystem.Sytem.LEVEL.OnFinished();
		}
		else if (col.tag == "Slice" && GameSystem.Sytem.CurrentKnife.isCutting)
		{
			GameSystem.Sytem.CurrentKnife.vegContact = true;
			if (LastItemCut.Length == 0 || LastItemCut != col.gameObject.transform.parent.name)
			{
				Object.FindObjectOfType<ProgressManager>().numberOfVegetablesCut++;
			}
			LastItemCut = col.gameObject.transform.parent.name;
			Object.FindObjectOfType<ProgressManager>().OnVegetableCut(col.gameObject.transform.parent.name);
			col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
			col.gameObject.GetComponent<Rigidbody>().AddTorque(-Vector3.up * 8900f, ForceMode.Impulse);
			randomAngle = new Vector3(UnityEngine.Random.Range(-0.3f, -0.6f), UnityEngine.Random.Range(0.2f, 0.3f), UnityEngine.Random.Range(-0.5f, 0.5f));
			col.gameObject.GetComponent<Rigidbody>().AddForce(randomAngle * UnityEngine.Random.Range(650, 1500), ForceMode.Impulse);
			GameSystem.Sytem.CurrentKnife.SetCutting(state: true);
			if (col.gameObject.transform.parent.GetComponent<MovableObject>().particle != null && !col.gameObject.transform.parent.GetComponent<MovableObject>().particle.isEmitting)
			{
				col.gameObject.transform.parent.GetComponent<MovableObject>().particle.Play();
			}
			UnityEngine.Object.Destroy(col.gameObject, 4f);
		}
	}
}
