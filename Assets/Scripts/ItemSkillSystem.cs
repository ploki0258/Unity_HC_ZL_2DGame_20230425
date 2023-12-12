using UnityEngine;

public class ItemSkillSystem : ExpSystem
{
	[Header("BOSS道具技能資料")]
	public ItemData itemData;

	protected void Update()
	{
		TrackingPlayer();
	}
}
