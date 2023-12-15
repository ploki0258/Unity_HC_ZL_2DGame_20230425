using UnityEngine;

/// <summary>
/// 經驗值系統
/// </summary>
public class ExpSystem : MonoBehaviour
{
	[SerializeField, Header("移動速度"), Range(0, 10)]
	float speed = 2f;
	[SerializeField, Header("被吃掉的距離"), Range(0, 5)]
	float distanceEat = 1f;
	[SerializeField, Header("經驗數值"), Range(0, 500)]
	float expValue = 30f;

	[HideInInspector]
	public Transform player;
	[HideInInspector]
	public LevelManager levelManager;

	private void Awake()
	{
		player = GameObject.Find("主角鼠").transform;
		levelManager = player.GetComponent<LevelManager>();
	}

	private void Update()
	{
		TrackingPlayer();
	}

	/// <summary>
	/// 經驗值道具追蹤玩家
	/// </summary>
	public void TrackingPlayer()
	{
		Debug.Log("已發動效果");
		transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
		// 如果此經驗值物件與玩家物件的距離小於 1 就吃掉
		if (Vector3.Distance(transform.position, player.position) <= distanceEat)
		{
			levelManager.AddExp(expValue);
			//levelManager.skillIcon.enabled = true;
			//levelManager.skillIcon.sprite = levelManager.itemSkillSystem.itemData.iconItem;
			Destroy(gameObject);
			Debug.Log("已發動效果-1");
		}
	}
}
