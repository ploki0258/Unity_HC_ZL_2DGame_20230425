﻿using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data Item", menuName = "Add New ItemBossData")]
public class ItemData : ScriptableObject
{
	[Header("道具名稱")]
	public string iconName;
	[Header("道具圖示")]
	public Sprite iconItem;
	public ItemEffectBossState effectBossState
	{
		get { return _effectBossState; }
		set
		{
			_effectBossState = value;
			UpdateItemBossDescription();
		}
	}
	[Header("Boss道具效果類型")]
	public ItemEffectBossState _effectBossState = ItemEffectBossState.無;
	[Header("Boss道具效果描述"), TextArea(2, 5)]
	public string itemBossDescription;
	[Header("技能持續時間"), Range(0f, 20f)]
	public float skillHoldTime;
	//[Header("道具存在時間"), Range(0f, 20f)]
	//public float itemExistTime;

	private void Reset()
	{
		//UpdateItemBossDescription();
	}

	private void UpdateItemBossDescription()
	{
		switch (effectBossState)
		{
			case ItemEffectBossState.無:
				itemBossDescription = "無效果";
				break;
			case ItemEffectBossState.靈魂汲取:
				itemBossDescription = "擊殺敵人後回復自身10%血量";
				break;
			case ItemEffectBossState.超絕防禦:
				itemBossDescription = "不會受到任何攻擊";
				break;
			case ItemEffectBossState.強力打擊:
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
			case ItemEffectBossState.巫毒之術:
				itemBossDescription = "每當玩家受到攻擊時，攻擊力提升10%";
				break;
		}
	}
}

/// <summary>
/// 在一定持續時間內效果有效
/// 1.靈魂汲取：擊殺敵人後回復自身10%血量			(心臟)
/// 2.睿智之心：獲得經驗值時，額外增加10%經驗值		(骷髏)
/// 3.超絕防禦：不會受到任何攻擊					(蝸牛)
/// 4.強力打擊：當對怪物攻擊時，降低怪物的攻擊力	(暈眩)
/// 5.神靈轉生：血量歸零時，有一定機率滿血復活		(十字架)
/// 6.巫毒之術：當玩家被攻擊時，使怪物受到相同的傷害	(巫毒娃娃)
/// 7.聖獸降臨：可以召喚一隻聖獸幫忙攻擊敵人		()
/// 8.無盡深淵：以自身為中心範圍內的敵人直接死亡	()
/// </summary>
public enum ItemEffectBossState
{
	無, 靈魂汲取, 睿智之心, 超絕防禦, 強力打擊, 神靈轉生, 巫毒之術, 聖獸降臨, 無盡深淵
}