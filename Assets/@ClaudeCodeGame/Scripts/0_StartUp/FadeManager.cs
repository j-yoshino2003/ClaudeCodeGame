using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// フェード演出を管理するクラス.
/// </summary>
[RequireComponent(typeof(Image))]
public class FadeManager : MonoBehaviour
{
    const float ALPHA_TRANSPARENT = 0f;
    const float ALPHA_OPAQUE = 1f;

    static FadeManager m_Instance;

    Image m_FadeImage;

    [SerializeField]
    float m_FadeDuration = 0.5f;

    bool m_IsFading = false;

    /// <summary>
    /// インスタンスの取得.
    /// </summary>
    public static FadeManager GetInstance()
    {
        return m_Instance;
    }

    void Awake()
    {
        // シングルトンの設定.
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
            m_FadeImage = GetComponent<Image>();

            // シーン読み込み完了時のコールバック登録.
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // コールバック解除.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene _Scene, LoadSceneMode _Mode)
    {
        FadeIn();
    }

    /// <summary>
    /// フェードイン (黒 → 透明).
    /// </summary>
    /// <param name="_OnComplete">完了時のコールバック.</param>
    public void FadeIn(Action _OnComplete = null)
    {
        if (m_IsFading)
        {
            return;
        }

        m_FadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeCoroutine(ALPHA_OPAQUE, ALPHA_TRANSPARENT, () =>
        {
            m_FadeImage.gameObject.SetActive(false);
            _OnComplete?.Invoke();
        }
        ));
    }

    /// <summary>
    /// フェードアウトしてシーン遷移 (透明 → 黒 → シーン遷移).
    /// </summary>
    /// <param name="_scene_type">遷移先のシーンタイプ.</param>
    public void FadeOutAndLoadScene(GameManager.SCENE_TYPE _scene_type)
    {
        if (m_IsFading)
        {
            return;
        }

        m_FadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeCoroutine(ALPHA_TRANSPARENT, ALPHA_OPAQUE, () =>
        {
            GameManager.GetInstance().LoadScene(_scene_type);
        }
        ));
    }

    IEnumerator FadeCoroutine(float _From, float _To, Action _OnComplete)
    {
        m_IsFading = true;
        float elapsed = 0f;
        Color color = m_FadeImage.color;

        while (true)
        {
            if (elapsed >= m_FadeDuration)
            {
                break;
            }

            elapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(_From, _To, Mathf.Clamp01(elapsed / m_FadeDuration));
            color.a = alpha;
            m_FadeImage.color = color;
            yield return null;
        }

        color.a = _To;
        m_FadeImage.color = color;
        m_IsFading = false;

        _OnComplete?.Invoke();
    }
}
