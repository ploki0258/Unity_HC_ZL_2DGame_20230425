using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���Ũt��
/// </summary>
public class LevelManager : MonoBehaviour
{
	[SerializeField, Header("�g���")]
	Image imgExp;
	[SerializeField, Header("��r����")]
	TextMeshProUGUI textLv;
	[SerializeField, Header("��r�g���")]
	TextMeshProUGUI textExp;

	public float[] expNeeds = { 100, 200, 300, 400, 500};

	private int lv = 1;
	private float exp = 0;

	/// <summary>
	/// Ĳ�o�ƥ�
	/// </summary>
	/// <param name="collision">�I�쪺����</param>
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Debug.Log($"<color=#0066ff>{collision.name}</color>");

		if (collision.name.Contains("�g���"))
		{
			collision.GetComponent<ExpSystem>().enabled = true;
		}
	}

	private void Start()
	{
		// AddExp(50);
	}

	public void AddExp(float exp)
	{
		this.exp += exp;

		textExp.text = this.exp + " / 100";
		imgExp.fillAmount = this.exp / 100;
	}
}