using UnityEngine;

public class DamageEnemy : DamageBasic
{
	[Header("玩家資料")]
	public DataBasic dataPlayer;

	private DataEnemy dataEnemy;    // 敵人資料

	private void Start()
	{
		// 將資料轉換為敵人資料
		dataEnemy = (DataEnemy)data;
		// Debug.Log(dataEnemy.expProbability);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			Damage(dataPlayer.Attack);

			// Debug.Log(dataPlayer.Attack);
		}
	}

	protected override void Dead()
	{
		base.Dead();
		Destroy(gameObject);

		float randomValue = Random.value;
		// 如果隨機值 小於 掉落機率 就掉落經驗值道具
		if (randomValue < dataEnemy.expProbability)
		{
			Instantiate(dataEnemy.prefabExp, transform.position, transform.rotation);
		}
		// Debug.Log("隨機值：" + randomValue);
	}
}
