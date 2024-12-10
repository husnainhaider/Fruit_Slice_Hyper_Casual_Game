using UnityEngine;

public class ObjDestroyer : MonoBehaviour
{
	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Slice")
		{
			UnityEngine.Object.Destroy(col.gameObject.transform.parent.gameObject);
		}
		if (col.tag == "Obstacle")
		{
			UnityEngine.Object.Destroy(col.gameObject);
		}
	}
}
