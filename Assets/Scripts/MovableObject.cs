using UnityEngine;

public class MovableObject : MonoBehaviour
{
	public float speed;

	public ParticleSystem particle;

	private void FixedUpdate()
	{
		base.transform.Translate(-Vector3.right * Time.deltaTime * GameSystem.Sytem.LEVEL.CurrentSpeed, Space.World);
	}
}
