using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// シーン選択メニューを管理するクラス.
/// </summary>
public class SceneSelectMenu : MonoBehaviour
{
    [SerializeField]
    GameManager.SCENE_TYPE NextScene;

    [SerializeField]
    Button StartButton;

    [SerializeField]
    Button GameButton;

    [SerializeField]
    Button ExitButton;

    void Start()
    {
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(OnStartButtonClicked);
        }

        if (GameButton != null)
        {
            GameButton.onClick.AddListener(OnGameButtonClicked);
        }

        if (ExitButton != null)
        {
            ExitButton.onClick.AddListener(OnExitButtonClicked);
        }
    }

    /// <summary>
    /// スタートボタン押下時の処理.
    /// 指定されたシーンへ遷移する.
    /// </summary>
    void OnStartButtonClicked()
    {
        FadeManager.GetInstance().FadeOutAndLoadScene(NextScene);
    }

    /// <summary>
    /// ゲームボタン押下時の処理.
    /// ゲームシーンへ遷移する.
    /// </summary>
    void OnGameButtonClicked()
    {
        FadeManager.GetInstance().FadeOutAndLoadScene(GameManager.SCENE_TYPE.GAME);
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
}
