using UnityEngine;

public class HandBomb : MonoBehaviour
{
	#region ���
	[SerializeField, Header("�z���d��")] float rangeExplode = 0;
	[SerializeField, Header("�z���ˮ`")] float damageExplode = 0;
	[SerializeField, Header("�D��s�b�ɶ�"), Range(0f, 50f)]
	float itemExistTime;
	#endregion

	private void Awake()
	{
		//Destroy(gameObject, itemExistTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			//Destroy(gameObject);
		}
	}
}
