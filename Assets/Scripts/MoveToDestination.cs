using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToDestination : MonoBehaviour
{
    public Transform MovingDestination;
   // public RectTransform SizeOfCard;
    public float Size = 0.457023f;
    public float totalTime;

    public float FadeOut = 1f;
    private float CurrentFadeOut;
    private float currentTotalTime;

    Image CardImage;
    RectTransform CardRect;

    public Image[] ImagesToReplace;
    private void OnEnable()
    {
        if(MainMenuController.Instance.freePlaySetting.ImageToReplaceAllCards)
        {
            foreach(Image CurrentImage in ImagesToReplace)
            {
                CurrentImage.sprite = MainMenuController.Instance.freePlaySetting.ImageToReplaceAllCards;
            }
        }
        CardImage = GetComponent<Image>();
        CardRect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if(currentTotalTime <= totalTime)
        {
            transform.position = Vector3.Lerp(transform.position, MovingDestination.position, (currentTotalTime / totalTime));
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(Size, Size, Size), (currentTotalTime / totalTime));
            Color tempcolor = CardImage.color;
            tempcolor.a = Mathf.Lerp(1, 0.3f, (currentTotalTime / totalTime));
            CardImage.color = tempcolor;
            //CurrentFadeOut += Time.deltaTime;
            currentTotalTime += Time.deltaTime;
        }
        else
        {
            FreePlayGameLogic.Instance.ReplaceCardsAfterSummingToTen();
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while(currentTotalTime <= totalTime)
        {
            transform.position = Vector3.Lerp(transform.position, MovingDestination.position, (currentTotalTime/totalTime));
            currentTotalTime += Time.deltaTime;
            yield return null;
        }
        FreePlayGameLogic.Instance.ReplaceCardsAfterSummingToTen();
        Destroy(gameObject);
    }
}
