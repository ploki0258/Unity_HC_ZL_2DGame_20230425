using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data Item", menuName = "Add New ItemBossData")]
public class ItemData : ScriptableObject
{
	[Header("道具圖示")]
	public Sprite iconItem;
	[SerializeField, Header("Boss道具效果類型")]
	ItemEffectBossState effectBossState = ItemEffectBossState.無;
	[Header("技能持續時間"), Range(0f, 20f)]
	public float skillHoldTime;
	[Header("道具存在時間"), Range(0f, 20f)]
	public float itemExistTime;
}

/// <summary>
/// 在一定持續時間內效果有效
/// 1.靈魂汲取：擊殺敵人後回復自身10%血量
/// 2.超絕防禦：不會受到任何攻擊
/// 3.萬有引力：吸收範圍內的所有道具
/// 4.神靈轉生：血量歸零時，有一定機率滿血復活
/// 5.無盡深淵：以自身為中心範圍內的敵人直接死亡
/// </summary>
public enum ItemEffectBossState
{
	無, 靈魂汲取, 超絕防禦, 萬有引力, 神靈轉生, 無盡深淵
}
