using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponSystem : MonoBehaviour
{
	#region 欄位
	[Header("生成間隔"), Range(0, 10)]
	public float interval = 3.5f;         // 武器生成間隔時間
	[Header("武器資料")]
	public DataBasic dataWeapon;
	[Header("武器預製物列表")]
	public List<GameObject> prefabWeapon = null; // 要生成的武器
	[Header("新增武器列表")]
	public List<GameObject> prefabAddWeapons = null;
	//[Header("投擲力道"), Range(1, 30)]
	//[SerializeField] float force = 10f;
	[Header("武器推力")]
	[SerializeField] Vector2 power;
	[Header("生成點")]
	[SerializeField] Transform pointWeapon;
	[Header("等級管理器")]
	[SerializeField] LevelManager levelManager;
	[Header("使用次數")] public int usageCount;

	public List<GameObject> tempWeapons = new List<GameObject>();
	[HideInInspector]
	public float attack;
	#endregion

	private void Start()
	{
		InvokeRepeating("SpawnWeapon", 0f, interval);
		attack = dataWeapon.attack;
	}

	private void Update()
	{
		if (usageCount <= 0)
		{
			tempWeapons.Clear();
		}
	}

	/// <summary>
	/// 生成武器方法
	/// </summary>
	void SpawnWeapon()
	{
		int index;
		int index_2;
		GameObject tempWeapon;

		// 升等增加武器種類
		if (levelManager.lv < 5)
			index = Random.Range(0, prefabWeapon.Count - 2);
		else if (levelManager.lv < 10)
			index = Random.Range(0, prefabWeapon.Count - 1);
		else
			index = Random.Range(0, prefabWeapon.Count);

		index_2 = Random.Range(0, tempWeapons.Count);

		if (tempWeapons.Count != 0)
		{
			tempWeapon = Instantiate(tempWeapons[index_2], pointWeapon.position, pointWeapon.rotation);
			usageCount--;
		}
		else
		{
			tempWeapon = Instantiate(prefabWeapon[index], pointWeapon.position, pointWeapon.rotation);
		}

		// 生成的武器.取得剛體元件.添加力量(武器推力) 
		tempWeapon.GetComponent<Rigidbody2D>().AddForce(power);

		// 生成的武器.取得武器元件.攻擊力 = 此腳本的攻擊力
		tempWeapon.GetComponent<Weapon>().attack = this.attack;
		float randomValue = Random.value;

		/*if (tempWeapon.name.Contains(WeaponType.炸彈.ToString()))
		{
			tempWeapon.GetComponent<Weapon>().ThrowBomb();
		}*/

		// 如果機率 小於等於 該武器的暴擊率
		// 生成的武器.取得武器元件.攻擊力 = 此腳本的攻擊力 * 暴擊傷害
		float rate = prefabWeapon[index].GetComponent<Weapon>().critical / 100;
		float hit = prefabWeapon[index].GetComponent<Weapon>().criticalHit;
		if (randomValue <= rate)
			tempWeapon.GetComponent<Weapon>().attack = this.attack * hit;
		Debug.Log($"<color=#FF7575>玩家傷害：{tempWeapon.GetComponent<Weapon>().attack}</color>");

		// 播放攻擊音效
		AudioClip sound = SoundManager.instance.soundFireWeapon;
		SoundManager.instance.PlaySound(sound);
	}

	/// <summary>
	/// 添加新武器至列表
	/// </summary>
	/// <param name="name">要添加的武器名稱</param>
	public void AddWeaponToList(string name)
	{
		//prefabWeapon.AddRange(prefabAddWeapons.Where(weapon => weapon.name.Contains(name)));
		tempWeapons = prefabAddWeapons.Where(weapon => weapon.name.Contains(name)).ToList();
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
