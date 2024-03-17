using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// 玩家技能
/// </summary>
public class SkillPlayer : MonoBehaviour
{
	#region 欄位
	[Header("說明介面")]
	public GameObject gpDiscripen;
	[Header("說明標題")]
	public TextMeshProUGUI titleDescription;
	[Header("說明圖片")]
	public Image imageDescription;
	[Header("道具說明")]
	public TextMeshProUGUI ItemDescription;
	[Header("BOSS技能種類")]
	public bossSkillEffect itemEffectBossState = bossSkillEffect.無;
	[SerializeField, Header("BOSS道具資料")]
	ItemData itemData = null;
	//[SerializeField] CircleCollider2D colliderEat = null;
	[SerializeField] float criticalImprove, criticalHitImprove, effectHoldTime;

	[SerializeField] DamageBasic damageBasic;
	[SerializeField] WeaponSystem weaponSystem;
	[SerializeField] ItemSkillSystem itemSkillSystem;
	float distance;
	float distanceEat;
	#endregion

	private void Awake()
	{
		damageBasic = GetComponent<DamageBasic>();
		weaponSystem = GetComponentInChildren<WeaponSystem>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name.Contains("道具"))
		{
			itemSkillSystem = collision.GetComponent<ItemSkillSystem>();

			criticalImprove = itemSkillSystem.criticalImprove;
			criticalHitImprove = itemSkillSystem.criticalHitImprove;
			effectHoldTime = itemSkillSystem.effectHoldTime;
			distanceEat = itemSkillSystem.distanceEat;

			distance = Vector3.Distance(transform.position, collision.transform.position);

			if (distance < distanceEat)
			{
				// 回復HP
				damageBasic.hp += itemSkillSystem.hpRestore;
				StartCoroutine(ItemEffect());
			}
		}
	}

	public void SkillBossDiscripen()
	{
		if (gpDiscripen.activeInHierarchy == true)
			return;

		titleDescription.text = itemData.bossSkillEffect.ToString();
		imageDescription.sprite = itemData.iconSkill;
		gpDiscripen.SetActive(true);
	}

	/// <summary>
	/// 道具效果：
	/// 增加暴擊率、暴擊傷害
	/// </summary>
	/// <returns></returns>
	private IEnumerator ItemEffect()
	{
		Debug.Log("已吃到道具");
		for (int i = 0; i < weaponSystem.prefabWeapon.Length; i++)
		{
			// 增加武器的暴擊率、暴擊傷害
			weaponSystem.prefabWeapon[i].GetComponent<Weapon>().critical += criticalImprove;
			weaponSystem.prefabWeapon[i].GetComponent<Weapon>().critical = Mathf.Clamp(weaponSystem.prefabWeapon[i].GetComponent<Weapon>().critical, 0f, 100f);
			weaponSystem.prefabWeapon[i].GetComponent<Weapon>().criticalHit += criticalHitImprove;

			// 持續指定時間(指定時間內效果有效)
			yield return new WaitForSeconds(effectHoldTime);

			// 恢復武器原本的暴擊率、暴擊傷害
			weaponSystem.prefabWeapon[i].GetComponent<Weapon>().critical -= criticalImprove;
			weaponSystem.prefabWeapon[i].GetComponent<Weapon>().critical = Mathf.Clamp(weaponSystem.prefabWeapon[i].GetComponent<Weapon>().critical, 0f, 100f);
			weaponSystem.prefabWeapon[i].GetComponent<Weapon>().criticalHit -= criticalHitImprove;
		}
		/*
		for (int i = 0; i < weaponSystem.prefabWeapon.Length; i++)
		{
			// 恢復武器原本的暴擊率、暴擊傷害
			weaponSystem.prefabWeapon[i].GetComponent<Weapon>().critical -= criticalImprove;
			weaponSystem.prefabWeapon[i].GetComponent<Weapon>().critical = Mathf.Clamp(weaponSystem.prefabWeapon[i].GetComponent<Weapon>().critical, 0f, 100f);
			weaponSystem.prefabWeapon[i].GetComponent<Weapon>().criticalHit -= criticalHitImprove;
		}*/
	}

	/*
	#region BOSS道具技能方法
	// 靈魂汲取：擊殺敵人後回復自身10%血量
	[Space(50f)]
	
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
	*/
}
