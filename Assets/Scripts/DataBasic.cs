using UnityEngine;

[CreateAssetMenu(fileName = "Data Basic", menuName = "Add New Data")]
public class DataBasic : ScriptableObject
{
    [Header("血量"), Range(1,10000)]
    public float hp;
    [Header("攻擊力"), Range(1, 1000)]
    public float attack;
    [Header("移動速度"), Range(1, 100)]
    public float moveSpeed;
    /*[Header("掉落經驗機率")]
    public float aaa;
    [Header("經驗值物件")]
    public GameObject Experience;
    */
}
