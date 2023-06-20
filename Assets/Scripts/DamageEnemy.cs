using UnityEngine;

public class DamageEnemy : DamageBasic
{
	public DataBasic dataPlayer;
	public GameObject prefabDamage = null;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			Damage(dataPlayer.Attack);
			Instantiate(prefabDamage, transform.position, transform.rotation);
			// Debug.Log(dataPlayer.Attack);
		}
	}
}
