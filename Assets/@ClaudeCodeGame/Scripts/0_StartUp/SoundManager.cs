using UnityEngine;

/// <summary>
/// サウンド管理クラス.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    static SoundManager m_Instance;

    AudioSource m_AudioSource;

    float m_SEVolume = 1.0f;

    /// <summary>
    /// インスタンスの取得.
    /// </summary>
    public static SoundManager GetInstance()
        => m_Instance;

    void Awake()
    {
        // シングルトンの設定.
        if (m_Instance == null)
        {
            m_Instance = this;
            m_AudioSource = GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// BGM を再生.
    /// </summary>
    /// <param name="_Clip">再生する BGM.</param>
    public void PlayBGM(AudioClip _Clip)
    {
        m_AudioSource.clip = _Clip;
        m_AudioSource.loop = true;
        m_AudioSource.Play();
    }

    /// <summary>
    /// BGM を停止.
    /// </summary>
    public void StopBGM()
    {
        m_AudioSource.Stop();
    }

    /// <summary>
    /// SE を再生.
    /// </summary>
    /// <param name="_AudioSource">再生に使用する AudioSource.</param>
    /// <param name="_Clip">再生する SE.</param>
    public void PlaySE(AudioSource _AudioSource, AudioClip _Clip)
    {
        _AudioSource.PlayOneShot(_Clip, m_SEVolume);
    }

    /// <summary>
    /// BGM 音量の設定.
    /// </summary>
    /// <param name="_Volume">音量 (0.0 ~ 1.0).</param>
    public void SetBGMVolume(float _Volume)
    {
        m_AudioSource.volume = Mathf.Clamp01(_Volume);
    }

    /// <summary>
    /// SE 音量の設定.
    /// </summary>
    /// <param name="_Volume">音量 (0.0 ~ 1.0).</param>
    public void SetSEVolume(float _Volume)
    {
        m_SEVolume = Mathf.Clamp01(_Volume);
    }

    /// <summary>
    /// BGM 音量の取得.
    /// </summary>
    public float GetBGMVolume()
        => m_AudioSource.volume;

    /// <summary>
    /// SE 音量の取得.
    /// </summary>
    public float GetSEVolume()
        => m_SEVolume;
}
