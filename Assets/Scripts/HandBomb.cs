using System.Collections.Generic;
using UnityEngine;

public class HandBomb : Weapon
{
	#region 欄位
	[SerializeField][Header("爆炸範圍"), Range(0f, 10f)] float rangeExplode = 0;
	[SerializeField][Header("爆炸傷害"), Range(0f, 500f)] float damageExplode = 0;
	[SerializeField][Header("爆炸物件")] GameObject explosionObj = null;
	[SerializeField][Header("爆炸推力")] Vector2 explosionThrust = new Vector2();

	Collider2D[] colliders2D;
	DamageEnemy[] hurtEnemys;
	public List<float> damageArray = new List<float>();
	#endregion

	private void Awake()
	{
		rig2D = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		ThrowBomb();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			//Debug.Log($"<color=#C7C7E2>{collision.gameObject.name}</color>");
			//Debug.Log($"<color=#C7C7E2>碰撞：{colliders2D.Length}\n傷害：{damageArray.Count}</color>");
			ExplosionDamage();

			GameObject tempExplosion = Instantiate(explosionObj, transform.position, transform.rotation);
			Destroy(this.gameObject, 0.2f);
			Destroy(tempExplosion, 1.0f);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1f, 0.8f, 0.3f, 0.5f);
		Gizmos.DrawSphere(transform.position, rangeExplode);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 0.8f, 0.3f, 0.5f);
		Gizmos.DrawWireSphere(transform.position, rangeExplode);
	}

	/// <summary>
	/// 投擲炸彈：隨機座標
	/// </summary>
	private void ThrowBomb()
	{
		//Debug.Log("丟炸彈");
		float i = Random.value;
		if (i <= 0.5f)
			rig2D.AddForce(-pos * force);
		else
			rig2D.AddForce(pos * force);
	}

	public void ExplosionDamage()
	{
		colliders2D = Physics2D.OverlapCircleAll(transform.position, rangeExplode);

		for (int i = 0; i < colliders2D.Length; i++)
		{
			float per_distance = Mathf.Max(1 - (Vector2.Distance(transform.position, colliders2D[i].gameObject.transform.position) / rangeExplode), 0f);
			float tempDamage = damageExplode * per_distance;
			// 無條件捨去法
			damageArray.Add(Mathf.FloorToInt(tempDamage));

			hurtEnemys = colliders2D[i].GetComponents<DamageEnemy>();

			if (hurtEnemys != null)
			{
				if (colliders2D[i].gameObject.CompareTag("Enemy"))
				{
					for (int j = 0; j < hurtEnemys.Length; j++)
					{
						hurtEnemys[j].Damage(Mathf.FloorToInt(tempDamage));
						// 計算炸彈與碰到的東西之間的向量
						Vector3 ab = (transform.position - hurtEnemys[j].transform.position);
						// 紀錄敵人原本的旋轉方向
						Vector3 originalVec = hurtEnemys[j].transform.rotation.eulerAngles;
						// 把敵人轉向炸彈位置的方向
						hurtEnemys[j].transform.rotation = Quaternion.LookRotation(ab, Vector3.up);
						// 把炸彈推力的本地座標 轉為 世界座標
						Vector3 vecWorld = transform.TransformDirection(explosionThrust);
						// 把敵人的方向轉為原本的方向
						hurtEnemys[j].transform.rotation = Quaternion.Euler(originalVec);
						// 對敵人施加轉換後的炸彈推力(敵人被炸飛)
						Rigidbody2D rig2D = hurtEnemys[j].gameObject.GetComponent<Rigidbody2D>();
						rig2D.AddForce(vecWorld, ForceMode2D.Impulse);
					}
				}
			}
		}
	}
}
