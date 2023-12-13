using UnityEngine;

public class ItemSkillSystem : ExpSystem
{
	[Header("BOSS道具技能資料")]
	public ItemData itemData;

	protected void Update()
	{
		TrackingPlayer();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name.Contains("主角鼠"))
		{
			if (Vector3.Distance(transform.position, player.position) <= 1f)
			{
				
			}
		}
	}
}
