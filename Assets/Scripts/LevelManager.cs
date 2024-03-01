using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

/// <summary>
/// 等級系統：管理角色的升級技能
/// </summary>
//[ExecuteInEditMode] // 在編輯模式中也能運行Play模式的功能
public class LevelManager : MonoBehaviour
{
	#region 欄位
	[SerializeField, Header("經驗值")]
	Image imgExp;
	[SerializeField, Header("文字等級")]
	TextMeshProUGUI textLv;
	[SerializeField, Header("文字經驗值")]
	TextMeshProUGUI textExp;
	[ContextMenuItem("Open\"Close", "OpenWindows")] // 在Inspector中右鍵可執行自定義方法
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

	[HideInInspector]
	public int lv = 1;      // 等級
	private float exp = 0;  // 經驗值
	public float timer = 0; // 計時器
	private ButtonSelectManager btnSelect;
	public ItemSkillSystem itemSkillSystem;
	#endregion

	void OpenWindows()
	{
		print("測試訊息...");
		if (goLvUp.activeInHierarchy == false)
			goLvUp.SetActive(true);
		else
			goLvUp.SetActive(false);
	}

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
		else if (collision.name.Contains("BOSS"))
		{
			collision.GetComponent<ItemSkillSystem>().enabled = true;
		}
	}

	private void Awake()
	{
		btnSelect = FindObjectOfType<ButtonSelectManager>();
		itemSkillSystem = FindObjectOfType<ItemSkillSystem>();
		skillIcon.enabled = false;
	}

	private void Start()
	{
		imgExp.fillAmount = 0f;                 // 歸零經驗條
		textLv.text = "LV " + lv.ToString();    // 恢復玩家起始等級

		// 恢復技能起始等級
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
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			ClickCloseButton();

#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
		{
			// AddExp(100);

			btnSelect.buttonSelectList.Clear();

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
		btnClose.SetActive(false);
		btnConfirm.SetActive(false);
		Time.timeScale = 0f;

		// 挑選全部技能資料中 等級小於10的技能
		randomSkill = dataSkill.Where(skill => skill.skillLv < 10).ToList();
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
			colorButton.GetComponent<Image>().color = btnSelect.normalColor;
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

		foreach (int skillID in btnSelect.buttonSelectList)
		{
			randomSkill[skillID].skillLv++; // 技能等級+1

			#region 執行技能效果
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
			#endregion
		}

		btnSelect.buttonSelectList.Clear(); // 清空列表
		goLvUp.SetActive(false);            // 關閉介面
		Time.timeScale = 1f;                // 恢復時間
	}

	//[Tooltip("最大選擇數量")]
	//private int maxSelectCount = 2;
	//[Tooltip("是否已達最大選擇數量")]
	//private bool maxSelect = false;

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
		Color colorBtn = goSkillUI[skillID].GetComponent<Image>().color;

		// 只要還有技能的話 就一直執行
		if (randomSkill.Count > 0)
		{
			// 如果等級 小於 10
			if (lv < 10)
			{
				// 如果已有選擇了
				if (btnSelect.buttonSelectList.Count == btnSelect.maxSelectCount - 1)
				{
					// 如果選擇的按鈕是其他按鈕的話 就不執行
					if (btnSelect.SelectButton(skillID))
					{
						return;
					}
					// 如果選擇的按鈕是自己的話 就從列表中移除
					else
					{
						btnSelect.ButtonIsSelect(skillID, btnSelect.SelectButton(skillID));
						//btnSelect.buttonSelectList.Remove(skillID);   // 從列表中移除
						goSkillUI[skillID].GetComponent<Image>().color = btnSelect.normalColor;
					}
				}
				// 均未選擇
				else
				{
					// 如果選擇的按鈕不包含在原本的列表裡 就添加至列表中 顏色變為黃色
					if (btnSelect.SelectButton(skillID))
					{
						Debug.Log($"<color=#9966ff>點擊技能編號：{skillID}</color>");
						btnSelect.ButtonIsSelect(skillID, btnSelect.SelectButton(skillID));
						//btnSelect.buttonSelectList.Add(skillID);  // 添加至列表
						goSkillUI[skillID].GetComponent<Image>().color = btnSelect.selectColor;
					}
					// 否則就移除 顏色恢復為灰色
					else
					{
						btnSelect.ButtonIsSelect(skillID, btnSelect.SelectButton(skillID));
						//btnSelect.buttonSelectList.Remove(skillID);   // 從列表中移除
						goSkillUI[skillID].GetComponent<Image>().color = btnSelect.normalColor;
					}
				}

				// 如果已有選擇技能 就顯示確認按鈕
				if (btnSelect.buttonSelectList.Count == 0)
					btnConfirm.SetActive(false);
				else
					btnConfirm.SetActive(true);
			}
			// 否則等級 大於等於 10
			else
			{
				//Debug.Log($"<color=orange>玩家等級已達{lv}等</color>");
				// 如果按鈕列表中的數量 等於 最大選擇數量 就顯示確認按鈕
				if (btnSelect.IsSelectMax())
				{
					// 如果選擇的按鈕是其他按鈕的話 就不執行
					if (btnSelect.SelectButton(skillID))
					{
						return;
					}
					// 否則選擇的按鈕是自己的話 就從列表中移除 顏色恢復為灰色
					else
					{
						btnSelect.ButtonIsSelect(skillID, btnSelect.SelectButton(skillID));   // 從列表中移除
						goSkillUI[skillID].GetComponent<Image>().color = btnSelect.normalColor;
					}
				}
				// 否則未達最大選擇數量的話 (選擇1個或以下的情況)
				else
				{
					// 如果選中的按鈕 不在列表中 則添加至列表中 顏色變為黃色
					if (btnSelect.SelectButton(skillID))
					{
						btnSelect.ButtonIsSelect(skillID, btnSelect.SelectButton(skillID));   // 添加至列表
						goSkillUI[skillID].GetComponent<Image>().color = btnSelect.selectColor;
					}
					// 否則選中的按鈕 在列表之中 從列表中移除 顏色恢復為灰色
					else
					{
						btnSelect.ButtonIsSelect(skillID, btnSelect.SelectButton(skillID));  // 從列表中移除
						goSkillUI[skillID].GetComponent<Image>().color = btnSelect.normalColor;
					}
				}

				// 如果隨機技能列表 大於等於 2的話
				if (randomSkill.Count > 1)
				{
					// 如果已達最大選擇數量 就顯示確認按鈕
					if (btnSelect.IsSelectMax())
						btnConfirm.SetActive(true);
					else
						btnConfirm.SetActive(false);
				}
				// 否則隨機技能列表 小於等於 1的話
				else
				{
					// 如果按鈕選擇列表 大於等於 1的話 就顯示確認按鈕
					if (btnSelect.buttonSelectList.Count > 0)
						btnConfirm.SetActive(true);
					else
						btnConfirm.SetActive(false);
				}
			}
		}
		/*
		// 如果按鈕列表中的數量 等於 最大選擇數量的話 或只剩下一種技能時 就顯示確認按鈕
		if (btnSelect.buttonSelectList.Count == maxSelectCount || randomSkill.Count < 2)
		{
			maxSelect = true;   // 是否已達最大選擇數量 = true

			btnConfirm.SetActive(true);
		}
		else
		{
			maxSelect = false;  // 是否已達最大選擇數量 = false

			btnConfirm.SetActive(false);
		}

		// 如果已達最大選擇數量的話 (已選擇2個的情況)
		if (maxSelect)
		{
			// 如果是未選的按鈕的話 就不執行
			if (!btnSelect.buttonSelectList.Contains(skillID))
			{
				return;
			}
			// 否則就從列表中移除 顏色變為灰色
			else
			{
				btnSelect.buttonSelectList.Remove(skillID);   // 從列表中移除
				goSkillUI[skillID].GetComponent<Image>().color = btnSelect.normalColor;
			}
		}
		// 否則未達最大選擇數量的話 (選擇1個或以下的情況)
		else
		{
			// 如果選中按鈕時 按鈕已被選擇 從列表中移除 顏色變為灰色
			if (btnSelect.buttonSelectList.Contains(skillID))
			{
				btnSelect.buttonSelectList.Remove(skillID);   // 從列表中移除
				goSkillUI[skillID].GetComponent<Image>().color = btnSelect.normalColor;
			}
			// 否則選中按鈕時 按鈕未被選 添加至列表中 顏色變為黃色
			else
			{
				btnSelect.buttonSelectList.Add(skillID);  // 添加至列表
				goSkillUI[skillID].GetComponent<Image>().color = btnSelect.selectColor;
			}
		}*/

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
		playerMoveSpeed.dataPlayer.moveSpeed = dataSkill[3].skillValues[lv];
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