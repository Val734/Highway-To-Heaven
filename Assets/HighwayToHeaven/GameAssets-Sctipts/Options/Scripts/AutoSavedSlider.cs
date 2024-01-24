using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSavedSlider : MonoBehaviour
{
    [SerializeField] string playerPrefsKey;
    [SerializeField] float defaultValue = 0.5f;

    Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
        slider.value = PlayerPrefs.GetFloat(playerPrefsKey, defaultValue);
    }
    private void Start()
    {
        InternalValueChanged(slider.value);
    }

    void OnValueChanged(float newValue)
    {
        PlayerPrefs.SetFloat(playerPrefsKey, newValue);
        PlayerPrefs.Save();

        InternalValueChanged(newValue);
    }
    protected virtual void InternalValueChanged(float newValue) { Debug.Log("HOLII"); }
    //virtual-_> funcion que s ele puede dar un código diferente cuando se hereda
}
