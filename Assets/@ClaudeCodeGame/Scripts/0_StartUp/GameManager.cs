using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体を管理するクラス.
/// </summary>
public class GameManager : MonoBehaviour
{
    static GameManager m_Instance;

    /// <summary>
    /// インスタンスの取得.
    /// </summary>
    public static GameManager GetInstance()
        => m_Instance;

    /// <summary>
    /// シーンの種類.
    /// </summary>
    public enum SCENE_TYPE
    {
        START_UP,   // 起動時の初期化シーン
        TITLE,      // タイトル画面シーン
        MAIN_GAME,  // メインゲームシーン
        GAME,       // ゲームプレイシーン
        RESULT      // リザルト画面シーン
    }

    void Awake()
    {
        // シングルトンの設定.
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);

            // タイトルシーンへ遷移.
            LoadScene(SCENE_TYPE.TITLE);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// シーンタイプを指定して遷移.
    /// </summary>
    /// <param name="_scene_type">遷移先のシーンタイプ.</param>
    public void LoadScene(SCENE_TYPE _scene_type)
    {
        SceneManager.LoadScene((int)_scene_type);
    }
}
