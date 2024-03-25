using UnityEngine;

public class HandBomb : MonoBehaviour
{
	#region 欄位
	[SerializeField, Header("爆炸範圍")] float rangeExplode = 0;
	[SerializeField, Header("爆炸傷害")] float damageExplode = 0;
	[SerializeField, Header("道具存在時間"), Range(0f, 50f)]
	float itemExistTime;
	#endregion

	private void Awake()
	{
		//Destroy(gameObject, itemExistTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			//Destroy(gameObject);
		}
	}
}
