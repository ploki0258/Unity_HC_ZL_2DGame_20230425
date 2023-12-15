using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTest : MonoBehaviour
{
	public ItemEffectBossState effectBossState
	{
		get
		{
			return _effectBossState;
		}
		set
		{
			_effectBossState = value;
			//UpdateItemBossDescription();
		}
	}
	[SerializeField, Header("Boss道具效果類型")]
	ItemEffectBossState _effectBossState;

	[Header("Boss道具效果說明")]
	public string itemBossDescription;

	private void Update()
	{
		//print("Boss道具效果類型：" + _effectBossState);
		UpdateItemBossDescription();
	}

	private void UpdateItemBossDescription()
	{
		switch (effectBossState)
		{
			case ItemEffectBossState.無:
				Debug.Log("無效果");
				itemBossDescription = "無效果";
				break;
			case ItemEffectBossState.靈魂汲取:
				Debug.Log("擊殺敵人後回復自身10%血量");
				itemBossDescription = "擊殺敵人後回復自身10%血量";
				break;
			case ItemEffectBossState.超絕防禦:
				itemBossDescription = "不會受到任何攻擊";
				break;
			case ItemEffectBossState.萬有引力:
				itemBossDescription = "吸收範圍內的所有道具";
				break;
			case ItemEffectBossState.神靈轉生:
				itemBossDescription = "血量歸零時，有一定機率滿血復活";
				break;
			case ItemEffectBossState.無盡深淵:
				itemBossDescription = "以自身為中心範圍內的敵人直接死亡";
				break;
			case ItemEffectBossState.聖獸降臨:
				itemBossDescription = "可以召喚一隻聖獸幫忙攻擊敵人";
				break;
			case ItemEffectBossState.睿智之心:
				itemBossDescription = "獲得經驗值時，額外增加10%經驗值";
				break;
			case ItemEffectBossState.不屈鬥士:
				itemBossDescription = "每當玩家受到攻擊時，攻擊力提升10%";
				break;
		}
	}
}
