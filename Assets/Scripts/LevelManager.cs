using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

/// <summary>
/// 等級系統：管理角色的升級技能
/// </summary>
public class LevelManager : MonoBehaviour
{
	#region 欄位
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
	[Header("技能欄圖示")]
	public Image skillIcon = null;
	[Header("關閉按鈕")]
	public GameObject btnClose;
	[Header("確認按鈕")]
	public GameObject btnConfirm;
	[Header("召喚獸生成點")]
	public Transform[] pointPetArray;

	/// <summary>
	/// 0 武器攻擊
	/// 1 武器間隔
	/// 2 玩家血量
	/// 3 移動速度
	/// 4 吸取範圍
	/// 5 召喚萌寵
	/// </summary>
	[SerializeField, Header("技能資料陣列")]
	DataSkill[] dataSkill;

	public List<DataSkill> randomSkill = new List<DataSkill>();
	public float[] expNeeds = { 100, 200, 300, 400, 500 };

	[Header("按鈕選擇列表"), Tooltip("所存放的按鈕編號的列表")]
	public List<int> buttonSelectList = new List<int>();

	private int lv = 1;         // 等級
	private float exp = 0;      // 經驗值
	public float timer = 0;    // 計時器
	private ButtonSelectManager buttonSelectManager;
	private ItemSkillSystem itemSkillSystem;
	private Color normalColor = Color.gray;
	private Color selectColor = new Color(255f, 255f, 0f);
	#endregion

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
			Debug.Log("已碰到經驗值物件");
		}
		else if (collision.name.Contains("BOSS"))
		{
			collision.GetComponent<ItemSkillSystem>().enabled = true;
			skillIcon.enabled = true;
			skillIcon.sprite = itemSkillSystem.itemData.iconItem;
			Debug.Log("已碰到BOSS物件");
		}
	}

	private void Awake()
	{
		buttonSelectManager = FindObjectOfType<ButtonSelectManager>();
		itemSkillSystem = FindObjectOfType<ItemSkillSystem>();
		skillIcon.enabled = false;
	}

	private void Start()
	{
		imgExp.fillAmount = 0f;                 // 歸零經驗條
		textLv.text = "LV " + lv.ToString();    // 回復起始等級

		for (int i = 0; i < dataSkill.Length; i++)
		{
			dataSkill[i].skillLv = 1;
		}
		#region 練習 & 測試
		// AddExp(50);
		// Debug.Log(expNeeds[3]);

		// for迴圈練習
		/*for (int i = 0; i < 10; i++)
		{
			Debug.Log($"<color=#ff6969>i 的值：{i}</color>");
		}
		*/
		#endregion
		// buttonSelectManager.DeselectButton();
	}

	private void Update()
	{
		// 如果所選擇的按鈕個數 等於 2個 或只剩下一種技能時 就顯示確認按鈕
		// 否則就隱藏
		if (buttonSelectList.Count == goSkillUI.Length - 1 || randomSkill.Count < 2)
		{
			btnConfirm.SetActive(true);
		}
		else
		{
			btnConfirm.SetActive(false);
		}

		ItemEffectForBoss();

#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
		{
			// AddExp(100);

			buttonSelectList.Clear();

			// 依據玩家當前的經驗值需求來升級經驗
			float needsValue = expNeeds[lv - 1];
			AddExp(needsValue);
		}
#endif
	}

	/// <summary>
	/// 添加經驗值功能
	/// </summary>
	/// <param name="exp">經驗值數量</param>
	public void AddExp(float exp)
	{
		this.exp += exp;

		// 目前的等級 = lv - 1
		if (this.exp >= expNeeds[lv - 1])
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
		// 播放升級音效
		AudioClip sound = SoundManager.instance.soundLvUp;
		SoundManager.instance.PlaySound(sound);

		goLvUp.SetActive(true);
		Time.timeScale = 0.00001f;

		// 挑選全部技能資料中 等級小於5的技能
		randomSkill = dataSkill.Where(skill => skill.skillLv < 5).ToList();
		// 將randomSkill清單隨機排序：洗牌
		randomSkill = randomSkill.OrderBy(skill => Random.Range(0, 999)).ToList();

		// 更新技能介面資訊
		// 顯示3個技能按鈕
		for (int i = 0; i < 3; i++)
		{
			if (i > randomSkill.Count - 1)
			{
				// 升滿的技能不顯示
				goSkillUI[i].SetActive(false);
			}
			else
			{
				goSkillUI[i].transform.Find("技能名稱底圖").Find("技能名稱").GetComponent<TextMeshProUGUI>().text = randomSkill[i].skillName;
				goSkillUI[i].transform.Find("技能描述底圖").Find("技能描述").GetComponent<TextMeshProUGUI>().text = randomSkill[i].skillDescription;
				goSkillUI[i].transform.Find("技能等級").GetComponent<TextMeshProUGUI>().text = "LV " + randomSkill[i].skillLv;
				goSkillUI[i].transform.Find("技能圖片").GetComponent<Image>().sprite = randomSkill[i].skillPicture;
				// goSkillUI[i].transform.Find("技能名稱").GetComponent<TextMeshProUGUI>().text = randomSkill[i].skillName;
				// goSkillUI[i].transform.Find("技能描述").GetComponent<TextMeshProUGUI>().text = randomSkill[i].skillDescription;
			}
		}

		// 初始化所有按鈕的顏色
		foreach (GameObject colorButton in goSkillUI)
		{
			colorButton.GetComponent<Image>().color = normalColor;
		}

		// 如果隨機技能等於0 則顯示關閉按鈕
		if (randomSkill.Count == 0)
		{
			btnClose.SetActive(true);
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
	/// 點擊關閉按鈕
	/// </summary>
	public void ClickCloseButton()
	{
		goLvUp.SetActive(false);    // 關閉介面
		Time.timeScale = 1f;        // 恢復時間
	}

	/// <summary>
	/// 點擊確認按鈕
	/// </summary>
	public void ClickConfirmButton()
	{
		// 播放技能升級音效
		AudioClip sound = SoundManager.instance.soundSkillLvUp;
		SoundManager.instance.PlaySound(sound);

		foreach (int skillID in buttonSelectList)
		{
			randomSkill[skillID].skillLv++; // 技能等級+1

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
			if (randomSkill[skillID].skillName == "召喚獸數量增加")
				UpgradeSummonPet();
		}

		buttonSelectList.Clear();   // 清空列表
		goLvUp.SetActive(false);    // 關閉介面
		Time.timeScale = 1f;        // 恢復時間
	}

	[Tooltip("最大選擇數量")]
	private int maxSelectCount = 2;
	[Tooltip("是否已達最大選擇數量")]
	private bool maxSelect = false;

	/// <summary>
	/// 點擊技能按鈕升級
	/// </summary>
	/// <param name="skillID">技能按鈕編號</param>
	public void ClickSkillButton(int skillID)
	{
		/*
		// Debug.Log($"<color=#9966ff>點擊技能編號：{skillID}</color>");
		randomSkill[skillID].skillLv++; // 技能等級+1
		goLvUp.SetActive(false);		// 隱藏技能介面
		Time.timeScale = 1f;			// 遊戲時間恢復
		*/

		// 如果按鈕列表中的數量 等於 最大選擇數量的話
		if (buttonSelectList.Count == maxSelectCount)
		{
			maxSelect = true;   // 是否已達最大選擇數量 = true
		}
		else
		{
			maxSelect = false;  // 是否已達最大選擇數量 = false
		}

		// 如果已達最大選擇數量的話
		if (maxSelect)
		{
			// 如果是未選的按鈕的話 就不執行
			// 否則就從列表中移除 顏色變為灰色
			if (!buttonSelectList.Contains(skillID))
			{
				return;
			}
			else
			{
				buttonSelectList.Remove(skillID);   // 從列表中移除
				goSkillUI[skillID].GetComponent<Image>().color = normalColor;
			}
		}
		else
		{
			// 如果選中按鈕時 添加至列表中 顏色變為黃色
			// 否則未選按鈕時 從列表中移除 顏色變為灰色
			if (buttonSelectList.Contains(skillID))
			{
				buttonSelectList.Remove(skillID);   // 從列表中移除
				goSkillUI[skillID].GetComponent<Image>().color = normalColor;
			}
			else
			{
				buttonSelectList.Add(skillID);  // 添加至列表
				goSkillUI[skillID].GetComponent<Image>().color = selectColor;
			}
		}

		#region 寫法測試
		/*
		buttonSelectManager.SwitchSelected();   // 點擊切換按鈕狀態

		for (int i = 0; i < goSkillUI.Length; i++)
		{
			Color colorButton = goSkillUI[i].GetComponent<Image>().color;

			if (buttonSelectList.Contains(skillID))
			{
				colorButton = Color.yellow;
			}
			else
			{
				colorButton = new Color(96f, 96f, 96f);
			}

			// buttonSelectManager.indexSelectButton = skillID;
			// buttonSelectManager.buttonList.Add(buttonSelectManager.indexChooseButton);
		}
		*/
		#endregion

		#region 原本Script
		//if (randomSkill[skillID].skillName == "武器攻擊力提升")
		//	UpgradeWeaponAttack();
		//if (randomSkill[skillID].skillName == "武器間隔縮短")
		//	UpgradeWeaponInterval();
		//if (randomSkill[skillID].skillName == "玩家血量增加")
		//	UpgradePlayerHp();
		//if (randomSkill[skillID].skillName == "移動速度提升")
		//	UpgradeMoveSpeed();
		//if (randomSkill[skillID].skillName == "經驗值範圍增加")
		//	UpgradeAbsorbExpRange();
		//if (randomSkill[skillID].skillName == "召喚獸數量增加")
		//	UpgradeSummonPet();
		#endregion
	}

	public void ItemEffectForBoss()
	{
		if (timer >= itemSkillSystem.itemData.skillHoldTime)
		{
			Debug.Log("BOSS道具效果消失");
			skillIcon.enabled = false;
			timer = 0;
		}
	}

	#region 技能升級方法
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

	[Header("主角鼠：玩家資料")]
	[SerializeField] DataBasic dataBasic;
	[SerializeField] DamagePlayer damagePlayer;
	[SerializeField] DamageBasic damageBasic;
	// 玩家血量提升
	private void UpgradePlayerHp()
	{
		int lv = dataSkill[2].skillLv - 1;
		dataBasic.hp = dataSkill[2].skillValues[lv];
		damagePlayer.hpBar.fillAmount = damageBasic.hpMax;
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

	[SerializeField, Header("主角鼠：召喚系統")]
	GameObject playerPet;
	// 經驗範圍增加
	private void UpgradeSummonPet()
	{
		int lv = dataSkill[5].skillLv - 1;
		int random = Random.Range(0, pointPetArray.Length);
		Vector3 posPet = pointPetArray[random].position;
		Instantiate(playerPet, posPet, Quaternion.identity);
	}
	#endregion
}