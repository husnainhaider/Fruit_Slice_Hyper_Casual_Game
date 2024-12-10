using UnityEngine;

[ExecuteInEditMode]
public class ImageWithIndependentRoundedCorners : MonoBehaviour
{
	public Vector4 r;

	public Material material;

	[HideInInspector]
	[SerializeField]
	private Vector4 rect2props;

	private readonly int prop_halfSize = Shader.PropertyToID("_halfSize");

	private readonly int prop_radiuses = Shader.PropertyToID("_r");

	private readonly int prop_rect2props = Shader.PropertyToID("_rect2props");

	private static readonly Vector2 wNorm = new Vector2(0.7071068f, -0.7071068f);

	private static readonly Vector2 hNorm = new Vector2(0.7071068f, 0.7071068f);

	private void OnRectTransformDimensionsChange()
	{
		Refresh();
	}

	private void OnValidate()
	{
		Refresh();
	}

	private void RecalculateProps(Vector2 size)
	{
		Vector2 lhs = new Vector2(size.x, 0f - size.y + r.x + r.z);
		float num = Vector2.Dot(lhs, wNorm) * 0.5f;
		rect2props.z = num;
		Vector2 lhs2 = new Vector2(size.x, size.y - r.w - r.y);
		float num2 = Vector2.Dot(lhs2, hNorm) * 0.5f;
		rect2props.w = num2;
		Vector2 lhs3 = new Vector2(size.x - r.x - r.y, 0f);
		Vector2 b = hNorm * Vector2.Dot(lhs3, hNorm);
		Vector2 a = new Vector2(r.x - size.x / 2f, size.y / 2f);
		Vector2 vector = a + b + wNorm * num + hNorm * (0f - num2);
		rect2props.x = vector.x;
		rect2props.y = vector.y;
	}

	private void Refresh()
	{
		Rect rect = ((RectTransform)base.transform).rect;
		RecalculateProps(rect.size);
		material.SetVector(prop_rect2props, rect2props);
		material.SetVector(prop_halfSize, rect.size * 0.5f);
		material.SetVector(prop_radiuses, r);
	}
}
