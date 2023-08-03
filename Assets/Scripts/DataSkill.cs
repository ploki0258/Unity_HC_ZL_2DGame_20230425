using UnityEngine;

[CreateAssetMenu(fileName = "Data Skill", menuName = "Add New Skill")]
public class DataSkill : ScriptableObject
{
	[Header("技能名稱")]
	public string skillName;
	[Header("技能圖示")]
	public Sprite skillPicture;
	[Header("技能描述"), TextArea(3, 8)]
	public string skillDescription;
	[Header("技能等級")]
	public int skillLv = 1;
	[Header("技能數值")]
	public float[] skillValues;
}
