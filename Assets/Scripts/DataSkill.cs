using UnityEngine;

[CreateAssetMenu(fileName = "Data Skill", menuName = "Add New Skill")]
public class DataSkill : ScriptableObject
{
	[Header("�ޯ�W��")]
	public string skillName;
	[Header("�ޯ�ϥ�")]
	public Sprite skillPicture;
	[Header("�ޯ�y�z"), TextArea(3, 8)]
	public string skillDescription;
	[Header("�ޯ൥��")]
	public int skillLv = 1;
	[Header("�ޯ�ƭ�")]
	public float[] skillValues;
}
