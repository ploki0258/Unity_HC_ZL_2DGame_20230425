using UnityEngine;

public class ItemSystem : ExpSystem
{
	[Header("技能持續時間")]
	public float skillHoldTime;
	[Header("BOSS技能道具資料")]
	public ItemData itemData;

	private float timer = 0;

	protected void Update()
	{
		Debug.Log("Funtion");
		Funtion();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name.Contains("主角鼠"))
		{
			levelManager.skillIcon.enabled = true;
			levelManager.skillIcon.sprite = itemData.iconItem;
		}
	}

	public void Funtion()
	{
		Debug.Log("已執行");
		timer += Time.deltaTime;

		if (timer <= skillHoldTime)
		{
			Debug.Log("發動BOSS道具技能");
		}
		else
		{
			levelManager.skillIcon.enabled = false;
		}
	}
}
