using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data Item", menuName = "Add New ItemBossData")]
public class ItemData : ScriptableObject
{
    [Header("道具圖示")]
    public Sprite iconItem;
}
