using UnityEngine;

public class PetSystem : PlayCtrl
{
	[SerializeField, Header("敵人資料")]
	DataEnemy data;
	
	private EnemySystem enemySystem;
    private float attackPet;
	private Transform enemy;
	private Transform player;
	private float timer;
	private DamagePlayer damagePlayer;
	
	private void Awake()
	{
		// 取得玩家的 transform
		player = GameObject.Find("主角鼠").transform;
		enemy = GameObject.FindGameObjectWithTag("Enemy").gameObject.transform;
		rig = GetComponent<Rigidbody2D>();
		ani = GetComponent<Animator>();
		attackPet = data.attack;
	}

	private void Update()
	{
		PetAttack();
		TrackingPlayer();
		Debug.Log(enemy.ToString());
	}

	private void TrackingPlayer()
	{
		//Transform[] pos = player.GetComponent<LevelManager>().pointPetArray;
		//int random = Random.Range(0, pos.Length);
		//Vector3 posPet = pos[random].position;
		transform.position = player.position + transform.position;
	}

	/// <summary>
	/// 追蹤敵人並攻擊
	/// </summary>
	private void PetAttack()
	{
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
	}
}
