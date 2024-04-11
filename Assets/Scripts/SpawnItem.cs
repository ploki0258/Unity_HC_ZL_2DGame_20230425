using UnityEngine;

public class SpawnItem : MonoBehaviour
{
	[SerializeField][Header("�첾")] Vector2 offset = new Vector2(0, 0);
	[SerializeField][Header("���e�d��")] float rangeWidth, rangeHeight;
	[SerializeField]
	[Header("�̤j/�̤p_�e��")]
	float minWidth, minHight, maxWidth, maxHight;
	Vector2 spawnRange;

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0f, 0.8f, 0.5f, 0.15f);
		Gizmos.DrawCube(transform.position + (Vector3)offset, new Vector3(rangeWidth, rangeHeight, 0f));
	}

	private void OnDrawGizmosSelected()
	{
		// �̤p�e�� �� �̤j�e��
		Gizmos.color = new Color(0.5f, 0f, 0.5f, 0.8f);
		Gizmos.DrawLine(new Vector3(minWidth, minHight), new Vector3(maxWidth, maxHight));
	}

	public Vector2 SpawnMapItemPos()
	{
		// �]�w�H�����e/����
		float _width = Random.Range(minWidth, maxWidth);
		float _hight = Random.Range(minHight, maxHight);
		// �]�w�D��ͦ��y��
		spawnRange = new Vector2(_width, _hight);
		return spawnRange;
	}
}
