using UnityEngine;

public class EnemySystem : MonoBehaviour
{
	[SerializeField, Header("�ĤH���")]
	DataEnemy data;

	private Transform player;

	private void Awake()
	{
		// ���o���a�� transform
		player = GameObject.Find("�D����").transform;
	}

	private void Update()
	{
		// �l�M���a
		transform.position = Vector3.MoveTowards(transform.position, player.position, data.moveSpeed * Time.deltaTime);
	}
}
