using System.Collections.Generic;
using UnityEngine;

public class HandBomb : Weapon
{
	#region Äæ¦ì
	[SerializeField, Header("Ãz¬µ½d³ò"), Range(0f, 10f)] float rangeExplode = 0;
	[SerializeField, Header("Ãz¬µ¶Ë®`"), Range(0f, 100f)] float damageExplode = 0;
	[SerializeField] [Header("Ãz¬µª«¥ó")] GameObject explosionObj = null;
	[SerializeField] DamageEnemy[] damageEnemys;

	public Collider2D[] colliders2D;
	public List<float> damageArray = new List<float>();
	#endregion

	private void Awake()
	{
		rig2D = GetComponent<Rigidbody2D>();
		//damageEnemys = GameObject.FindObjectsOfType<DamageEnemy>();
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
			ExplosionDamage();
			Debug.Log($"<color=#C7C7E2>¸I¼²¡G{colliders2D.Length}\n¶Ë®`¡G{ExplosionDamage().Count}</color>");
			/*for (int i = 0; i < ExplosionDamage().Count; i++)
			{
				damageEnemys[i].Damage(ExplosionDamage()[i]);
			}*/

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
	/// §ëÂY¬µ¼u¡GÀH¾÷®y¼Ð
	/// </summary>
	private void ThrowBomb()
	{
		Debug.Log("¥á¬µ¼u");
		float i = Random.value;
		if (i <= 0.5f)
			rig2D.AddForce(pos * new Vector2(-1.0f, 1f) * force);
		else
			rig2D.AddForce(pos * new Vector2(1.0f, 1f) * force);
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
