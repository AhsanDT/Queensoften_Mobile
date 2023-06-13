using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelection : MonoBehaviour
{
    // Start is called before the first frame update

    public Button[] ModeButtons;
    public int LevelAfterModeUnlocked = 3;

    private int unlockedLevels;
    void OnEnable()
    {
        CheckModeUnlocked();
    }


    private void CheckModeUnlocked()
    {
        for(int i = 0;i<ModeButtons.Length-1;i++)
        {
            unlockedLevels = PlayerPrefsHandler.GetSelectedUnlockedLevelWRTEnvFunc(i);
            if (unlockedLevels> LevelAfterModeUnlocked)
            {
                ModeButtons[i + 1].interactable = true;
            }
            else
            {
                ModeButtons[i + 1].interactable = false;
            }
        }
    }
    // Update is called once per frame
   
}
