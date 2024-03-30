using System.Collections.Generic;
using UnityEngine;

public class HandBomb : Weapon
{
	#region ���
	[SerializeField, Header("�z���d��"), Range(0f, 10f)] float rangeExplode = 0;
	[SerializeField, Header("�z���ˮ`"), Range(0f, 100f)] float damageExplode = 0;
	//[SerializeField] DataBasic dataBasic;

	DamageEnemy damageEnemy;
	public Collider2D[] colliders2D;
	public List<float> damageArray = new List<float>();
	#endregion

	private void Awake()
	{
		rig2D = GetComponent<Rigidbody2D>();
		damageEnemy = GameObject.FindAnyObjectByType<DamageEnemy>();
	}

	private void Start()
	{
		//damageExplode = dataBasic.attack;
		ThrowBomb();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			//Debug.Log($"<color=#C7C7E2>{collision.gameObject.name}</color>");
			ExplosionDamage();
			Debug.Log($"<color=#C7C7E2>�ˮ`�G{ExplosionDamage().Count}\n�I���G{colliders2D.Length}</color>");
			for (int i = 0; i < ExplosionDamage().Count; i++)
			{
				damageEnemy.Damage(ExplosionDamage()[i]);
			}
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
	/// ���Y���u�G�H���y�СB�O�D
	/// </summary>
	private void ThrowBomb()
	{
		Debug.Log("�ᬵ�u");
		float i = Random.value;
		if (i <= 0.5f)
			rig2D.AddForce(pos * new Vector2(-1.1f, 1f) * force);
		else
			rig2D.AddForce(pos * new Vector2(1.1f, 1f) * force);
	}

	public List<float> ExplosionDamage()
	{
		colliders2D = Physics2D.OverlapCircleAll(transform.position, rangeExplode);

		for (int i = 0; i < colliders2D.Length; i++)
		{
			float dis = Mathf.Max(1 - (Vector2.Distance(transform.position, colliders2D[i].gameObject.transform.position) / rangeExplode), 0f);
			float tempDamage = damageExplode * dis;
			damageArray.Add(tempDamage);
		}

		return damageArray;
	}
}
