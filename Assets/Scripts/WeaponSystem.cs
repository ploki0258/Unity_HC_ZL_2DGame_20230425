using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Header("�ͦ����j"), Range(0, 10)]
    [SerializeField] float interval = 3.5f;         // �Z���ͦ����j�ɶ�
    [Header("�Z���w�s��")]
    [SerializeField] GameObject[] prefabWeapon = null; // �n�ͦ����Z��
    [Header("�Z���̤j�ƶq")]
    [SerializeField] int weaponMax;

    private void Start()
	{
        InvokeRepeating("SpawnWeapon", 0f, interval);
    }

    /// <summary>
    /// �ͦ��Z����k
    /// </summary>
    void SpawnWeapon()
    {
        int i = Random.Range(0, weaponMax);
        Instantiate(prefabWeapon[i], transform.position, transform.rotation);
        // prefabWeapon[i].GetComponent<Transform>().
    }
}
