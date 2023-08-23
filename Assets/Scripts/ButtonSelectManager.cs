using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectManager : MonoBehaviour
{
	[Header("按鈕編號"), Tooltip("所選擇的按鈕編號")]
	public int indexSelectButton;
	[Header("按鈕狀態"), Tooltip("該按鈕是否被選擇")]
	public bool isSelected = false;

	//[Header("按鈕編號列表"), Tooltip("所存放的按鈕編號的列表")]
	//public List<int> buttonList = new List<int>();

	/// <summary>
	/// 已選擇按鈕
	/// </summary>
	public void SelectButton()
	{
		isSelected = true;
		Debug.Log("已選擇");
	}

	/// <summary>
	/// 取消選擇的按鈕
	/// </summary>
	public void DeselectButton()
	{
		isSelected = false;
		Debug.Log("取消選擇");
	}

	/// <summary>
	/// 回傳按鈕的選擇狀態
	/// </summary>
	/// <returns></returns>
	public bool IsSelected()
	{
		return isSelected;
	}

	/// <summary>
	/// 切換按鈕的選擇狀態
	/// </summary>
	public void SwitchSelected()
	{
		isSelected = !isSelected;
	}
}
