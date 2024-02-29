using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamagePlayer : DamageBasic
{
	[Header("血條")]
	public Image hpBar = null;
	[Header("玩家控制器")]
	public PlayCtrl playCtrl;
	[Header("武器生成系統")]
	public WeaponSystem weaponSystem;
	[ContextMenuItem("Open\"Close", "OpenWindows")]
	[Header("結束介面")]
	public GameObject gpFinal;
	[Header("結束標題")]
	public TextMeshProUGUI textFinal;

	private void Start()
	{
		hpBar.fillAmount = hpMax;
		// Damage(100);
	}

	public override void Damage(float damage)
	{
		base.Damage(damage);

		// 播放玩家受傷音效
		AudioClip sound = SoundManager.instance.soundPlayerHurt;
		SoundManager.instance.PlaySound(sound);

		hpBar.fillAmount = hp / hpMax;
	}

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

	public void Win()
	{
		if (gpFinal.activeInHierarchy == true)
			return;

		textFinal.text = "You are Win";
		gpFinal.SetActive(true);
	}

	void OpenWindows()
	{
		print("測試訊息...");
		if (gpFinal.activeSelf == false)
			gpFinal.SetActive(true);
		else
			gpFinal.SetActive(false);
	}
}
