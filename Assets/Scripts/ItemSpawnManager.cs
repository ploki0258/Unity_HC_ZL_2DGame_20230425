using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
	[SerializeField][Header("生成資料")] SpawnItem[] spawnItems;
	[SerializeField][Header("生成物件")] GameObject[] spawnObjects = null;
	[SerializeField][Header("條件等級")] int spawnLv;
	[SerializeField][Header("生成間隔")] float spawnInterval;

	LevelManager levelManager;

	private void Awake()
	{
		levelManager = GameObject.Find("主角鼠").GetComponent<LevelManager>();
	}

	private void Start()
	{
		InvokeRepeating("CreateItem", 0, spawnInterval);
	}

	void CreateItem()
	{
		if (levelManager.lv >= spawnLv)
		{
			//Debug.Log("進入生成");
			int x = Random.Range(0, spawnItems.Length);
			Instantiate(spawnObjects[0], spawnItems[x].SpawnMapItemPos(), Quaternion.identity);
		}
	}
}
