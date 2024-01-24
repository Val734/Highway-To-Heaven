using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Quality_dropdown : MonoBehaviour
{
    [SerializeField] public UnityEvent onOptionsMenuClosed;
    [SerializeField] TMP_Dropdown qualityDropdown;
    //[SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] string playerPrefsKey;
    [SerializeField] int defaultValue = 4;

    void Start()
    {
        qualityDropdown= GetComponentInChildren<TMP_Dropdown>();
        
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < QualitySettings.count; i++)
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = QualitySettings.names[i];
                options.Add(option);//añadimos a estas opciones la opcion para poder crear
            }
            qualityDropdown.options = options;
            qualityDropdown.value = PlayerPrefs.GetInt(playerPrefsKey, defaultValue);
            qualityDropdown.onValueChanged.AddListener(OnQualityDropDownValueChanged);
    }

    private void OnQualityDropDownValueChanged(int value)
    {
        QualitySettings.SetQualityLevel(value);
         PlayerPrefs.SetInt(playerPrefsKey, value);
        PlayerPrefs.Save(); 
    }

}
