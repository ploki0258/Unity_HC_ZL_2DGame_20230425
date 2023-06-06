using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("���Y�O�D"), Range(1, 50)]
	[SerializeField] float force = 10f;
	[Header("���Y�y��")]
	[SerializeField] Vector2 pos;


	Rigidbody2D rig2D;

	private void Awake()
	{
		rig2D = GetComponent<Rigidbody2D>();
		rig2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
	}
}
