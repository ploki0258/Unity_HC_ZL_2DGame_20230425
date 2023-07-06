using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 等級系統
/// </summary>
public class LevelManager : MonoBehaviour
{
	[SerializeField, Header("經驗值")]
	Image imgExp;
	[SerializeField, Header("文字等級")]
	TextMeshProUGUI textLv;
	[SerializeField, Header("文字經驗值")]
	TextMeshProUGUI textExp;

	public float[] expNeeds = { 100, 200, 300, 400, 500};

	private int lv = 1;
	private float exp = 0;

	/// <summary>
	/// 觸發事件
	/// </summary>
	/// <param name="collision">碰到的物件</param>
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Debug.Log($"<color=#0066ff>{collision.name}</color>");

		if (collision.name.Contains("經驗值"))
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