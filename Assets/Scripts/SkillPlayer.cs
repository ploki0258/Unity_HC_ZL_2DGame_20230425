using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPlayer : MonoBehaviour
{
	[Header("說明介面")]
	public GameObject gpDiscripen;
	[Header("說明標題")]
	public TextMeshProUGUI textDescription;
	[Header("技能說明")]
	public TextMeshProUGUI textSkillDescription;
	[Header("說明圖片")]
	public Image imageDescription;
	[Header("BOSS技能種類")]
	public bossSkillEffect itemEffectBossState = bossSkillEffect.無;
	[SerializeField, Header("BOSS道具資料")]
	ItemData itemData = null;

	private void Update()
	{
		switch (itemEffectBossState)
		{
			case bossSkillEffect.無:
				//Debug.Log("無效果");
				break;

			case bossSkillEffect.靈魂汲取:
				//Debug.Log("執行" + itemEffectBossState.ToString() + "效果");
				break;

			case bossSkillEffect.超絕防禦:
				//Debug.Log("執行" + itemEffectBossState.ToString() + "效果");
				break;

			case bossSkillEffect.強力打擊:
				//Debug.Log("執行" + itemEffectBossState.ToString() + "效果");
				break;

			case bossSkillEffect.神靈轉生:
				//Debug.Log("執行" + itemEffectBossState.ToString() + "效果");
				break;

			case bossSkillEffect.無盡深淵:
				//Debug.Log("執行" + itemEffectBossState.ToString() + "效果");
				break;

			case bossSkillEffect.聖獸降臨:
				//Debug.Log("執行" + itemEffectBossState.ToString() + "效果");
				break;

			case bossSkillEffect.睿智之心:
				//Debug.Log("執行" + itemEffectBossState.ToString() + "效果");
				break;

			case bossSkillEffect.巫毒之術:
				//Debug.Log("執行" + itemEffectBossState.ToString() + "效果");
				break;
		}
	}

	public void SkillBossDiscripen()
	{
		if (gpDiscripen.activeInHierarchy == true)
			return;

		textDescription.text = itemData.bossSkillEffect.ToString();
		imageDescription.sprite = itemData.iconSkill;
		gpDiscripen.SetActive(true);
	}

	#region BOSS道具技能方法
	// 靈魂汲取：擊殺敵人後回復自身10%血量
	[Header("主角鼠：玩家資料")]
	[SerializeField] DataBasic dataBasicPlater;
	private void 靈魂汲取()
	{
		
	}

	// 超絕防禦：不會受到任何攻擊
	[SerializeField] DamagePlayer damagePlayer;
	[SerializeField] DamageBasic damageBasic;
	private void 超絕防禦()
	{
		
	}

	[Header("主角鼠：玩家資料")]
	[SerializeField] DataBasic dataBasic;
	// 強力打擊：吸收範圍內的所有道具
	private void 萬有引力()
	{
		
	}

	// 神靈轉生：血量歸零時，有一定機率滿血復活
	private void 神靈轉生()
	{

	}

	// 無盡深淵：以自身為中心範圍內的敵人直接死亡
	private void 無盡深淵()
	{

	}

	// 聖獸降臨：可以召喚一隻聖獸幫忙攻擊敵人
	private void 聖獸降臨()
	{

	}

	// 睿智之心：獲得經驗值時，額外增加10%經驗值
	private void 睿智之心()
	{

	}

	// 不屈鬥士：每當玩家受到攻擊時，攻擊力提升10%
	private void 不屈鬥士()
	{

	}
	#endregion
}
