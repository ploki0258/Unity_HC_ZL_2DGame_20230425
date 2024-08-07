﻿using System.Collections;
using UnityEngine;

public class ItemSkillSystem : ExpSystem
{
	[SerializeField, Header("恢復生命"), Range(0, 100)]
	public float hpRestore = 0;
	[SerializeField, Header("暴擊率提升"), Range(0, 50)]
	public float criticalImprove = 0;
	[SerializeField, Header("暴擊傷害提升"), Range(0, 50)]
	public float criticalHitImprove = 0;
	[SerializeField, Header("道具存在時間"), Range(0f, 50f)]
	public float itemExistTime = 10f;
	[SerializeField, Header("道具效果持續時間"), Range(0f, 50f)]
	public float effectHoldTime;

	private DamageBasic damageBasic;
	protected WeaponSystem dataWeapon;
	private SpriteRenderer spriteRenderer;
	private CircleCollider2D circleCollider;

	protected override void Awake()
	{
		base.Awake();
		damageBasic = player.GetComponent<DamageBasic>();
		dataWeapon = player.GetComponentInChildren<WeaponSystem>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		circleCollider = GetComponent<CircleCollider2D>();

		Destroy(this.gameObject, itemExistTime);
	}

	protected override void Update()
	{
		//base.Update();
		EatEffect(player.position, effectHoldTime, false);
	}

	protected override void EatEffect(Vector3 target, float delayTime = 0, bool quick = true)
	{
		base.EatEffect(target, delayTime, quick);
		distance = Vector3.Distance(transform.position, target);
		if (distance <= distanceEat)
		{
			spriteRenderer.enabled = false;
			circleCollider.enabled = false;

			// 回復血量
			damageBasic.hp += hpRestore;
			damageBasic.hp = Mathf.Clamp(damageBasic.hp, 0f, damageBasic.hpMax);
			Debug.Log("<color=green>已回復血量</color>");
			StartCoroutine(ItemEffect(criticalImprove, criticalHitImprove, effectHoldTime));
		}
	}

	/// <summary>
	/// 道具效果：
	/// 回復血量、增加暴擊率、暴擊傷害
	/// </summary>
	/// <param name="hpRestore">血量回復值</param>
	/// <param name="criticalImprove">暴擊率提升值</param>
	/// <param name="criticalHitImprove">暴擊傷害提升值</param>
	/// <param name="effectHoldTime">效果持續時間(秒)</param>
	/// <returns></returns>
	public IEnumerator ItemEffect(float criticalImprove, float criticalHitImprove, float effectHoldTime)
	{
		for (int i = 0; i < dataWeapon.prefabWeapon.Count; i++)
		{
			// 增加武器的暴擊率、暴擊傷害
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical += criticalImprove;
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical = Mathf.Clamp(dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical, 0f, 100f);
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().criticalHit += criticalHitImprove;
			Debug.Log("<color=green>已增加暴擊率、暴擊傷害</color>");
			// 恢復武器原本的暴擊率、暴擊傷害
			//dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical -= criticalImprove;
			//dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical = Mathf.Clamp(dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical, 0f, 100f);
			//dataWeapon.prefabWeapon[i].GetComponent<Weapon>().criticalHit -= criticalHitImprove;
		}

		// 效果持續時間(指定時間內效果有效)
		yield return new WaitForSeconds(effectHoldTime);

		for (int i = 0; i < dataWeapon.prefabWeapon.Count; i++)
		{
			// 恢復武器原本的暴擊率、暴擊傷害
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical -= criticalImprove;
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical = Mathf.Clamp(dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical, 0f, 100f);
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().criticalHit -= criticalHitImprove;
			Debug.Log("<color=green>已恢復暴擊率、暴擊傷害</color>");
		}
	}
}
