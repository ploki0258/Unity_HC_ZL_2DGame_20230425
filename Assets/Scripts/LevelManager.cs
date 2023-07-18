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
	[SerializeField, Header("升級介面")]
	GameObject goLvUp = null;
	[Header("技能按鈕1~3")]
	[SerializeField] GameObject goSkillUI1;
	[SerializeField] GameObject goSkillUI2;
	[SerializeField] GameObject goSkillUI3;
	[SerializeField, Header("技能資料陣列")]
	DataSkill[] dataSkill;

	public float[] expNeeds = { 100, 200, 300, 400, 500 };

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
		imgExp.fillAmount = 0f; // 歸零經驗條
								// AddExp(50);
								// Debug.Log(expNeeds[3]);

		// for迴圈練習
		/*for (int i = 0; i < 10; i++)
		{
			Debug.Log($"<color=#ff6969>i 的值：{i}</color>");
		}
		*/
	}

	/// <summary>
	/// 添加經驗值功能
	/// </summary>
	/// <param name="exp">經驗值數量</param>
	public void AddExp(float exp)
	{
		this.exp += exp;

		// 目前得等級 = lv - 1
		if (this.exp > expNeeds[lv - 1])
		{
			this.exp -= expNeeds[lv - 1];    // 計算多出的經驗值
			lv++;                       // 等級+1
			textLv.text = "LV " + lv;   // 更新等級文字
			LevelUp();                  // 顯示升級介面
		}

		textExp.text = this.exp + " / " + expNeeds[lv - 1]; // 更新經驗值數值
		imgExp.fillAmount = this.exp / expNeeds[lv - 1];    // 更新經驗值條
	}

	/// <summary>
	/// 顯示升級介面
	/// </summary>
	private void LevelUp()
	{
		goLvUp.SetActive(true);
		Time.timeScale = 0.00001f;
	}

	[ContextMenu("產生經驗值需求資料")]
	void ExpNeeds()
	{
		// 賦予陣列的長度(大小)
		expNeeds = new float[100];

		for (int i = 0; i < 100; i++)
		{
			expNeeds[i] = (i + 1) * 100;
		}
	}
}