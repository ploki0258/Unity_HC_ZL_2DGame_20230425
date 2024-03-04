using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTest : MonoBehaviour
{
	public bossSkillEffect effectBossState
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
	bossSkillEffect _effectBossState;

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
			case bossSkillEffect.�L:
				Debug.Log("�L�ĪG");
				itemBossDescription = "�L�ĪG";
				break;
			case bossSkillEffect.�F��V��:
				Debug.Log("�����ĤH��^�_�ۨ�10%��q");
				itemBossDescription = "�����ĤH��^�_�ۨ�10%��q";
				break;
			case bossSkillEffect.�W�����m:
				itemBossDescription = "���|����������";
				break;
			case bossSkillEffect.�j�O����:
				itemBossDescription = "�l���d�򤺪��Ҧ��D��";
				break;
			case bossSkillEffect.���F���:
				itemBossDescription = "��q�k�s�ɡA���@�w���v����_��";
				break;
			case bossSkillEffect.�L�ɲ`�W:
				itemBossDescription = "�H�ۨ������߽d�򤺪��ĤH�������`";
				break;
			case bossSkillEffect.�t�~���{:
				itemBossDescription = "�i�H�l��@���t�~���������ĤH";
				break;
			case bossSkillEffect.�ʹ�����:
				itemBossDescription = "��o�g��ȮɡA�B�~�W�[10%�g���";
				break;
			case bossSkillEffect.�Ŭr���N:
				itemBossDescription = "�C���a��������ɡA�����O����10%";
				break;
		}
	}
}
