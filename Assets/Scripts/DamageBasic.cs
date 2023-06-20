using UnityEngine;

public class DamageBasic : MonoBehaviour
{
	[Header("���")]
	public DataBasic data;

	float hp;   // ��q

	private void Awake()
	{
		hp = data.hp;
	}

	public void Damage(float damage)
	{
		hp -= damage;
		Mathf.Clamp(hp, 0f, hp);
		Debug.Log($"<color=red>{ gameObject.name } �Ѿl��q�G{hp}</color>");

		if (hp <= 0)
			Dead();
	}

	void Dead()
	{

		Debug.Log($"<color=yellow>{ gameObject.name } ���`</color>");
	}
}
