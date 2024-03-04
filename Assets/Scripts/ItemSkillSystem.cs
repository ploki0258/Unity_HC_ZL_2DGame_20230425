using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSkillSystem : ExpSystem
{
	[SerializeField, Header("恢復生命"), Range(0, 100)]
	float hpRestore = 0;
	[SerializeField, Header("暴擊率提升"), Range(0, 50)]
	float criticalImprove = 0;
	[SerializeField, Header("暴擊傷害提升"), Range(0, 50)]
	float criticalHitImprove = 0;
	[SerializeField, Header("道具存在時間"), Range(0f, 50f)]
	float itemExistTime;
	[SerializeField, Header("BOSS道具技能資料")]
	ItemData itemData;

	private DamageBasic damageBasic;
	private WeaponSystem dataWeapon;

	protected override void Awake()
	{
		base.Awake();
		damageBasic = player.GetComponent<DamageBasic>();
		dataWeapon = player.GetComponentInChildren<WeaponSystem>();
		Destroy(this.gameObject, itemExistTime);
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void TrackingPlayer()
	{
		base.TrackingPlayer();
		if (distance <= distanceEat)
		{
			damageBasic.Damage(-hpRestore);
		}
		StartCoroutine(ItemEffect());
	}

	private IEnumerator ItemEffect()
	{
		// 增加武器的暴擊率、暴擊傷害
		for (int i = 0; i < dataWeapon.prefabWeapon.Length; i++)
		{
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical += criticalImprove;
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().criticalHit += criticalHitImprove;
		}

		// 持續指定時間(指定時間內效果有效)
		yield return new WaitForSeconds(itemData.skillHoldTime);

		// 恢復武器原本的暴擊率、暴擊傷害
		for (int i = 0; i < dataWeapon.prefabWeapon.Length; i++)
		{
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical -= criticalImprove;
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().criticalHit -= criticalHitImprove;
		}
	}
}
