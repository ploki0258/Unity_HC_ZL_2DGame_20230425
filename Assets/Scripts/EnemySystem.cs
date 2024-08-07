﻿using UnityEngine;

/// <summary>
/// 敵人系統：
/// 1.追蹤(面向)玩家並攻擊
/// 2.繪製攻擊範圍
/// </summary>
public class EnemySystem : MonoBehaviour
{
	[SerializeField, Header("敵人資料")]
	DataEnemy data;
	[SerializeField, Header("位移")]
	Vector3 offset;
	[SerializeField, Header("頭的位置是否面向右邊")] bool isHeadRight = true;

	private Transform player;
	private float timer;
	private DamagePlayer damagePlayer;
	private Animator animator;

	private void Awake()
	{
		// 取得玩家的 transform
		player = GameObject.Find("主角鼠").transform;
		damagePlayer = player.GetComponent<DamagePlayer>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		float distance = Vector3.Distance(transform.position, player.position);
		// print(distance);
		animator.SetBool("Action", true);
		// 如果距離 大於 敵人資料.攻擊範圍
		// 就追蹤玩家
		// 否則就(每隔攻擊間隔)攻擊玩家
		if (distance > data.attackRange)
		{
			// 追尋玩家
			transform.position = Vector3.MoveTowards(transform.position, player.position, data.moveSpeed * Time.deltaTime);
		}
		else
		{
			// Debug.Log($"<color=#f69>進入攻擊範圍</color>");
			timer += Time.deltaTime;
			// Debug.Log($"<color=#f94>計時器：{timer}</color>");

			if (timer >= data.attackInterval)
			{
				timer = 0;
				damagePlayer.Damage(data.attack);
			}
		}

		// 敵人面向玩家
		// 如果敵人在玩家的右邊
		if (transform.position.x > player.position.x)
		{
			// 轉向玩家
			if (isHeadRight == true)
			{
				transform.eulerAngles = new Vector3(0, 180, 0);
			}
			else
			{
				transform.eulerAngles = new Vector3(0, 0, 0);
			}
		}
		else
		{
			if (isHeadRight == true)
			{
				transform.eulerAngles = new Vector3(0, 0, 0);
			}
			else
			{
				transform.eulerAngles = new Vector3(0, 180, 0);
			}
		}
	}

	// 繪製圖示
	private void OnDrawGizmos()
	{
		// 圖示.顏色
		Gizmos.color = new Color(0, 1, 0.3f, 0.5f);
		// 圖示.繪製圖形(圓形)
		Gizmos.DrawSphere(transform.position + offset, data.attackRange);
	}
}
