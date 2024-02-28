﻿using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
	[Header("生成間隔"), Range(0, 10)]
	public float interval = 3.5f;         // 武器生成間隔時間
	[Header("武器資料")]
	public DataBasic dataWeapon;
	[Header("武器預製物")]
	[SerializeField] GameObject[] prefabWeapon = null; // 要生成的武器
													   // [Header("投擲力道"), Range(1, 30)]
													   // [SerializeField] float force = 10f;
	public float attack;

	[Header("武器推力")]
	[SerializeField] Vector2 power;
	[Header("生成點")]
	[SerializeField] Transform pointWeapon;
	[Header("等級管理器")]
	[SerializeField] LevelManager levelManager;

	private void Start()
	{
		InvokeRepeating("SpawnWeapon", 0f, interval);
		attack = dataWeapon.attack;
	}

	/// <summary>
	/// 生成武器方法
	/// </summary>
	void SpawnWeapon()
	{
		int index;

		if (levelManager.lv < 5)
			index = Random.Range(0, prefabWeapon.Length - 2);
		else if (levelManager.lv < 10)
			index = Random.Range(0, prefabWeapon.Length - 1);
		else
			index = Random.Range(0, prefabWeapon.Length);

		GameObject tempWeapon = Instantiate(prefabWeapon[index], pointWeapon.position, pointWeapon.rotation);
		
		// 生成的武器.取得剛體元件.添加力量(武器推力) 
		tempWeapon.GetComponent<Rigidbody2D>().AddForce(power);
		
		// 生成的武器.取得武器元件.攻擊力 = 此腳本的攻擊力
		tempWeapon.GetComponent<Weapon>().attack = this.attack;
		float randomValue = Random.value;
		float rate = prefabWeapon[index].GetComponent<Weapon>().critical / 100;
		float hit = prefabWeapon[index].GetComponent<Weapon>().criticalHit;
		// 如果機率 小於等於 該武器的暴擊率
		// 生成的武器.取得武器元件.攻擊力 = 此腳本的攻擊力 * 暴擊傷害
		if (randomValue <= rate)
			tempWeapon.GetComponent<Weapon>().attack = this.attack * hit;
		
		// 播放攻擊音效
		AudioClip sound = SoundManager.instance.soundFireWeapon;
		SoundManager.instance.PlaySound(sound);
	}

	/// <summary>
	/// 重新啟動武器生成系統
	/// </summary>
	public void Restart()
	{
		CancelInvoke("SpawnWeapon");
		InvokeRepeating("SpawnWeapon", 0f, interval);
	}

	/// <summary>
	/// 停止武器生成系統
	/// </summary>
	public void Stop()
	{
		CancelInvoke("SpawnWeapon");
	}
}
