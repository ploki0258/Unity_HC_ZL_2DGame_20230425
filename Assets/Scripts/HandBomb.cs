using UnityEngine;

public class HandBomb : Weapon
{
	#region ���
	[SerializeField, Header("�z���d��")] float rangeExplode = 0;
	[SerializeField, Header("�z���ˮ`")] float damageExplode = 0;
	#endregion

	private void Awake()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		
	}
}
