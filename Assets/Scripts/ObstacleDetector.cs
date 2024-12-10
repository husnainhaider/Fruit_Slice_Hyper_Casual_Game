using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
	private int layerMask = 1024;

	private void FixedUpdate()
	{
		if (GameSystem.Sytem.GAME_STATE == GAME_STATE.GAME && Physics.Raycast(base.transform.position, base.transform.TransformDirection(Vector3.down), out RaycastHit hitInfo, float.PositiveInfinity, layerMask) && hitInfo.collider.tag == "Obstacle" && GameSystem.Sytem.CurrentKnife.isCutting)
		{
			if (hitInfo.collider.name.Contains("Wood"))
			{
				GameSystem.Sytem.CurrentKnife.vegContact = false;
				GameSystem.Sytem.CurrentKnife.OnHit();
			}
			else if (hitInfo.collider.name.Contains("Metal"))
			{
				SoundManager.SOUND.Metal_Hit.Play();
				SoundManager.SOUND.Knife_Cut.Stop();
				GameSystem.Sytem.CurrentKnife.KnifeAnimation.SetBool("isFailed", value: true);
				GameSystem.Sytem.LEVEL.OnFail();
			}
		}
	}
}
