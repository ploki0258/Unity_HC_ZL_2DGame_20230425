using UnityEngine;

public class SpawnItem : MonoBehaviour
{
	[SerializeField][Header("位移")] Vector2 offset = new Vector2(0, 0);
	[SerializeField][Header("長寬範圍")] float rangeWidth, rangeHeight;
	[SerializeField]
	[Header("最大/最小_寬高")]
	float minWidth, minHight, maxWidth, maxHight;
	Vector2 spawnRange;

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0f, 0.8f, 0.5f, 0.15f);
		Gizmos.DrawCube(transform.position + (Vector3)offset, new Vector3(rangeWidth, rangeHeight, 0f));
	}

	private void OnDrawGizmosSelected()
	{
		// 最小寬高 到 最大寬高
		Gizmos.color = new Color(0.5f, 0f, 0.5f, 0.8f);
		Gizmos.DrawLine(new Vector3(minWidth, minHight), new Vector3(maxWidth, maxHight));
	}

	public Vector2 SpawnMapItemPos()
	{
		// 設定隨機的寬/高值
		float _width = Random.Range(minWidth, maxWidth);
		float _hight = Random.Range(minHight, maxHight);
		// 設定道具生成座標
		spawnRange = new Vector2(_width, _hight);
		return spawnRange;
	}
}
