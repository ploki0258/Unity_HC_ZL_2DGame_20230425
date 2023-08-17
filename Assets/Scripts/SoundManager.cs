using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [Header("升級")]
    public AudioClip soundLvUp;
    [Header("升級技能")]
    public AudioClip soundSkillLvUp;
    [Header("玩家受傷")]
    public AudioClip soundPlayerHurt;
    [Header("玩家死亡")]
    public AudioClip soundPlayerDead;
    [Header("怪物受傷")]
    public AudioClip soundEnemyHurt;
    [Header("怪物死亡")]
    public AudioClip soundEnemyDead;
    [Header("發射武器")]
    public AudioClip soundFireWeapon;

    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
	/// 音效播放功能
	/// </summary>
	/// <param name="sound">播放的音效</param>
	/// <param name="min">音量最小值</param>
	/// <param name="max">音量最大值</param>
	public void PlaySound(AudioClip sound, float min, float max)
    {
        // 隨機範圍
        float volume = Random.Range(min, max);
        // 音效元件 的 撥放一次音效(音效, 音量)
        aud.PlayOneShot(sound, volume);
    }
}
