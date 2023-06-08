using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    private Transform player;

	private void Awake()
	{
		player = GameObject.Find("¥D¨¤¹«").transform;
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime);
	}
}
