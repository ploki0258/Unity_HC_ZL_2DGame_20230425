using UnityEngine;

/// <summary>
/// 經驗值系統
/// </summary>
public class ExpSystem : MonoBehaviour
{
	[SerializeField, Header("移動速度"), Range(0, 10)]
	public float speed = 2f;
	[SerializeField, Header("被吃掉的距離"), Range(0, 5)]
	public float distanceEat = 1f;
	[SerializeField, Header("經驗數值"), Range(0, 500)]
	public float expValue = 30f;

	[HideInInspector]
	public Transform player;
	[HideInInspector]
	public LevelManager levelManager;

	protected float distance;

	protected virtual void Awake()
	{
		player = GameObject.Find("主角鼠").transform;
		levelManager = player.GetComponent<LevelManager>();
		this.enabled = false;
	}

	protected virtual void Update()
	{
		TrackingPlayer(player.position);
	}

	/// <summary>
	/// 經驗值道具追蹤玩家
	/// </summary>
	/// <param name="target">要追蹤的目標</param>
	/// <param name="delayTime">延遲刪除的時間(秒)</param>
	/// <param name="quick">是否立即刪除</param>
	protected virtual void TrackingPlayer(Vector3 target, float delayTime = 0f, bool quick = true)
	{
		// 追蹤玩家
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
		// 如果此經驗值物件與玩家物件的距離小於 1 就吃掉
		distance = Vector3.Distance(transform.position, target);
		if (distance <= distanceEat)
		{
			// 增加經驗值
			levelManager.AddExp(expValue);
			// 判斷是否立即刪除
			if (quick)
			{
				Destroy(gameObject);
			}
			else
			{
				Destroy(gameObject, delayTime);
			}
		}
	}
}
