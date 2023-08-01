using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("跟隨目標")]
    public Transform target;
    [Header("位移量")]
    public float offset = -2f;

	private void Update()
	{
		Vector3 targetPos = target.position;	// 取得目標物件座標
		targetPos.y += offset;					// 目標物件座標Y軸減去位移量的值
		transform.position = targetPos;			// 更新此物件座標與目標物件一樣
	}
}
