using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueensProfile : MonoBehaviour
{
    public GameObject[] Panels;
    public GameObject QuitPannel;

    private void OnEnable()
    {
        QuitPannel.SetActive(false);
        foreach (GameObject Panel in Panels)
        {
            Panel.SetActive(false);
        }
    }
    public void SetActivePanel(int PanelNo)
    {

        MainMenuController.Instance.ButtonClickSound();
        foreach (GameObject Panel in Panels)
        {
            Panel.SetActive(false);
        }
        Panels[PanelNo].SetActive(true);
        //gameObject.SetActive(false);
    }

    public void Back()
    {
        MainMenuController.Instance.ButtonClickSound();
        foreach (GameObject Panel in Panels)
        {
            Panel.SetActive(false);
        }
        gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        MainMenuController.Instance.ButtonClickSound();
        Application.Quit();
    }

    public void QuitPannelActive()
    {
        MainMenuController.Instance.ButtonClickSound();
        QuitPannel.SetActive(true);
    }

    public void TermsAndFaqs()
    {

        ApiCode.Instance.SetDataToAPITermsAndConditions();
    }

    public void CallMyAchievementsAPI()
    {
        ApiCode.Instance.LoaderObject.SetActive(true);
        ApiCode.Instance.SetDataTOAPIMyAchievements();
       // Invoke(nameof(TurnOffLoader), 3f);
    }

    private void TurnOffLoader()
    {
        ApiCode.Instance.LoaderObject.SetActive(false);
    }
}
