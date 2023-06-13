using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPannel : MonoBehaviour
{
    public Slider volumeSlider;


    public Sprite NormalSprite;
    public Sprite SelectedSprite;

    public Image[] buttonsImages;

    public float CurrentVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("CurrentVolume", 1);
        }
        set
        {
            PlayerPrefs.SetFloat("CurrentVolume", value);
        }
    }
    public int CurrentQualitySettings
    {
        get
        {
            return PlayerPrefs.GetInt("CurrentQualitySettings", 2);
        }
        set
        {
            PlayerPrefs.SetInt("CurrentQualitySettings", value);
        }
    }
    private void OnEnable()
    {
        volumeSlider.value = CurrentVolume;
        UpdateVolume();

        if (CurrentQualitySettings == 0)
        {
            SetLow();
        }
        else if (CurrentQualitySettings == 1)
        {
            SetMedium();
        }
        else
        {
            SetHigh();
        }
        UpdateSprites(CurrentQualitySettings);
    }
    void UpdateSprites(int qualityNumber)
    {
        for (int i = 0; i < 3; i++)
        {
            if (qualityNumber == i)
            {
                buttonsImages[i].sprite = SelectedSprite;
            }
            else
            {
                buttonsImages[i].sprite = NormalSprite;
            }
        }
    }

    public void ChangeVolume()
    {
        CurrentVolume = volumeSlider.value;
        UpdateVolume();
    }

    void UpdateVolume()
    {
        AudioListener.volume = CurrentVolume;
    }

    public void SetLow()
    {
        CurrentQualitySettings = 0;
        QualitySettings.SetQualityLevel(0);
        UpdateSprites(0);

    }
    public void SetHigh()
    {
        CurrentQualitySettings = 2;
        QualitySettings.SetQualityLevel(2);
        UpdateSprites(2);


    }
    public void SetMedium()
    {
        CurrentQualitySettings = 1;
        QualitySettings.SetQualityLevel(1);
        UpdateSprites(1);

    }

}
