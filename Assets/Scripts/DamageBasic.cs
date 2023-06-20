using UnityEngine;

public class DamageBasic : MonoBehaviour
{
	[Header("資料")]
	public DataBasic data;

	float hp;   // 血量

	private void Awake()
	{
		hp = data.hp;
	}

	public void Damage(float damage)
	{
		hp -= damage;
		Mathf.Clamp(hp, 0f, hp);
		Debug.Log($"<color=red>{ gameObject.name } 剩餘血量：{hp}</color>");

		if (hp <= 0)
			Dead();
	}

	void Dead()
	{

		Debug.Log($"<color=yellow>{ gameObject.name } 死亡</color>");
	}
}
