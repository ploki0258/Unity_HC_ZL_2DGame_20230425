using UnityEngine;

[CreateAssetMenu(fileName = "Data Basic", menuName = "Add New Data")]
public class DataBasic : ScriptableObject
{
    [Header("��q"), Range(1,10000)]
    public float hp;
    [Header("�����O"), Range(1, 1000)]
    public float Attack;
    [Header("���ʳt��"), Range(1, 100)]
    public float moveSpeed;
    /*[Header("�����g����v")]
    public float aaa;
    [Header("�g��Ȫ���")]
    public GameObject Experience;
    */
}
