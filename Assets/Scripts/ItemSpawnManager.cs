using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
	[SerializeField][Header("生成資料")] SpawnItem[] spawnItems;
	[SerializeField][Header("生成物件")] GameObject[] spawnObjects = null;
	[SerializeField][Header("條件等級")] int[] spawnLvArray;
	[SerializeField][Header("生成間隔")] float spawnInterval;

	LevelManager levelManager;
	int index;

	private void Awake()
	{
		levelManager = GameObject.Find("主角鼠").GetComponent<LevelManager>();
	}

	private void Start()
	{
		SpawnSystem();
	}

	private void Update()
	{
		/*if (levelManager.lv >= spawnLvArray[0])
			ReSpawn();
		else if (levelManager.lv >= spawnLvArray[1])
			ReSpawn();
		else if (levelManager.lv >= spawnLvArray[2])
			ReSpawn();
		else if (levelManager.lv >= spawnLvArray[3])
			ReSpawn();
		else if (levelManager.lv >= spawnLvArray[4])
			ReSpawn();
		*/
	}

	/// <summary>
	/// 生成道具
	/// </summary>
	void CreateItem()
	{
		Vector3 originalVec;
		GameObject tempObj;

		if (levelManager.lv >= spawnLvArray[0])
			index = Random.Range(0, spawnObjects.Length - 4);
		else if (levelManager.lv >= spawnLvArray[1])
			index = Random.Range(0, spawnObjects.Length - 3);
		else if (levelManager.lv >= spawnLvArray[2])
			index = Random.Range(0, spawnObjects.Length - 2);
		else if (levelManager.lv >= spawnLvArray[3])
			index = Random.Range(0, spawnObjects.Length - 1);
		else if (levelManager.lv >= spawnLvArray[4])
			index = Random.Range(0, spawnObjects.Length);

		int x = Random.Range(0, spawnItems.Length);
		originalVec = (Vector3)spawnItems[x].SpawnMapItemPos();
		//Collider2D[] collider2D = Physics2D.OverlapBoxAll(originalVec, Vector3.one, 0f);

		// 生成 武器道具_炸彈
		if (levelManager.lv >= spawnLvArray[0])
		{
			tempObj = Instantiate(spawnObjects[index], originalVec, Quaternion.identity);

			// 沒有物件存在了
			/*if (collider2D.Length > 0)
			{
				bool foundEmptyPos = false;
				Vector3 newPos = Vector3.zero;

				while (!foundEmptyPos)
				{
					newPos = (Vector3)spawnItems[x].SpawnMapItemPos();
					Collider2D[] newColliders2D = Physics2D.OverlapBoxAll(newPos, Vector3.one, 0f);

					if (newColliders2D.Length == 0)
					{
						foundEmptyPos = true;
					}
				}

				tempObj = Instantiate(spawnObjects[0], newPos, Quaternion.identity);
			}
			else
			{
				tempObj = Instantiate(spawnObjects[0], originalVec, Quaternion.identity);
			}*/
		}
	}

	/// <summary>
	/// 生成系統：重複生成道具
	/// </summary>
	void SpawnSystem()
	{
		InvokeRepeating("CreateItem", 0, spawnInterval);
	}

	/// <summary>
	/// 重啟生成系統
	/// </summary>
	void ReSpawn()
	{
		CancelInvoke();
		SpawnSystem();
	}
}