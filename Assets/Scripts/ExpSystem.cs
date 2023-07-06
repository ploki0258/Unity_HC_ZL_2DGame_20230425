using UnityEngine;

/// <summary>
/// �g��Ȩt��
/// </summary>
public class ExpSystem : MonoBehaviour
{
	[SerializeField, Header("���ʳt��"), Range(0, 10)]
	float speed = 2f;
	[SerializeField, Header("�Q�Y�����Z��"), Range(0, 5)]
	float distanceEat = 1f;
	[SerializeField, Header("�g��ƭ�"), Range(0, 500)]
	float expValue = 30f;

	private Transform player;
	private LevelManager levelManager;

	private void Awake()
	{
		player = GameObject.Find("�D����").transform;
		levelManager = player.GetComponent<LevelManager>();
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
		// �p�G���g��Ȫ���P���a���󪺶Z���p�� 1 �N�Y��
		if (Vector3.Distance(transform.position, player.position) <= distanceEat)
		{
			levelManager.AddExp(expValue);
			Destroy(gameObject);
		}
	}
}
