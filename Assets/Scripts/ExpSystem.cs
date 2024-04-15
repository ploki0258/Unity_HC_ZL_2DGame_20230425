using Fungus;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 經驗值系統
/// </summary>
public class ExpSystem : MonoBehaviour
{
	[SerializeField, Header("移動速度"), Range(0, 100)]
	public float speed = 2f;
	[SerializeField, Header("被吃掉的距離"), Range(0, 10)]
	public float distanceEat = 1f;
	[SerializeField, Header("經驗數值"), Range(0, 500)]
	public float expValue = 30f;

	[HideInInspector]
	protected Transform player;
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
		TrackingPlayer(player.position, true);
		EatEffect(player.position, true);
	}

	/// <summary>
	/// 經驗值道具追蹤玩家
	/// </summary>
	/// <param name="target">要追蹤的目標</param>
	/// <param name="canMove">是否要跟隨</param>
	protected virtual void TrackingPlayer(Vector3 target, bool canMove)
	{
		// 追蹤玩家
		if (canMove == true)
			transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
		else
			return;
	}

	/// <summary>
	/// 道具效果
	/// </summary>
	/// <param name="target">要追蹤的目標</param>
	/// <param name="canMove">是否要跟隨</param>
	/// <param name="delayTime">延遲刪除的時間</param>
	/// <param name="quick">是否立即刪除</param>
	protected virtual void EatEffect(Vector3 target, bool canMove, float delayTime = 0f, bool quick = true)
	{
		distance = Vector3.Distance(transform.position, target);
		// 如果此經驗值物件與玩家物件的距離小於 1 就吃掉
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
