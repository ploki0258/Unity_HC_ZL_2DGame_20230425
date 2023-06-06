using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("�ͦ����j"), Range(0,10)]
    [SerializeField] float interval = 3.5f;         // �Ǫ��ͦ����j�ɶ�
    [Header("�Ǫ��w�s��")]
    [SerializeField] GameObject prefabEnemy = null; // �n�ͦ����Ǫ�

	private void Start()
	{
        // ���𭫽ƩI�s(�n���檺��k, �X���}�l, ���ƶ��j)
        InvokeRepeating("SpawnEnemy", 0f, interval);
	}

	/// <summary>
    /// �ͦ��Ǫ���k
    /// </summary>
    void SpawnEnemy()
    {
        Instantiate(prefabEnemy, transform.position, transform.rotation);
    }
}
