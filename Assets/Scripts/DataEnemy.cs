using UnityEngine;

[CreateAssetMenu(fileName = "Data Enemy", menuName = "Add New EnemyData")]
public class DataEnemy : DataBasic
{
    [Header("�����g����v"), Range(0, 1)]
    public float expProbability;
    [Header("�g��Ȫ���")]
    public GameObject prefabExp;
}
