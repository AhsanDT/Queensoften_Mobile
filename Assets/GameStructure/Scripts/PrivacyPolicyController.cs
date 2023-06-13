using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyPolicyController : MonoBehaviour
{

    public GameObject warningPannel; 
    public GameObject mainMenuPannel;
    
    public void AcceptPrivacyPolicy()
    {
        mainMenuPannel.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void DeclinedPrivacyPolicy()
    {
        warningPannel.SetActive(true);
    }
    public void AcceptPrivacyPolicyFromWarning()
    {
        warningPannel.SetActive(false);
        mainMenuPannel.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void DeclinedConfirmed()
    {
        Application.Quit();
    }
    public void BackFromPrivacyPolicyPannel()
    {
        mainMenuPannel.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
