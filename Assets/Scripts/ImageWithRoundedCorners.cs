using UnityEngine;

[ExecuteInEditMode]
public class ImageWithRoundedCorners : MonoBehaviour
{
	private static readonly int Props = Shader.PropertyToID("_WidthHeightRadius");

	public Material material;

	public float radius;

	private void OnRectTransformDimensionsChange()
	{
		Refresh();
	}

	private void OnValidate()
	{
		Refresh();
	}

	private void Refresh()
	{
		Rect rect = ((RectTransform)base.transform).rect;
		material.SetVector(Props, new Vector4(rect.width, rect.height, radius, 0f));
	}
}
