using UnityEngine;

public class DamageEnemy : DamageBasic
{
	[Header("���a���")]
	public DataBasic dataPlayer;

	private DataEnemy dataEnemy;    // �ĤH���

	private void Start()
	{
		// �N����ഫ���ĤH���
		dataEnemy = (DataEnemy)data;
		// Debug.Log(dataEnemy.expProbability);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			Damage(dataPlayer.Attack);

			// Debug.Log(dataPlayer.Attack);
		}
	}

	protected override void Dead()
	{
		base.Dead();
		Destroy(gameObject);

		float randomValue = Random.value;
		// �p�G�H���� �p�� �������v �N�����g��ȹD��
		if (randomValue < dataEnemy.expProbability)
		{
			Instantiate(dataEnemy.prefabExp, transform.position, transform.rotation);
		}
		// Debug.Log("�H���ȡG" + randomValue);
	}
}
