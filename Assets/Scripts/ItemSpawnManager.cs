using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
	[SerializeField][Header("�ͦ����")] SpawnItem[] spawnItems;
	[SerializeField][Header("�ͦ�����")] GameObject[] spawnObjects = null;
	[SerializeField][Header("���󵥯�")] int spawnLv;
	[SerializeField][Header("�ͦ����j")] float spawnInterval;

	LevelManager levelManager;

	private void Awake()
	{
		levelManager = GameObject.Find("�D����").GetComponent<LevelManager>();
	}

	private void Start()
	{
		InvokeRepeating("CreateItem", 0, spawnInterval);
	}

	void CreateItem()
	{
		if (levelManager.lv >= spawnLv)
		{
			//Debug.Log("�i�J�ͦ�");
			int x = Random.Range(0, spawnItems.Length);
			Instantiate(spawnObjects[0], spawnItems[x].SpawnMapItemPos(), Quaternion.identity);
		}
	}
}
