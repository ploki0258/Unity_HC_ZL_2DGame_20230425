using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
	[Header("生成怪物系統陣列")]
	public SpawnSystem[] spawnSystems;
	[Header("生成怪物資料")]
	public DataSpawnEnemy[] dataSpawnEnemys;
	[Header("生成點陣列")]
	public Transform[] spawnPointArray;
	[Header("生成BOSS技能道具")]
	public GameObject itemBoss;

	private float timer;
	private int index;

	private void Update()
	{
		ChangeSpawEnemy();
	}

	/// <summary>
	/// 改變生成波數怪物
	/// </summary>
	private void ChangeSpawEnemy()
	{
		timer += Time.deltaTime;

		if (index >= dataSpawnEnemys.Length)
			return;

		// 生成下一波怪物
		if (timer >= dataSpawnEnemys[index].timeToSpawn)
		{
			// 生成BOSS
			if (index == dataSpawnEnemys.Length - 1)
			{
				int random = Random.Range(0, spawnSystems.Length);
				Vector3 pos = spawnSystems[random].transform.position;
				Instantiate(dataSpawnEnemys[index].prefabEnemy, pos, Quaternion.identity);
				index++;

				SpawBossSkillItem();
				return;
			}

			for (int i = 0; i < spawnSystems.Length; i++)
			{
				spawnSystems[i].prefabEnemy = dataSpawnEnemys[index].prefabEnemy;
				spawnSystems[i].interval = dataSpawnEnemys[index].intervalSpawn;
				spawnSystems[i].Restart();
			}

			index++;
			// print("生成波數：" + index);
		}
	}

	/// <summary>
	/// 生成BOSS技能道具
	/// </summary>
	private void SpawBossSkillItem()
	{
		int random = Random.Range(0, spawnPointArray.Length);
		float width = Random.Range(0f, 22f);
		float height = Random.Range(0f, 22f);

		Vector3 posA = spawnPointArray[random].transform.position;
		posA.x = width;
		posA.y = height;
		Vector3 posB = new Vector3(posA.x, posA.y, posA.z);

		if (dataSpawnEnemys[index].prefabEnemy)
		{
			Instantiate(itemBoss, posB, Quaternion.identity);
		}
	}
}
