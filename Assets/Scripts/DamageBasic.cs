using TMPro;
using UnityEngine;

public class DamageBasic : MonoBehaviour
{
	[Header("���(Enemy)")]
	public DataBasic data;
	[Header("�ˮ`�ȹw�s��")]
	public GameObject prefabDamage = null;

	float hp;   // ��q

	private void Awake()
	{
		hp = data.hp;
	}

	/// <summary>
	/// �ĤH���˥\��
	/// </summary>
	/// <param name="damage">�ˮ`�q</param>
	public void Damage(float damage)
	{
		hp -= damage;
		GameObject tempDamage = Instantiate(prefabDamage, transform.position, transform.rotation);
		tempDamage.transform.Find("�ˮ`�Ȥ�r").GetComponent<TextMeshProUGUI>().text = damage.ToString();

		Destroy(tempDamage, 1.5f);

		hp = Mathf.Clamp(hp, 0f, hp);	// �����q�W�U��
		Debug.Log($"<color=red>{ gameObject.name } �Ѿl��q�G{hp}</color>");

		if (hp <= 0)
		{
			Dead();
		}
	}

	/// <summary>
	/// ���`�\��
	/// </summary>
	protected virtual void Dead()
	{
		Debug.Log($"<color=yellow>{ gameObject.name } ���`</color>");
	}
}
