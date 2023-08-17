using UnityEngine;
using UnityEngine.Events;

public class DamageEnemy : DamageBasic
{
	[Header("死亡事件")]
	public UnityEvent onDead;

	private DataEnemy dataEnemy;        // 敵人資料
	private DamagePlayer damagePlayer;  // 玩家資料

	private void Start()
	{
		// 將資料轉換為敵人資料
		dataEnemy = (DataEnemy)data;
		// Debug.Log(dataEnemy.expProbability);

		damagePlayer = GameObject.Find("主角鼠").GetComponent<DamagePlayer>();
		if (name.Contains("BOSS"))
			onDead.AddListener(() => damagePlayer.Win());
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			float damage = collision.gameObject.GetComponent<Weapon>().attack;
			Damage(damage);
			// Debug.Log(dataPlayer.Attack);
		}
	}

	/// <summary>
	/// 敵人死亡功能
	/// </summary>
	protected override void Dead()
	{
		base.Dead();
		onDead.Invoke();
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
