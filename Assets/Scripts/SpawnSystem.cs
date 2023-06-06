using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("生成間隔"), Range(0,10)]
    [SerializeField] float interval = 3.5f;         // 怪物生成間隔時間
    [Header("怪物預製物")]
    [SerializeField] GameObject prefabEnemy = null; // 要生成的怪物

	private void Start()
	{
        // 延遲重複呼叫(要執行的方法, 幾秒後開始, 重複間隔)
        InvokeRepeating("SpawnEnemy", 0f, interval);
	}

	/// <summary>
    /// 生成怪物方法
    /// </summary>
    void SpawnEnemy()
    {
        Instantiate(prefabEnemy, transform.position, transform.rotation);
    }
}
