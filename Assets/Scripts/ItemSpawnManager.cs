using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
	[SerializeField][Header("�ͦ����")] SpawnItem[] spawnItems;
	[SerializeField][Header("�ͦ�����")] GameObject[] spawnObjects = null;
	[SerializeField][Header("���󵥯�")] int[] spawnLvArray;
	[SerializeField][Header("�ͦ����j")] float spawnInterval;

	LevelManager levelManager;
	//int index;

	private void Awake()
	{
		levelManager = GameObject.Find("�D����").GetComponent<LevelManager>();
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
	/// �ͦ��D��
	/// </summary>
	void CreateItem()
	{

		// �ɵ��ɼW�[�ͦ��D��
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

		// �ͦ��D��
		if (levelManager.lv >= spawnLvArray[0])
		{
			GameObject tempObj = Instantiate(spawnObjects[id], originalVec, Quaternion.identity);

			// �S������s�b�F
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
	/// �ͦ��t�ΡG���ƥͦ��D��
	/// </summary>
	void RepeatSpawnSystem()
	{
		InvokeRepeating("CreateItem", 0, spawnInterval);
	}

	/// <summary>
	/// ���ҥͦ��t��
	/// </summary>
	void ReSpawn()
	{
		CancelInvoke();
		RepeatSpawnSystem();
	}
}