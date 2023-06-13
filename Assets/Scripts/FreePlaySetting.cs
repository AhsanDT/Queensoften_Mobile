using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreePlaySetting : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainPanel;
    public GameObject SecoundPanel;

    public Image[] AllCardsImages;
    public Image[] MainCards;
    public Image[] JokerCards;
    public Sprite ImageToReplaceAllCards;
    public Sprite DefaulImage;


    //[HideInInspector]
    public int CurrentId = 0;
   
    private void OnEnable()
    {
        MainMenuController.Instance.TodaysChallenge.SetActive(false);
        MainPanel.SetActive(true);
        SecoundPanel.SetActive(false);

        ReplaceAllCardsImages();

    }


    public void ReplaceAllCardsImages()
    {
        if(ImageToReplaceAllCards)
        {
            foreach (Image currentImage in AllCardsImages)
            {
                if(CurrentId ==0)
                {
                    currentImage.sprite = DefaulImage;
                }
                else
                {
                    currentImage.sprite = ImageToReplaceAllCards;
                }
               
            }

            if(MainMenuController.Instance.freePlayGameLogicScript)
            {
                if(!MainMenuController.Instance.freePlayGameLogicScript.GameStarted)
                {
                    Debug.Log("Game Not Started");
                    foreach (Image currentImage in MainCards)
                    {
                        if (CurrentId == 0)
                        {
                            currentImage.sprite = DefaulImage;
                        }
                        else
                        {
                            currentImage.sprite = ImageToReplaceAllCards;
                        }
                    }

                    MainMenuController.Instance.freePlayGameLogicScript.CardSprite = ImageToReplaceAllCards;
                }

                if (!MainMenuController.Instance.freePlayGameLogicScript.JokerActivated)
                {
                    Debug.Log("Joker Not Activated");
                    foreach (Image currentImage in JokerCards)
                    {
                        if (CurrentId == 0)
                        {
                            currentImage.sprite = DefaulImage;
                        }
                        else
                        {
                            currentImage.sprite = ImageToReplaceAllCards;
                        }
                    }

                    MainMenuController.Instance.freePlayGameLogicScript.JokerDeckCard = ImageToReplaceAllCards;
                }
            }
            

            
        }  
    }
    public void Play()
    {
        MainMenuController.Instance.ButtonClickSound();
        MainPanel.SetActive(false);
        SecoundPanel.SetActive(true);
    }
}
