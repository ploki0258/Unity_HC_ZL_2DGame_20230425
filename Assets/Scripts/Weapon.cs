using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField, Header("武器種類")] WeaponType weaponType = WeaponType.劍;
	[SerializeField, Header("投擲力道"), Range(1, 50)] public float force = 10f;
	[SerializeField, Header("投擲座標")]public Vector2 pos;
	[SerializeField, Header("暴擊率"), Range(0f, 100f), Tooltip("轉換機率為0~1之間的數值")]
	public float critical;
	[SerializeField, Header("暴擊傷害"), Tooltip("原本傷害的倍率")]
	public float criticalHit = 2;

	[NonSerialized] public float attack;

	protected Rigidbody2D rig2D;
	float timer = 0f;

	private void Awake()
	{
		rig2D = GetComponent<Rigidbody2D>();
		rig2D.AddForce(pos * force);
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer > 8f)
		{
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// 元件右鍵新增功能：恢復初始參數
	/// </summary>
	[ContextMenu("恢復初始數值")]
	void Initialization()
	{
		switch (weaponType)
		{
			case WeaponType.劍:
				force = 10f;
				pos = new Vector2(10f, 10f);
				critical = 5f;
				criticalHit = 2f;
				break;
			case WeaponType.靈劍:
				force = 20f;
				pos = new Vector2(0f, 10f);
				critical = 10f;
				criticalHit = 2.5f;
				break;
			case WeaponType.骷髏劍:
				force = 50f;
				pos = new Vector2(-5f, 2f);
				critical = 30f;
				criticalHit = 3f;
				break;
			case WeaponType.炸彈:
				force = 15f;
				pos = new Vector2(5f, 10f);
				critical = 0f;
				criticalHit = 0f;
				break;
		}
		/*
		if (weaponType == WeaponType.劍)
		{
			force = 10f;
			pos = new Vector2(10f, 10f);
			critical = 5f;
			criticalHit = 2f;
		}
		if (weaponType == WeaponType.靈劍)
		{
			force = 20f;
			pos = new Vector2(0f, 10f);
			critical = 10f;
			criticalHit = 2.5f;
		}
		if (weaponType == WeaponType.骷髏劍)
		{
			force = 50f;
			pos = new Vector2(-5f, 2f);
			critical = 30f;
			criticalHit = 3f;
		}
		*/
	}
}

public enum WeaponType { 劍, 靈劍, 骷髏劍, 炸彈 }