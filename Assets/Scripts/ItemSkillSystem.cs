using System.Collections;
using UnityEngine;

public class ItemSkillSystem : ExpSystem
{
	[Header("BOSS道具技能資料")]
	public ItemData itemData;
	[Header("恢復生命"), Range(0, 20)]
	float hpRestore = 0;
	[Header("暴擊率提升")]
	float criticalImprove = 0;
	[Header("暴擊傷害提升")]
	float criticalHitImprove = 0;

	float timer = 0f;
	private DamagePlayer dataPlayer;
	private WeaponSystem dataWeapon;

	private void Awake()
	{
		dataPlayer = player.GetComponent<DamagePlayer>();
		dataWeapon = player.GetComponentInChildren<WeaponSystem>();
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer >= itemData.itemExistTime)
		{
			timer = 0f;
			Destroy(gameObject);
		}
	}

	public override void TrackingPlayer()
	{
		base.TrackingPlayer();

		dataPlayer.hp += hpRestore;
		StartCoroutine(ItemEffect());
	}

	private IEnumerator ItemEffect()
	{
		for (int i = 0; i < dataWeapon.prefabWeapon.Length; i++)
		{
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical += criticalImprove;
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().criticalHit += criticalHitImprove;
		}

		yield return new WaitForSeconds(itemData.skillHoldTime);

		for (int i = 0; i < dataWeapon.prefabWeapon.Length; i++)
		{
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().critical -= criticalImprove;
			dataWeapon.prefabWeapon[i].GetComponent<Weapon>().criticalHit -= criticalHitImprove;
		}
	}
}
