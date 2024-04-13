using TMPro;
using UnityEngine;

public class LevelWindows : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI textTipLV = null;

	LevelManager levelManager;

	private void Awake()
	{
		levelManager = GameObject.Find("¥D¨¤¹«").GetComponent<LevelManager>();
	}

	private void Start()
	{
		textTipLV.alpha = 0;
	}

	private void Update()
	{
		ShwoLvTip();
	}

	void ShwoLvTip()
	{
		if (levelManager.lv >= 10)
		{
			textTipLV.alpha = 1;
		}
		else
		{
			textTipLV.alpha = 0;
		}
	}
}
