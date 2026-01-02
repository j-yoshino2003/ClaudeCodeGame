using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// チュートリアルダイアログを管理するクラス.
/// </summary>
public class TutorialDialog : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI MessageText;

    [SerializeField]
    Button NextButton;

    [SerializeField]
    Button SkipButton;

    [SerializeField]
    string[] Messages;

    int m_CurrentIndex = 0;

    void Start()
    {
        if (Messages == null || Messages.Length == 0)
        {
            GoToGame();
            return;
        }

        NextButton.onClick.AddListener(OnNextButtonClicked);
        SkipButton.onClick.AddListener(OnSkipButtonClicked);
        ShowCurrentMessage();
    }

    /// <summary>
    /// 現在のメッセージを表示する.
    /// </summary>
    void ShowCurrentMessage()
    {
        MessageText.text = Messages[m_CurrentIndex];
    }

    /// <summary>
    /// 次へボタン押下時の処理.
    /// </summary>
    void OnNextButtonClicked()
    {
        m_CurrentIndex++;

        if (m_CurrentIndex >= Messages.Length)
        {
            GoToGame();
            return;
        }

        ShowCurrentMessage();
    }

    /// <summary>
    /// スキップボタン押下時の処理.
    /// </summary>
    void OnSkipButtonClicked()
    {
        GoToGame();
    }

    /// <summary>
    /// ゲームシーンへ遷移する.
    /// </summary>
    void GoToGame()
    {
        FadeManager.GetInstance().FadeOutAndLoadScene(GameManager.SCENE_TYPE.GAME);
    }
}
