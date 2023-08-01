using UnityEngine;

public class EnemySystem : MonoBehaviour
{
	[SerializeField, Header("敵人資料")]
	DataEnemy data;
	[SerializeField, Header("位移")]
	Vector3 offset;

	private Transform player;
	private float timer;

	private void Awake()
	{
		// 取得玩家的 transform
		player = GameObject.Find("主角鼠").transform;
	}

	private void Update()
	{
		float distance = Vector3.Distance(transform.position, player.position);
		print(distance);

		// 如果距離 大於 敵人資料.攻擊範圍
		// 就追蹤玩家
		if (distance > data.attackRange)
		{
			// 追尋玩家
			transform.position = Vector3.MoveTowards(transform.position, player.position, data.moveSpeed * Time.deltaTime);
		}
		else
		{
			Debug.Log($"<color=#f69>進入攻擊範圍</color>");
			timer += Time.deltaTime;
			Debug.Log($"<color=#f94>計時器：{timer}</color>");
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 1, 0.3f, 0.5f);

		Gizmos.DrawSphere(transform.position + offset, data.attackRange);
	}
}
