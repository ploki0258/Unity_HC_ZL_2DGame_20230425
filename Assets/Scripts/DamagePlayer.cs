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
		hpBar.fillAmount = hp / hpMax;
	}

	protected override void Dead()
	{
		base.Dead();
		// 終止玩家控制器
		playCtrl.enabled = false;
		// 停止武器生成系統
		weaponSystem.Stop();

		textFinal.text = "You are Dead";
		gpFinal.SetActive(true);
	}

	public void Win()
	{
		textFinal.text = "You are Win";
		gpFinal.SetActive(true);
	}
}
