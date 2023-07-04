using UnityEngine;

public class ExpSystem : MonoBehaviour
{
	[SerializeField, Header("移動速度"), Range(0, 10)]
	float speed = 2f;

	private Transform player;

	private void Awake()
	{
		player = GameObject.Find("主角鼠").transform;
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
	}
}
