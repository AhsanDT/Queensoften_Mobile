using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMenu : MonoBehaviour
{
    public GameObject[] Panels;

    public string OpenAnimations;
    public string CloseAnimations;

    private bool CurrentOpened;
    private bool CurrentClosed;
    private Animator anim;
    public QueensProfile QueensProfile;
    public GameObject Achiev;
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.Play(OpenAnimations);
        CurrentOpened = true;
        CurrentClosed = false;
        foreach (GameObject Panel in Panels)
        {
            Panel.SetActive(false);
        }
    }
    public void SetActivePanel(int PanelNo)
    {
        if(!CurrentOpened)
        {
            if(!CurrentClosed)
            {
                MainMenuController.Instance.ButtonClickSound();
                foreach (GameObject Panel in Panels)
                {
                    Panel.SetActive(false);
                }
                Panels[PanelNo].SetActive(true);
                if (Panels[PanelNo] == Achiev)
                {
                    QueensProfile.CallMyAchievementsAPI();
                }
               // gameObject.SetActive(false);
                ApiCode.Instance.MyItemsData.ResetContentsValues();
                ApiCode.Instance.store_data.ResetContentsValues();

                anim.Play(CloseAnimations);
            }

        }
        
    }

    public void UnSetCurrentlyOpened()
    {
        CurrentOpened = false;
    }
    public void SetCurrentlyClosed()
    {
       
        CurrentClosed = true;
    }

    public void CloseGameObject()
    {
        gameObject.SetActive(false);
    }

    public void CloseMenu()
    {
        anim.Play(CloseAnimations);
        //gameObject.SetActive(false);
    }

    public void BackToMainScreen()
    {
        anim.Play(CloseAnimations);
        MainMenuController.Instance.BackToMainMenu();
    }
}
