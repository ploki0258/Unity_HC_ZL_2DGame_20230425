using TMPro;
using UnityEngine;

public class DamageBasic : MonoBehaviour
{
	[Header("資料(Enemy)")]
	public DataBasic data;
	[Header("傷害值預製物")]
	public GameObject prefabDamage = null;

	float hp;   // 血量

	private void Awake()
	{
		hp = data.hp;
	}

	/// <summary>
	/// 敵人受傷功能
	/// </summary>
	/// <param name="damage">傷害量</param>
	public void Damage(float damage)
	{
		hp -= damage;
		GameObject tempDamage = Instantiate(prefabDamage, transform.position, transform.rotation);
		tempDamage.transform.Find("傷害值文字").GetComponent<TextMeshProUGUI>().text = damage.ToString();

		Destroy(tempDamage, 1.5f);

		hp = Mathf.Clamp(hp, 0f, hp);	// 限制血量上下限
		Debug.Log($"<color=red>{ gameObject.name } 剩餘血量：{hp}</color>");

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
		Debug.Log($"<color=yellow>{ gameObject.name } 死亡</color>");
	}
}
