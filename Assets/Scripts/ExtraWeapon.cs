using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraWeapon : MonoBehaviour
{
	[SerializeField][Header("�����Z��")] float distanceEat = 1f;
	[SerializeField][Header("�s�b�ɶ�")] float existTime = 0f;

	Transform player;
	WeaponSystem weaponSystem;

	private void Awake()
	{
		player = GameObject.Find("�D����").GetComponent<Transform>();
		weaponSystem = GameObject.FindObjectOfType<WeaponSystem>();
	}

	private void Start()
	{
		Destroy(this.gameObject, existTime);
	}

	private void Update()
	{
		if (EatWeaponItem())
		{
			weaponSystem.AddWeaponToList(WeaponType.���u.ToString());
			Destroy(this.gameObject, 0.1f);
		}
	}

	bool EatWeaponItem()
	{
		float distance = Vector3.Distance(transform.position, player.position);
		if (distance <= this.distanceEat)
		{
			return true;
		}
		return false;
	}
}
