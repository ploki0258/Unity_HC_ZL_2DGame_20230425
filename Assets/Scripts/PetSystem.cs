using UnityEngine;

public class PetSystem : PlayCtrl
{
	[SerializeField, Header("敵人資料")]
	DataEnemy data;
	[SerializeField, Header("相對位移值"), Tooltip("相對位移向量")]
	Vector3 offset;

	private EnemySystem enemySystem;
	private float attackPet;
	// private Transform enemy;
	private Transform player;
	private float timer;
	private DamagePlayer damagePlayer;

	private void Awake()
	{
		// 取得玩家的 transform
		player = GameObject.Find("主角鼠").transform;
		damagePlayer = GameObject.Find("主角鼠").GetComponent<DamagePlayer>();
		// enemy = GameObject.FindGameObjectWithTag("Enemy").gameObject.transform;
		rig = GetComponent<Rigidbody2D>();
		ani = GetComponent<Animator>();
		attackPet = data.attack;
	}

	private void Update()
	{
		// PetAttack();
		TrackingPlayer();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			damagePlayer.Damage(data.attack);   // 攻擊怪物
			ani.SetTrigger(parAniName);         //	設置攻擊動畫

			timer += Time.deltaTime;

			if (timer >= data.attackInterval)
			{
				timer = 0;
				damagePlayer.Damage(data.attack);
				ani.SetTrigger(parAniName);
			}
		}
	}

	/// <summary>
	/// 跟隨玩家
	/// </summary>
	private void TrackingPlayer()
	{
		Vector3 pos = transform.position;
		Vector3 posPlayer = player.position;
		Vector3 targetPosition = posPlayer + offset;
		targetPosition = Vector3.Lerp(pos, posPlayer, Time.deltaTime);
		transform.position = targetPosition;
	}

	/// <summary>
	/// 追蹤敵人並攻擊
	/// </summary>
	private void PetAttack()
	{
		/*
		float distance = Vector3.Distance(transform.position, enemy.position);

		// 如果距離 小於 敵人資料.攻擊範圍(進入攻擊範圍) 就攻擊敵人
		if (distance < data.attackRange)
		{
			timer += Time.deltaTime;

			if (timer >= data.attackInterval)
			{
				timer = 0;
				damagePlayer.Damage(data.attack);
				ani.SetTrigger(parAniName);
			}
		}
		*/
	}
}
