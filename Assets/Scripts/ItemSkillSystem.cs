using System.Collections;
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
	public float itemExistTime;
	[SerializeField, Header("道具效果持續時間"), Range(0f, 50f)]
	public float effectHoldTime;

	//private DamageBasic damageBasic;
	//private WeaponSystem dataWeapon;

	protected override void Awake()
	{
		base.Awake();
		//damageBasic = player.GetComponent<DamageBasic>();
		//dataWeapon = player.GetComponentInChildren<WeaponSystem>();
		Destroy(this.gameObject, itemExistTime);
	}

	protected override void Update()
	{
		base.Update();
	}
	/*
	protected override void TrackingPlayer()
	{
		base.TrackingPlayer();
		if (distance <= distanceEat)
		{
			damageBasic.hp += hpRestore;

			foreach (GameObject tempWeapon in dataWeapon.prefabWeapon)
			{
				tempWeapon.GetComponent<Weapon>().critical += criticalImprove;
				tempWeapon.GetComponent<Weapon>().criticalHit += criticalHitImprove;
			}
		}
	}
	*/
}
