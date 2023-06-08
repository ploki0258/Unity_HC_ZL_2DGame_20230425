using UnityEngine;

[CreateAssetMenu(fileName = "Data Enemy", menuName = "Add New EnemyData")]
public class DataEnemy : DataBasic
{
    [Header("掉落經驗機率"), Range(0, 1)]
    public float expProbability;
    [Header("經驗值物件")]
    public GameObject prefabExp;
}
