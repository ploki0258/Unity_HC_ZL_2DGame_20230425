using System.Collections.Generic;
using UnityEngine;

public class HandBomb : Weapon
{
	#region ���
	[SerializeField][Header("�z���d��"), Range(0f, 10f)] float rangeExplode = 0;
	[SerializeField][Header("�z���ˮ`"), Range(0f, 500f)] float damageExplode = 0;
	[SerializeField][Header("�z������")] GameObject explosionObj = null;
	[SerializeField][Header("�z�����O")] Vector3 explosionThrust = new Vector2();

	public Collider2D[] colliders2D;
	public DamageEnemy[] hurtEnemys;
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
			//Debug.Log($"<color=#C7C7E2>�I���G{colliders2D.Length}\n�ˮ`�G{damageArray.Count}</color>");
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
	/// ���Y���u�G�H���y��
	/// </summary>
	private void ThrowBomb()
	{
		//Debug.Log("�ᬵ�u");
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
			// �L����˥h�k
			damageArray.Add(Mathf.FloorToInt(tempDamage));

			hurtEnemys = colliders2D[i].GetComponents<DamageEnemy>();

			if (hurtEnemys != null)
			{
				if (colliders2D[i].gameObject.CompareTag("Enemy"))
				{
					for (int j = 0; j < hurtEnemys.Length; j++)
					{
						hurtEnemys[j].Damage(Mathf.FloorToInt(tempDamage));
						//hurtEnemys[j].gameObject.transform.position += explosionThrust;
					}
				}
			}
		}
	}
}
