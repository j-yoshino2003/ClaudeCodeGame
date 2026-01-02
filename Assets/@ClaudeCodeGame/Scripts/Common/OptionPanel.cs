using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// オプションパネルを管理するクラス.
/// </summary>
public class OptionPanel : MonoBehaviour
{
    const float SLIDE_DURATION = 0.3f;

    [SerializeField]
    Slider BGMSlider;

    [SerializeField]
    Slider SESlider;

    [SerializeField]
    TMP_Dropdown ResolutionDropdown;

    [SerializeField]
    Button OpenButton;

    [SerializeField]
    Button CloseButton;

    RectTransform m_RectTransform;
    float m_PanelWidth;
    List<Resolution> m_Resolutions = new List<Resolution>();

    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();

        // パネルの幅を取得して画面外に配置.
        m_PanelWidth = m_RectTransform.rect.width;
        m_RectTransform.anchoredPosition = new Vector2(m_PanelWidth, 0f);

        InitializeVolumeSliders();
        InitializeResolutionDropdown();

        OpenButton.onClick.AddListener(Open);
        CloseButton.onClick.AddListener(Close);
    }

    /// <summary>
    /// オプションパネルを開く.
    /// </summary>
    public void Open()
    {
        m_RectTransform.DOKill();
        m_RectTransform.DOAnchorPosX(0f, SLIDE_DURATION).SetEase(Ease.OutQuad);
    }

    /// <summary>
    /// オプションパネルを閉じる.
    /// </summary>
    public void Close()
    {
        m_RectTransform.DOKill();
        m_RectTransform.DOAnchorPosX(m_PanelWidth, SLIDE_DURATION).SetEase(Ease.OutQuad);
    }

    /// <summary>
    /// オプションパネルの開閉を切り替える.
    /// </summary>
    public void Toggle()
    {
        // 現在位置で開閉状態を判断.
        if (m_RectTransform.anchoredPosition.x < m_PanelWidth)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    /// <summary>
    /// 音量スライダーの初期化.
    /// </summary>
    void InitializeVolumeSliders()
    {
        var soundManager = SoundManager.GetInstance();

        // BGM スライダー.
        BGMSlider.value = soundManager.GetBGMVolume();
        BGMSlider.onValueChanged.AddListener(OnBGMSliderValueChanged);

        // SE スライダー.
        SESlider.value = soundManager.GetSEVolume();
        SESlider.onValueChanged.AddListener(OnSESliderValueChanged);
    }

    /// <summary>
    /// 解像度ドロップダウンの初期化.
    /// </summary>
    void InitializeResolutionDropdown()
    {
        m_Resolutions.Clear();
        ResolutionDropdown.ClearOptions();

        var currentResolution = Screen.currentResolution;

        // 重複を除去しつつ解像度一覧を作成.
        foreach (var resolution in Screen.resolutions)
        {
            if (m_Resolutions.Any(r => r.width == resolution.width && r.height == resolution.height))
            {
                continue;
            }

            m_Resolutions.Add(resolution);
        }

        var options = m_Resolutions
            .Select(r => $"{r.width} x {r.height}")
            .ToList();

        var currentResolutionIndex = m_Resolutions
            .FindIndex(r => r.width == currentResolution.width && r.height == currentResolution.height);

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = Mathf.Max(0, currentResolutionIndex);
        ResolutionDropdown.RefreshShownValue();
        ResolutionDropdown.onValueChanged.AddListener(OnResolutionDropdownValueChanged);
    }

    /// <summary>
    /// BGM スライダー値変更時の処理.
    /// </summary>
    /// <param name="_Value">新しい音量値.</param>
    void OnBGMSliderValueChanged(float _Value)
    {
        SoundManager.GetInstance().SetBGMVolume(_Value);
    }

    /// <summary>
    /// SE スライダー値変更時の処理.
    /// </summary>
    /// <param name="_Value">新しい音量値.</param>
    void OnSESliderValueChanged(float _Value)
    {
        SoundManager.GetInstance().SetSEVolume(_Value);
    }

    /// <summary>
    /// 解像度ドロップダウン値変更時の処理.
    /// </summary>
    /// <param name="_Index">選択されたインデックス.</param>
    void OnResolutionDropdownValueChanged(int _Index)
    {
        var resolution = m_Resolutions[_Index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
