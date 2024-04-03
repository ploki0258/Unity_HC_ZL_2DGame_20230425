using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
	[SerializeField][Header("生成區域")] BoxCollider2D[] spawnAreas = new BoxCollider2D[0];
	[SerializeField][Header("生成物件")] GameObject[] spawnObjects = null;
	[SerializeField][Header("條件等級")] int spawnLv;
	[SerializeField][Header("生成間隔")] float spawnInterval;

	LevelManager levelManager;
	float width_1, width_2, width_3, width_4,
		  hight_1, hight_2, hight_3, hight_4;

	private void Awake()
	{
		levelManager = GameObject.Find("主角鼠").GetComponent<LevelManager>();
	}

	private void Start()
	{
		width_1 = spawnAreas[0].size.x;
		hight_1 = spawnAreas[0].size.y;

		width_2 = spawnAreas[1].size.x;
		hight_2 = spawnAreas[1].size.y;

		width_3 = spawnAreas[2].size.x;
		hight_3 = spawnAreas[2].size.y;

		width_4 = spawnAreas[3].size.x;
		hight_4 = spawnAreas[3].size.y;

		InvokeRepeating("GenerateItem", 0, spawnInterval);
	}

	void GenerateItem()
	{
		if (levelManager.lv >= spawnLv)
		{
			//Debug.Log("進入生成");
			Instantiate(spawnObjects[0], spawnAreas[0].transform.position, Quaternion.identity);
		}
	}
}
