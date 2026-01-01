using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトル画面を管理するクラス.
/// </summary>
public class TitleManager : MonoBehaviour
{
    const float SLIDE_DURATION = 0.3f;

    [SerializeField]
    Button StartButton;

    [SerializeField]
    Button SettingsButton;

    [SerializeField]
    Button ExitButton;

    [SerializeField]
    RectTransform SettingsPanel;

    float m_SettingsPanelWidth;
    bool m_IsSettingsOpen = false;

    void Start()
    {
        StartButton.onClick.AddListener(OnStartButtonClicked);
        SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        ExitButton.onClick.AddListener(OnExitButtonClicked);

        // 設定パネルの幅を取得して画面外に配置.
        m_SettingsPanelWidth = SettingsPanel.rect.width;
        SettingsPanel.anchoredPosition = new Vector2(m_SettingsPanelWidth, 0f);
    }

    /// <summary>
    /// スタートボタン押下時の処理.
    /// メインゲームシーンへ遷移する.
    /// </summary>
    void OnStartButtonClicked()
    {
        FadeManager.GetInstance().FadeOutAndLoadScene(GameManager.SCENE_TYPE.MAIN_GAME);
    }

    /// <summary>
    /// 設定ボタン押下時の処理.
    /// 設定パネルの開閉を切り替える.
    /// </summary>
    void OnSettingsButtonClicked()
    {
        if (m_IsSettingsOpen)
        {
            CloseSettingsPanel();
        }
        else
        {
            OpenSettingsPanel();
        }
    }

    /// <summary>
    /// 終了ボタン押下時の処理.
    /// アプリケーションを終了する.
    /// </summary>
    void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// 設定パネルを開く.
    /// </summary>
    public void OpenSettingsPanel()
    {
        m_IsSettingsOpen = true;
        SettingsPanel.DOAnchorPosX(0f, SLIDE_DURATION).SetEase(Ease.OutQuad);
    }

    /// <summary>
    /// 設定パネルを閉じる.
    /// </summary>
    public void CloseSettingsPanel()
    {
        m_IsSettingsOpen = false;
        SettingsPanel.DOAnchorPosX(m_SettingsPanelWidth, SLIDE_DURATION).SetEase(Ease.OutQuad);
    }
}
