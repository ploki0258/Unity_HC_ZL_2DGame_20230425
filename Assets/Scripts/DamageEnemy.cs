using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 敵人受傷系統：
/// 1.敵人扣血
/// 2.敵人死亡
/// 3.掉落經驗值道具
/// </summary>
public class DamageEnemy : DamageBasic
{
	[Header("死亡事件")]
	public UnityEvent onDead;

	private DataEnemy dataEnemy;        // 敵人資料
	private DamagePlayer damagePlayer;  // 玩家資料
	private SkillPlayer skillPlayer;    // 玩家技能資料

	private void Start()
	{
		// 將資料轉換為敵人資料
		dataEnemy = (DataEnemy)data;
		// Debug.Log(dataEnemy.expProbability);

		damagePlayer = GameObject.Find("主角鼠").GetComponent<DamagePlayer>();
		skillPlayer = GameObject.Find("主角鼠").GetComponent<SkillPlayer>();

		if (name.Contains("BOSS"))
			onDead.AddListener(() => damagePlayer.Win());
		//onDead.AddListener(() => skillPlayer.SkillBossDiscripen());
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			float damage = collision.gameObject.GetComponent<Weapon>().attack;
			Damage(damage);
			// Debug.Log(dataPlayer.Attack);

			// 播放怪物受傷音效
			AudioClip sound = SoundManager.instance.soundEnemyHurt;
			SoundManager.instance.PlaySound(sound);
		}
		/*
		if (collision.gameObject.name.Contains("炸彈"))
		{
			HandBomb bomb = collision.gameObject.GetComponent<HandBomb>();
			
			for (int i = 0; i < bomb.colliders2D.Length; i++)
			{
				Debug.Log(bomb.damageArray[i]);
				Damage(bomb.ExplosionDamage()[i]);
			}
		}*/
	}

	/// <summary>
	/// 敵人死亡功能
	/// </summary>
	protected override void Dead()
	{
		base.Dead();
		onDead.Invoke();
		Destroy(gameObject);

		// 播放怪物死亡音效
		AudioClip sound = SoundManager.instance.soundEnemyDead;
		SoundManager.instance.PlaySound(sound);

		float randomValue = Random.value;
		// 如果隨機值 小於 掉落機率 就掉落經驗值道具
		if (randomValue < dataEnemy.expProbability)
		{
			GameObject temp = Instantiate(dataEnemy.prefabExp, transform.position, transform.rotation);

			// 隨機翻轉經驗值道具
			if (Random.value < 0.5f)
			{
				temp.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
			}
			else
			{
				temp.transform.rotation = Quaternion.identity;
			}
		}
		// Debug.Log("隨機值：" + randomValue);
	}
}
