using System;
using TMPro;
using UnityEngine;

/// <summary>
/// 基本受傷系統：
/// 1.扣血功能
/// 2.生成傷害值預製物
/// 3.死亡功能
/// </summary>
public class DamageBasic : MonoBehaviour
{
	[Header("資料(Enemy)")]
	public DataBasic data;
	[Header("傷害值預製物")]
	public GameObject prefabDamage = null;

	[SerializeField]
	public float hpMax;		// 血量最大值
	protected float _hp;	// 血量
	
	private void Awake()
	{
		hpMax = data.hp;
		hp = hpMax;
	}

	/// <summary>
	/// Hp屬性
	/// </summary>
	public float hp
	{
		get { return _hp; }
		set
		{
			_hp = value;

			if (hpChangeAction != null)
			{
				hpChangeAction.Invoke();
			}
		}
	}
	public Action hpChangeAction;

	/// <summary>
	/// 敵人受傷功能
	/// </summary>
	/// <param name="damage">傷害量</param>
	public virtual void Damage(float damage)
	{
		hp -= damage;
		GameObject tempDamage = Instantiate(prefabDamage, transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
		tempDamage.transform.Find("傷害值文字").GetComponent<TextMeshProUGUI>().text = damage.ToString();

		Destroy(tempDamage, 1.5f);

		hp = Mathf.Clamp(hp, 0f, hpMax);	// 限制血量上下限
		//Debug.Log($"<color=#FF60AF>{ gameObject.name } 剩餘血量：{hp}</color>");

		if (hp <= 0)
		{
			Dead();
		}
	}

	/// <summary>
	/// 死亡功能
	/// </summary>
	protected virtual void Dead()
	{
		// Debug.Log($"<color=yellow>{ gameObject.name } 死亡</color>");
	}
}
