using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private Slider _audioSlider;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private Button _returnButton;

    private Resolution[] _resolutions;

    void Start()
    {
        InitializeResolutionDropdown();
        InitializeAudioSlider();
        SetupReturnButton();
    }

    private void InitializeResolutionDropdown()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = $"{_resolutions[i].width} x {_resolutions[i].height} @ {_resolutions[i].refreshRate}Hz";
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
        _resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void InitializeAudioSlider()
    {
        _audioSlider.value = AudioListener.volume;
        _audioSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetupReturnButton()
    {
        _returnButton.onClick.AddListener(CloseOptionsPanel);
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void CloseOptionsPanel()
    {
        _optionsPanel.SetActive(false);
    }
    
}
