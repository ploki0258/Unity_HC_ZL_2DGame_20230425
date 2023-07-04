using UnityEngine;

public class ExpSystem : MonoBehaviour
{
	[SerializeField, Header("���ʳt��"), Range(0, 10)]
	float speed = 2f;

	private Transform player;

	private void Awake()
	{
		player = GameObject.Find("�D����").transform;
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
	}
}
