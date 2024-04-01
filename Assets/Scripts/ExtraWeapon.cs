using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraWeapon : MonoBehaviour
{
    [SerializeField][Header("®ø¥¢¶ZÂ÷")] float distance = 1f;
    
	Transform player;
	WeaponSystem weaponSystem;

	private void Awake()
	{
		player = GameObject.Find("¥D¨¤¹«").GetComponent<Transform>();
		weaponSystem = GameObject.FindObjectOfType<WeaponSystem>();
	}

	private void Update()
	{
		if (EatWeaponItem())
		{
			weaponSystem.AddWeaponToList(WeaponType.¬µ¼u.ToString());
			Destroy(this.gameObject, 0.1f);
		}
	}

	bool EatWeaponItem()
	{
		float distance = Vector3.Distance(transform.position, player.position);
		if (distance <= this.distance)
		{
			return true;
		}
		return false;
	}
}
