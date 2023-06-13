using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update

    public ScrollRect Tutorialrect;

    private void OnEnable()
    {
        Tutorialrect.verticalNormalizedPosition = 1;
    }
   
}
