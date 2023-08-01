using UnityEngine;

public class EnemySystem : MonoBehaviour
{
	[SerializeField, Header("�ĤH���")]
	DataEnemy data;
	[SerializeField, Header("�첾")]
	Vector3 offset;

	private Transform player;
	private float timer;

	private void Awake()
	{
		// ���o���a�� transform
		player = GameObject.Find("�D����").transform;
	}

	private void Update()
	{
		float distance = Vector3.Distance(transform.position, player.position);
		print(distance);

		// �p�G�Z�� �j�� �ĤH���.�����d��
		// �N�l�ܪ��a
		if (distance > data.attackRange)
		{
			// �l�M���a
			transform.position = Vector3.MoveTowards(transform.position, player.position, data.moveSpeed * Time.deltaTime);
		}
		else
		{
			Debug.Log($"<color=#f69>�i�J�����d��</color>");
			timer += Time.deltaTime;
			Debug.Log($"<color=#f94>�p�ɾ��G{timer}</color>");
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 1, 0.3f, 0.5f);

		Gizmos.DrawSphere(transform.position + offset, data.attackRange);
	}
}
