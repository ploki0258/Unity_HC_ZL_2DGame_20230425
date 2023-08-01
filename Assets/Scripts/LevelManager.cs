using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

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
	[SerializeField] GameObject[] goSkillUI;

	/// <summary>
	/// 0 武器攻擊
	/// 1 武器間隔
	/// 2 玩家血量
	/// 3 移動速度
	/// 4 吸取範圍
	/// </summary>
	[SerializeField, Header("技能資料陣列")]
	DataSkill[] dataSkill;

	public List<DataSkill> randomSkill = new List<DataSkill>();
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
		imgExp.fillAmount = 0f;                 // 歸零經驗條
		textLv.text = "LV " + lv.ToString();    // 回復起始等級

		for (int i = 0; i < dataSkill.Length; i++)
		{
			dataSkill[i].skillLv = 1;
		}

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
			this.exp -= expNeeds[lv - 1];   // 計算多出的經驗值
			lv++;                           // 等級+1
			textLv.text = "LV " + lv;       // 更新等級文字
			LevelUp();                      // 顯示升級介面
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

		// 挑選全部技能資料中 等級小於5的技能
		randomSkill = dataSkill.Where(skill => skill.skillLv <= 5).ToList();
		// 將randomSkill清單隨機排序：洗牌
		randomSkill = randomSkill.OrderBy(skill => Random.Range(0, 999)).ToList();

		// 更新技能介面資訊
		for (int i = 0; i < 3; i++)
		{
			goSkillUI[i].transform.Find("技能名稱").GetComponent<TextMeshProUGUI>().text = randomSkill[i].skillName;
			goSkillUI[i].transform.Find("技能描述").GetComponent<TextMeshProUGUI>().text = randomSkill[i].skillDescription;
			goSkillUI[i].transform.Find("技能等級").GetComponent<TextMeshProUGUI>().text = "LV " + randomSkill[i].skillLv;
			goSkillUI[i].transform.Find("技能圖片").GetComponent<Image>().sprite = randomSkill[i].skillPicture;
		}
	}

	[ContextMenu("產生經驗值需求資料")]
	void ExpNeeds()
	{
		// 賦予陣列的長度(大小)
		expNeeds = new float[100];

		// 設定各種等級的需求經驗
		for (int i = 0; i < 100; i++)
		{
			expNeeds[i] = (i + 1) * 100;
		}
	}

	/// <summary>
	/// 點擊技能升級
	/// </summary>
	/// <param name="skillID">技能編號</param>
	public void ClickSkillButton(int skillID)
	{
		// Debug.Log($"<color=#9966ff>點擊技能編號：{skillID}</color>");
		randomSkill[skillID].skillLv++;
		goLvUp.SetActive(false);
		Time.timeScale = 1f;

		if (randomSkill[skillID].skillName == "武器攻擊力提升")
			UpgradeWeaponAttack();
		if (randomSkill[skillID].skillName == "武器間隔縮短")
			UpgradeWeaponInterval();
		if (randomSkill[skillID].skillName == "玩家血量增加")
			UpgradePlayerHp();
		if (randomSkill[skillID].skillName == "移動速度提升")
			UpgradeMoveSpeed();
		if (randomSkill[skillID].skillName == "經驗值範圍增加")
			UpgradeAbsorbExpRange();
	}

	[SerializeField, Header("主角鼠：武器系統")]
	WeaponSystem weaponSystem;
	// 武器攻擊提升
	private void UpgradeWeaponAttack()
	{
		int lv = dataSkill[0].skillLv - 1;
		weaponSystem.attack = dataSkill[0].skillValues[lv];
	}
	// 武器間隔縮短
	private void UpgradeWeaponInterval()
	{
		int lv = dataSkill[1].skillLv - 1;
		weaponSystem.interval = dataSkill[1].skillValues[lv];
		weaponSystem.Restart();
	}

	[SerializeField, Header("主角鼠：玩家資料")]
	DataBasic dataBasic;
	// 玩家血量提升
	private void UpgradePlayerHp()
	{
		int lv = dataSkill[2].skillLv - 1;
		dataBasic.hp = dataSkill[2].skillValues[lv];
	}

	[SerializeField, Header("主角鼠：角色控制")]
	PlayCtrl playerMoveSpeed;
	// 玩家移動提升
	private void UpgradeMoveSpeed()
	{
		int lv = dataSkill[3].skillLv - 1;
		playerMoveSpeed.speed = dataSkill[3].skillValues[lv];
	}

	[SerializeField, Header("主角鼠：吸取經驗值範圍")]
	CircleCollider2D playerExpRange;
	// 經驗範圍增加
	private void UpgradeAbsorbExpRange()
	{
		int lv = dataSkill[4].skillLv - 1;
		playerExpRange.radius = dataSkill[4].skillValues[lv];
	}
}