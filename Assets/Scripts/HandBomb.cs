using UnityEngine;

public class HandBomb : Weapon
{
	#region Äæ¦ì
	[SerializeField, Header("Ãz¬µ½d³ò")] float rangeExplode = 0;
	[SerializeField, Header("Ãz¬µ¶Ë®`")] float damageExplode = 0;
	#endregion

	private void Awake()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		
	}
}
