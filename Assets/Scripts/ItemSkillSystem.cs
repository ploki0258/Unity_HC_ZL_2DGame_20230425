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
	[SerializeField]
	ItemData itemData = null;

	private DamageBasic damageBasic;
	private WeaponSystem dataWeapon;
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
		base.Update();
	}

	protected override void TrackingPlayer()
	{
		base.TrackingPlayer();
		if (distance <= distanceEat)
		{
			spriteRenderer.enabled = false;
			circleCollider.enabled = false;

			damageBasic.hp += hpRestore;
			StartCoroutine(ItemEffect(criticalImprove, criticalHitImprove));
			DestroyObject(this.gameObject, effectHoldTime, true);
		}
	}

	/// <summary>
	/// 道具效果：
	/// 增加暴擊率、暴擊傷害
	/// </summary>
	/// <returns></returns>
	public IEnumerator ItemEffect(float criticalImprove, float criticalHitImprove)
	{
		Debug.Log("<color=green>已吃到道具</color>");
		for (int i = 0; i < dataWeapon.prefabWeapon.Length; i++)
		{
			// 增加武器的暴擊率、暴擊傷害
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical += criticalImprove;
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical = Mathf.Clamp(dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical, 0f, 100f);
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().criticalHit += criticalHitImprove;

			// 效果持續時間(指定時間內效果有效)
			yield return new WaitForSeconds(effectHoldTime);

			// 恢復武器原本的暴擊率、暴擊傷害
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical -= criticalImprove;
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical = Mathf.Clamp(dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical, 0f, 100f);
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().criticalHit -= criticalHitImprove;
		}
		/*
		for (int i = 0; i < dataWeapon.prefabWeapon.Length; i++)
		{
			// 恢復武器原本的暴擊率、暴擊傷害
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical -= criticalImprove;
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical = Mathf.Clamp(dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical, 0f, 100f);
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().criticalHit -= criticalHitImprove;
		}*/
	}
}
