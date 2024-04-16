using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 玩家受傷系統：
/// 1.玩家扣血
/// 2.玩家死亡 / 勝利
/// 3.刷新血條顯示
/// </summary>
public class DamagePlayer : DamageBasic
{
	[Header("血條")]
	public Image hpBar = null;
	[Header("玩家控制器")]
	public PlayCtrl playCtrl;
	[Header("武器生成系統")]
	public WeaponSystem weaponSystem;
	[ContextMenuItem("Open/Close", "OpenWindows")]
	[Header("結束介面")]
	public GameObject gpFinal;
	[Header("結束標題")]
	public TextMeshProUGUI textFinal;

	private void Start()
	{
		hpBar.fillAmount = hpMax;
		hpChangeAction += HpBarChange;
		// 測試用
		//Damage(50);
	}

	private void OnDisable()
	{
		hpChangeAction -= HpBarChange;
	}

	/// <summary>
	/// 刷新血條顯示
	/// 當Hp發生變化時
	/// </summary>
	void HpBarChange()
	{
		hpBar.fillAmount = _hp / hpMax;
	}

	public override void Damage(float damage)
	{
		base.Damage(damage);

		// 播放玩家受傷音效
		AudioClip sound = SoundManager.instance.soundPlayerHurt;
		SoundManager.instance.PlaySound(sound);
		Debug.Log("玩家血量：" + _hp);
		//hpBar.fillAmount = _hp / hpMax;
	}

	/// <summary>
	/// 玩家死亡功能
	/// </summary>
	protected override void Dead()
	{
		base.Dead();

		// 播放玩家死亡音效
		AudioClip sound = SoundManager.instance.soundPlayerDead;
		SoundManager.instance.PlaySound(sound);

		// 終止玩家控制器
		playCtrl.enabled = false;
		// 停止武器生成系統
		weaponSystem.Stop();

		if (gpFinal.activeInHierarchy == true)
			return;

		textFinal.text = "You are Dead";
		gpFinal.SetActive(true);
	}

	/// <summary>
	/// 玩家勝利功能
	/// </summary>
	public void Win()
	{
		// 播放玩家勝利音效
		AudioClip sound = SoundManager.instance.soundPlayerVictory;
		SoundManager.instance.PlaySound(sound);

		// 終止玩家控制器
		playCtrl.enabled = false;
		// 停止武器生成系統
		weaponSystem.Stop();

		if (gpFinal.activeInHierarchy == true)
			return;

		textFinal.text = "You are Win";
		gpFinal.SetActive(true);
	}

	// 右鍵自訂功能
	void OpenWindows()
	{
		//print("測試訊息...");
		if (gpFinal.activeSelf == false)
			gpFinal.SetActive(true);
		else
			gpFinal.SetActive(false);
	}
}
