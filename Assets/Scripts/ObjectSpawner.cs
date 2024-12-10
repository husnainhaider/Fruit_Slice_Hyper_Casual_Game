using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	private void Update()
	{
		int layerMask = 512;
		if (Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.right), out RaycastHit hitInfo, float.PositiveInfinity, layerMask) && hitInfo.distance >= 90f)
		{
			Object.FindObjectOfType<LevelSpawner>().SpawnObject();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
