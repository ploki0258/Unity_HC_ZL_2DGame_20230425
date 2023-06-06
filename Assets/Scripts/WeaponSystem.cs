using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Header("生成間隔"), Range(0, 10)]
    [SerializeField] float interval = 3.5f;         // 武器生成間隔時間
    [Header("武器預製物")]
    [SerializeField] GameObject[] prefabWeapon = null; // 要生成的武器
    [Header("武器最大數量")]
    [SerializeField] int weaponMax;

    private void Start()
	{
        InvokeRepeating("SpawnWeapon", 0f, interval);
    }

    /// <summary>
    /// 生成武器方法
    /// </summary>
    void SpawnWeapon()
    {
        int i = Random.Range(0, weaponMax);
        Instantiate(prefabWeapon[i], transform.position, transform.rotation);
        // prefabWeapon[i].GetComponent<Transform>().
    }
}
