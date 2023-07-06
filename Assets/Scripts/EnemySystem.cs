using UnityEngine;

public class EnemySystem : MonoBehaviour
{
	[SerializeField, Header("敵人資料")]
	DataEnemy data;

	private Transform player;

	private void Awake()
	{
		// 取得玩家的 transform
		player = GameObject.Find("主角鼠").transform;
	}

	private void Update()
	{
		// 追尋玩家
		transform.position = Vector3.MoveTowards(transform.position, player.position, data.moveSpeed * Time.deltaTime);
	}
}
