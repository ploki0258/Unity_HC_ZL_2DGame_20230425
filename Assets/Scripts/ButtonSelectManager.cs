using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectManager : MonoBehaviour
{
	//[Header("按鈕編號"), Tooltip("所選擇的按鈕編號")]
	//public int indexSelectButton;
	[Header("最大選擇數量"), Range(0, 3), Tooltip("可選擇的最大數量")]
	public int maxSelectCount = 2;
	[Header("初始按鈕顏色")]
	public Color normalColor = Color.gray;
	[Header("選擇按鈕顏色")]
	public Color selectColor = new Color(1f, 1f, 0f);
	[Space(10)]
	[Header("按鈕選擇列表"), Tooltip("所存放的按鈕編號的列表")]
	public List<int> buttonSelectList = new List<int>();

	[Tooltip("該按鈕是否被選擇")]
	bool isSelected = false;

	/// <summary>
	/// 是否選擇按鈕
	/// </summary>
	/// <param name="idBtn">按鈕編號</param>
	/// <returns></returns>
	public bool SelectButton(int idBtn)
	{
		// 如果選擇的按鈕不包含在原本的列表裡 則回傳 true
		isSelected = !buttonSelectList.Contains(idBtn);
		return isSelected;
		/*if (isSelected == true)
		{
			//Debug.Log($"已選擇： {idBtn}");
			return true;
		}
		else
		{
			//Debug.Log($"未選擇： {idBtn}");
			return false;
		}*/
	}

	/// <summary>
	/// 未選擇的按鈕
	/// </summary>
	/*public bool DeselectButton(int idBtn)
	{
		Debug.Log($"未選擇： {idBtn}");
		isSelected = !buttonSelectList.Contains(idBtn);
		return isSelected;
	}*/

	/// <summary>
	/// 是否已達最大選擇數量
	/// </summary>
	/// <returns></returns>
	public bool IsSelectMax()
	{
		if (buttonSelectList.Count == maxSelectCount)
			return true;
		else
			return false;
	}

	/// <summary>
	/// 回傳已被選擇的按鈕數量
	/// </summary>
	/// <returns></returns>
	public int ButtonSelectCount()
	{
		int count = buttonSelectList.Count;
		return count;
	}

	public void ButtonIsSelect(int idBtn, bool select)
	{
		if (select == true)
		{
			//Debug.Log($"已添加至列表：{idBtn}");
			buttonSelectList.Add(idBtn);
		}
		else
		{
			//Debug.Log($"已從列表移除：{idBtn}");
			buttonSelectList.Remove(idBtn);
		}
	}
}
