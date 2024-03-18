using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("投擲力道"), Range(1, 50)]
	[SerializeField] float force = 10f;
	[Header("投擲座標")]
	[SerializeField] Vector2 pos;
	[SerializeField, Header("暴擊率"), Range(0f, 100f), Tooltip("轉換機率為0~1之間的數值")]
	public float critical;
	[SerializeField, Header("暴擊傷害"), Tooltip("原本傷害的倍率")]
	public float criticalHit = 2;
	
	[NonSerialized] public float attack;

	Rigidbody2D rig2D;

	private void Awake()
	{
		rig2D = GetComponent<Rigidbody2D>();
		rig2D.AddForce(pos * force);

		Destroy(gameObject, 5f);
	}
}
