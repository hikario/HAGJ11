using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    //Audio
    [SerializeField]
    private TextMeshProUGUI ValueText;

    public AudioMixer mixer;

    public void SetLevel(float sliderValue)
    {
        if (sliderValue == 100)
        {
            mixer.SetFloat("MusicVol", -100);
        }
        else
        {
            mixer.SetFloat("MusicVol", 0 - (sliderValue / 3));
        }
    }

    public void OnChangeSlider(float Value)
    {
        ValueText.SetText($"{(100-Value).ToString("N0")}");
    }
}
