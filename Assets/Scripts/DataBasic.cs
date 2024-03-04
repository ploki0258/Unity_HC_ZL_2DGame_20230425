using UnityEngine;

[CreateAssetMenu(fileName = "Data Basic", menuName = "Menu/Add New Data")]
public class DataBasic : ScriptableObject
{
    [ContextMenuItem("ResetInitial", "ResetData")]
    [Header("血量"), Range(1,10000)]
    public float hp;
    [Header("攻擊力"), Range(1, 1000)]
    public float attack;
    [Header("移動速度"), Range(1, 100)]
    public float moveSpeed;

    void ResetData()
    {
        hp = 200f;
        attack = 50f;
        moveSpeed = 10f;
	}
}
