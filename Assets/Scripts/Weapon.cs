using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("投擲力道"), Range(1, 50)]
	[SerializeField] float force = 10f;
	[Header("投擲座標")]
	[SerializeField] Vector2 pos;
	[SerializeField, Header("暴擊率")]
	public float critical;
	[SerializeField, Header("暴擊傷害")]
	public float criticalHit = 2;
	public float attack;

	Rigidbody2D rig2D;

	private void Awake()
	{
		rig2D = GetComponent<Rigidbody2D>();
		rig2D.AddForce(pos * force);

		Destroy(gameObject, 5f);
	}
}
