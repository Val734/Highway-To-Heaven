using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;

public class Resolution_dropdown : MonoBehaviour
{
    [SerializeField] public UnityEvent onOptionsMenuClosed;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    [SerializeField] string playerPrefsKey;
    [SerializeField] int defaultValue = 6;

    protected virtual void Awake()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown = GetComponentInChildren<TMP_Dropdown>();
        resolutionDropdown.onValueChanged.AddListener(OnResolutionDropDownValueChanged);
        resolutionDropdown.value = PlayerPrefs.GetInt(playerPrefsKey, defaultValue);
    }

    private void Start()
    {
        int currentResolution = -1;
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = $"{Screen.resolutions[i].width}x{Screen.resolutions[i].height}-{Mathf.RoundToInt((float)Screen.resolutions[i].refreshRateRatio.value)}Hz";
            options.Add(option);

            if (Screen.currentResolution.width == Screen.resolutions[i].width &&
                Screen.currentResolution.height == Screen.resolutions[i].height &&
                Screen.currentResolution.refreshRateRatio.value == Screen.resolutions[i].refreshRateRatio.value)
            {
                currentResolution = i;
                Debug.Log("Current Resolution Set: " + option.text);
            }
        }

        resolutionDropdown.options = options;
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.onValueChanged.AddListener(OnResolutionDropDownValueChanged);
    }

    private void OnResolutionDropDownValueChanged(int value)
    {
        Screen.SetResolution(Screen.resolutions[value].width, Screen.resolutions[value].height, FullScreenMode.Windowed, Screen.resolutions[value].refreshRateRatio);
        PlayerPrefs.SetInt(playerPrefsKey, value);
        PlayerPrefs.Save();
    }
}
