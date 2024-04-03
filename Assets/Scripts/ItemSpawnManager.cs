using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
	[SerializeField][Header("�ͦ��ϰ�")] BoxCollider2D[] spawnAreas = new BoxCollider2D[0];
	[SerializeField][Header("�ͦ�����")] GameObject[] spawnObjects = null;
	[SerializeField][Header("���󵥯�")] int spawnLv;
	[SerializeField][Header("�ͦ����j")] float spawnInterval;

	LevelManager levelManager;
	float width_1, width_2, width_3, width_4,
		  hight_1, hight_2, hight_3, hight_4;

	private void Awake()
	{
		levelManager = GameObject.Find("�D����").GetComponent<LevelManager>();
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
			//Debug.Log("�i�J�ͦ�");
			Instantiate(spawnObjects[0], spawnAreas[0].transform.position, Quaternion.identity);
		}
	}
}
