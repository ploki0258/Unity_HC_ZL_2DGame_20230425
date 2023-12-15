using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTest : MonoBehaviour
{
	public ItemEffectBossState effectBossState
	{
		get
		{
			return _effectBossState;
		}
		set
		{
			_effectBossState = value;
			//UpdateItemBossDescription();
		}
	}
	[SerializeField, Header("Boss�D��ĪG����")]
	ItemEffectBossState _effectBossState;

	[Header("Boss�D��ĪG����")]
	public string itemBossDescription;

	private void Update()
	{
		//print("Boss�D��ĪG�����G" + _effectBossState);
		UpdateItemBossDescription();
	}

	private void UpdateItemBossDescription()
	{
		switch (effectBossState)
		{
			case ItemEffectBossState.�L:
				Debug.Log("�L�ĪG");
				itemBossDescription = "�L�ĪG";
				break;
			case ItemEffectBossState.�F��V��:
				Debug.Log("�����ĤH��^�_�ۨ�10%��q");
				itemBossDescription = "�����ĤH��^�_�ۨ�10%��q";
				break;
			case ItemEffectBossState.�W�����m:
				itemBossDescription = "���|����������";
				break;
			case ItemEffectBossState.�U���ޤO:
				itemBossDescription = "�l���d�򤺪��Ҧ��D��";
				break;
			case ItemEffectBossState.���F���:
				itemBossDescription = "��q�k�s�ɡA���@�w���v����_��";
				break;
			case ItemEffectBossState.�L�ɲ`�W:
				itemBossDescription = "�H�ۨ������߽d�򤺪��ĤH�������`";
				break;
			case ItemEffectBossState.�t�~���{:
				itemBossDescription = "�i�H�l��@���t�~���������ĤH";
				break;
			case ItemEffectBossState.�ʹ�����:
				itemBossDescription = "��o�g��ȮɡA�B�~�W�[10%�g���";
				break;
			case ItemEffectBossState.���}���h:
				itemBossDescription = "�C���a��������ɡA�����O����10%";
				break;
		}
	}
}
