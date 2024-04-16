using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
	[SerializeField][Header("ネΘ戈")] SpawnItem[] spawnItems;
	[SerializeField][Header("ネΘン")] GameObject[] spawnObjects = null;
	[SerializeField][Header("兵ン单")] int[] spawnLvArray;
	[SerializeField][Header("ネΘ丁筳")] float spawnInterval;

	LevelManager levelManager;
	//int index;

	private void Awake()
	{
		levelManager = GameObject.Find("à公").GetComponent<LevelManager>();
	}

	private void Start()
	{
		RepeatSpawnSystem();
	}

	private void Update()
	{

	}

	int id;
	/// <summary>
	/// ネΘ笵ㄣ
	/// </summary>
	void CreateItem()
	{

		// ど单糤ネΘ笵ㄣ
		for (int i = 0; i < spawnObjects.Length; i++)
		{
			if (levelManager.lv >= spawnLvArray[i])
				//                                        5 - (4 - 0)
				id = Random.Range(0, spawnObjects.Length - (4 - i));
		}
		/*if (levelManager.lv >= spawnLvArray[0])
			index = Random.Range(0, spawnObjects.Length - 4);
		else if (levelManager.lv >= spawnLvArray[1])
			index = Random.Range(0, spawnObjects.Length - 3);
		else if (levelManager.lv >= spawnLvArray[2])
			index = Random.Range(0, spawnObjects.Length - 2);
		else if (levelManager.lv >= spawnLvArray[3])
			index = Random.Range(0, spawnObjects.Length - 1);
		else if (levelManager.lv >= spawnLvArray[4])
			index = Random.Range(0, spawnObjects.Length);
		*/

		int x = Random.Range(0, spawnItems.Length);
		Vector3 originalVec = (Vector3)spawnItems[x].SpawnMapItemPos();
		//Collider2D[] collider2D = Physics2D.OverlapBoxAll(originalVec, Vector3.one, 0f);

		// ネΘ笵ㄣ
		if (levelManager.lv >= spawnLvArray[0])
		{
			GameObject tempObj = Instantiate(spawnObjects[id], originalVec, Quaternion.identity);

			// ⊿Τン
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
	/// ネΘ╰参狡ネΘ笵ㄣ
	/// </summary>
	void RepeatSpawnSystem()
	{
		InvokeRepeating("CreateItem", 0, spawnInterval);
	}

	/// <summary>
	/// 币ネΘ╰参
	/// </summary>
	void ReSpawn()
	{
		CancelInvoke();
		RepeatSpawnSystem();
	}
}