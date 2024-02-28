using UnityEngine;
using UnityEngine.UI;

public class PlayCtrl : MonoBehaviour
{
	[Header("玩家資料")]
	public DataBasic dataPlayer;
	[Header("參數名稱")]
	public string parAniName = "runSwitch";

	[HideInInspector]
	public Rigidbody2D rig = null;	public Animator ani = null;

	private void Awake()
	{
		// Debug.Log($"{10+20}");

		rig = GetComponent<Rigidbody2D>();
		ani = GetComponent<Animator>();
	}

	private void Start()
	{
		// Debug.Log("<color=yellow>開始事件</color>");
	}

	private void Update()
	{
		// Debug.Log("<color=red>更新事件</color>");
		Move();
	}

	/// <summary>
	/// 角色移動
	/// </summary>
	private void Move()
	{
		float ad = Input.GetAxisRaw("Horizontal");
		float ws = Input.GetAxisRaw("Vertical");

		// 角色移動
		rig.velocity = new Vector2(ad * dataPlayer.moveSpeed, ws * dataPlayer.moveSpeed);

		// 移動動畫
		ani.SetBool(parAniName, (ws != 0 || ad != 0));

		// 翻轉
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			transform.eulerAngles = new Vector3(0, 0, 0);
		}
	}
}
