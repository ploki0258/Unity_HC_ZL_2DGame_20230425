using UnityEngine;

[CreateAssetMenu(fileName = "Data Enemy", menuName = "Menu/Add New EnemyData")]
public class DataEnemy : DataBasic
{
    [Header("掉落經驗機率"), Range(0, 1)]
    public float expProbability;
    [Header("經驗值物件")]
    public GameObject prefabExp;
    [Header("攻擊範圍"), Range(0, 10)]
    public float attackRange = 2f;
    [Header("攻擊間隔"), Range(0, 5)]
    public float attackInterval = 2.5f;
}
