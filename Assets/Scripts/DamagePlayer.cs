using UnityEngine;
using UnityEngine.UI;

public class DamagePlayer : DamageBasic
{
    [Header("血條")]
    public Image hpBar = null;

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
}
