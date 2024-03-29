using System.Net;
using UnityEngine;

public class HandBomb : Weapon
{
	#region Äæ¦ì
	[SerializeField, Header("Ãz¬µ½d³ò")] float rangeExplode = 0;
	[SerializeField, Header("Ãz¬µ¶Ë®`")] float damageExplode = 0;

	DamageBasic damageBasic;
	Collider2D[] colliders2D;
	float[] damageArray = new float[0];
	#endregion

	private void Awake()
	{
		rig2D = GetComponent<Rigidbody2D>();
		damageBasic = GameObject.FindAnyObjectByType<DamageBasic>();
	}

	private void Start()
	{
		ThrowBomb();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy") && colliders2D.Length > 0)
		{
			Debug.Log($"<color=#C7C7E2>{collision.gameObject.name}</color>");
			/*for (int i = 0; i < colliders2D.Length; i++)
			{
				damageBasic.Damage(ExplosionDamage()[i]);
			}*/
		}
	}

	/// <summary>
	/// §ëÂY¬µ¼u¡GÀH¾÷®y¼Ð¡B¤O¹D
	/// </summary>
	public void ThrowBomb()
	{
		float i = Random.value;
		if (i <= 0.5f)
			rig2D.AddForce(pos * new Vector2(-1f, 1f) * force);
		else
			rig2D.AddForce(pos * new Vector2(1f, 1f) * force);
	}

	public float[] ExplosionDamage()
	{
		colliders2D = Physics2D.OverlapCircleAll(transform.position, rangeExplode);

		for (int i = 0; i < colliders2D.Length; i++)
		{
			float dis = Mathf.Max(1 - (Vector2.Distance(transform.position, colliders2D[i].gameObject.transform.position) / rangeExplode), 0f);
			float tempDamage = damageExplode * dis;
			damageArray[i] = tempDamage;
		}

		return damageArray;
	}
}
