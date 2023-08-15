using UnityEngine;

[CreateAssetMenu(fileName = "Data Spawn Enemy", menuName = "Add New Spawn Enemy Data")]
public class DataSpawnEnemy : ScriptableObject
{
	[Header("這波怪物生成的時間")]
	public float timeToSpawn;
	[Header("要生成的怪物")]
	public GameObject prefabEnemy;
	[Header("生成怪物的間隔"), Range(0, 5)]
	public float intervalSpawn;
}
