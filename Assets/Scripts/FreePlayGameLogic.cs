//using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class FreePlayGameLogic : MonoBehaviour
{

    public enum GameType { freeplay, timed, challenges };
    public enum CardTypeActive { none, joker, shuffle };
    public GameType gameType;
    public CardTypeActive cardtypeactive;
    public List<CardData> EnterCardData = new List<CardData>();
    public List<CardData> FaceCardsOnly = new List<CardData>();
    public List<CardData> ShuffleData = new List<CardData>();
    public List<CardButtonData> JokerCards = new List<CardButtonData>();
    public List<CardButtonData> JokerCardsCopy = new List<CardButtonData>();
    public List<CardButtonData> CardButtonDataValues = new List<CardButtonData>();
    public List<WinCombo> WinCombinations = new List<WinCombo>();

    public List<CardData> ShuffleDataShuffleModule = new List<CardData>(12);


    [Header("Joker Module")]
    public List<CardData> JokerCardPackData = new List<CardData>();
    public Transform[] JokerCurrentPositions;
    public Transform[] JokerInitialPositions;
    public Transform[] JokerMainPositions;
    public Transform[] JokerSelectPositions;
    public Transform MainCardPack;
    public Transform InitalMainCardPosition;
    public Transform DesMainCardPosition;
    public GameObject SecoundDeck;
    public Transform SecoundDeckInitialPos;
    public Animator SecoundDeckAnimator;
    public Transform MoveDecPos;
    public GameObject JokerCardPack;
    public Sprite JokerDeckCard;
    [Header("JokerTimes")]
    public float JokerCardDeckTime;
    public float JokerCardsPositionsTime;
    public float JokerCardSelectedTime = 2f;
    public float JokerCardSetValuesAndSprite = 2f;
    public float CardScaleMax = 1.501151f;
    public float CardScaleMin = 1f;
    public float CardMainDeckScale = 0.5933901f;

    public GameObject[] ButtonsToDisable;
    public GameObject StoreButton;
    public Sprite CardSprite;
    public Text TensScoreText;
    public Text TimerText;
    public RectTransform NormalCard;
    public RectTransform FlipCard;

    public GameObject levelFailPanel;
    public GameObject LevelWinPanel;
    public RectTransform startpos01;
    public RectTransform startpos02;
    public GameObject Prefab01;
    public GameObject Prefab02;
    public Transform Parent;
    public float TotalTime01 = 3f;
    public float TotalTime02 = 3f;
   
   
    private bool flipped;
    private bool turning;

    private System.Random _random = new System.Random();
    private float CurrentCardMovePos;
    private float CurrentCardDesMovePos;
    private float CurrntJokerMovePos;
    private float CurrntJokerMovePosBack;
    private float CurrntMainDeckMovePosBack;

    private int CurrentJokerCardsValue = 1;
    private int CurrentShuffleCardsValue;
    [HideInInspector]
    public bool JokerActivated = false;
    private bool JokerAnimationsCompleted = false;
    private bool JokerCardSelected = false;
    private bool JokerCardSelectedMoveAnimationsInProcess = false;
    private bool BackJokerCards = false;
    private bool Once1 = true;
    private bool Once2 = true;
    private bool Once3 = true;
    private bool Once4 = true;
    private bool Once5 = true;
    private bool MoveJokerCards = false;
    private bool ShuffleUsed = false;
    private bool JokeruUsed = false;
    private float CurrentTime01;
    private float CurrentTime02;
    private int CurrentJokerCardSelected;
    float CurrentJokerTimer = 0f;
    private float CurrentJokerCardSetValuesAndSprite;
    private int CurrentCardSelected;
    private static FreePlayGameLogic instance;

    System.DateTime NewDate;

    private StringBuilder QueenOfTenTimerBuilder;

    public GameObject Jokercard;
    public Text JokerCardText;

    public GameObject ShuffleCard;
    public Text ShuffleCardText;

    public int CurrentItemIdShuffle;
    public int CurrentItemQuantityShuffle;

    public int CurrentItemIdJoker;
    public int CurrentItemQuantityJoker;

    public int CurrnetIndex = -1;
    public int sum = 0;
    public int turn = 0;
    int currnetTens = 0;
    private bool CanClear = true;
    public List<int> CardsToReplace = new List<int>();


    [HideInInspector]
    public bool GameStarted;
    private bool EndTimer = false;
    private float CurrentTime;
    private int secounds;
    private int minutes;
    private int hours;
    public GameObject AchievementUnlocked;
    public GameObject AchievementTextObject;
    public Text AchievementText;
    public GameObject MainDeck;
    public GameObject AreYouReady;

    public bool AllFaceCards;
    public bool NoFaceCards;
    public bool FourQueensInRow;

    [Header("Selected Images And Color")]
    public Image[] SelectedImages;
    public Color NormalSelectedColor;
    public Color WrongSelectedColor;

    private AudioSource MusicAudio;
    public int Face_Cards_Counter;
    public static FreePlayGameLogic Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FreePlayGameLogic>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    

    private bool CheckFourQueensInRow()
    {
        int currentQueen1 = 0;
        int currentQueen2 = 0;
        int currentQueen3 = 0;
        for (int i = 0;i<CardButtonDataValues.Count;i++)
        {
            if(i<4)
            {
                if(CardButtonDataValues[i].FaceCardType=='q')
                {
                    currentQueen1++;
                }
            }
            if(i>=4 && i<8)
            {
                if (CardButtonDataValues[i].FaceCardType == 'q')
                {
                    currentQueen2++;
                }
            }
            if(i>=8)
            {
                if (CardButtonDataValues[i].FaceCardType == 'q')
                {
                    currentQueen3++;
                }
            }
        }

        if(currentQueen1==4 || currentQueen2 == 4 || currentQueen3 == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }


    private void SlowMusic()
    {
        GameObject Music = GameObject.Find("Music");
        if(Music)
        {
            MusicAudio = Music.GetComponent<AudioSource>();
            MusicAudio.volume = 0.05f;
        }
    }
    private void FastMusic()
    {
        if(MusicAudio)
        {
            MusicAudio.volume = 0.2f;
        }
    }
    private void OnEnable()
    {
        EndTimer = true;
        GameStarted = true;
        MainDeck.SetActive(true);
        SetQuantititiesOfItems();
        AreYouReady.SetActive(true);
        //OnStartGame();
        WhatStoreCardsToEnable();
        SlowMusic();
       // AddSpirtesAndValuesToButtons();
    }


    private void OnDisable()
    {
        FastMusic();
        GameStarted = false;
    }

    public void SetQuantititiesOfItems()
    {
        CurrentItemQuantityJoker = MyItemsData.Instance.CurrentJokerValue;
        JokerCardText.text = CurrentItemQuantityJoker.ToString("00");

        CurrentItemQuantityShuffle = MyItemsData.Instance.CurrentShuffleValue;
        ShuffleCardText.text = CurrentItemQuantityShuffle.ToString("00");
    }
    public void OnStartGame()
    {
        if (gameType == GameType.timed || gameType == GameType.challenges)
        {
            EndTimer = false;
            CurrentTime = 0f;
            QueenOfTenTimerBuilder = new(10);
            TimerText.gameObject.SetActive(true);
        }
        else
        {
            if (gameType == GameType.freeplay)
            {
               // TimerText.gameObject.SetActive(false);
            }

        }
        CanClick = false;
        Face_Cards_Counter = 0;
        OnlyOnceHitApi = true;
        ShuffleUsed = false;
        JokeruUsed = false;
        levelFailPanel.SetActive(false);
        LevelWinPanel.SetActive(false);
        currnetTens = 0;
        TensScoreText.text = currnetTens.ToString();
        AddCardSpirtesAndValuesToButtons();
        ShuffleArray();


        if(gameType==GameType.challenges)
        {
            if (MainMenuController.Instance.CurrentChallengeString == "Win the game with all face cards starting on the table")
            {
                AddCardSpirtesAndValuesToButtonsFaceUpCards();

            }
            if (MainMenuController.Instance.CurrentChallengeString == "Win a game starting with no face cards on the table")
            {
                AddCardSpirtesAndValuesToButtonsFaceUpCards(true);
               // Invoke(nameof(AddSpirtesAndValuesToButtonsV2), 0.5f);
            }
            else
            {
               // Invoke(nameof(AddSpirtesAndValuesToButtons), 1.5f);
                //Invoke(nameof(AddSpirtesAndValuesToButtons), 1.5f);
            }
        }
        else
        {
           // Invoke(nameof(AddSpirtesAndValuesToButtons), 1.5f);
        }
        Invoke(nameof(AddSpirtesAndValuesToButtons), 1.5f);

    }

   
    private void Update()
    {
        if (gameType == GameType.timed || gameType == GameType.challenges)
        {
            if(!EndTimer)
            {
                CurrentTime += Time.deltaTime;
                secounds = (int)(CurrentTime % 60);
                minutes = (int)(CurrentTime / 60) % 60;
                hours = (int)(CurrentTime / 3600) % 24;
                QueenOfTenTimerBuilder.Clear();
                QueenOfTenTimerBuilder.Append(hours.ToString("00"));
                QueenOfTenTimerBuilder.Append(":");
                QueenOfTenTimerBuilder.Append(minutes.ToString("00"));
                QueenOfTenTimerBuilder.Append(":");
                QueenOfTenTimerBuilder.Append(secounds.ToString("00"));
                TimerText.text = QueenOfTenTimerBuilder.ToString();
            }
            
        }

            if (JokerActivated && !JokerAnimationsCompleted)
            {
            if(Once1)
            {
                foreach (GameObject buttonstodisable in ButtonsToDisable)
                {
                    buttonstodisable.SetActive(false);
                }
                Once1 = false;
            }
            if(CurrentCardMovePos < JokerCardDeckTime)
            {
                CurrentCardMovePos += Time.deltaTime;
                MainCardPack.transform.position = Vector3.Lerp(MainCardPack.transform.position, DesMainCardPosition.position, (CurrentCardMovePos / JokerCardDeckTime));
                if(CurrentCardMovePos > 0.7)
                {
                    if (Once2)
                    {
                        SecoundDeck.SetActive(true);
                        Once2 = false;
                    }
                }
                
            }
            else
            {
                if(CurrentCardDesMovePos < JokerCardDeckTime)
                {
                    SecoundDeckAnimator.enabled = false;
                    CurrentCardDesMovePos += Time.deltaTime;
                    MainCardPack.transform.position = Vector3.Lerp(MainCardPack.transform.position, MoveDecPos.position, (CurrentCardDesMovePos / JokerCardDeckTime));
                    MainCardPack.transform.localScale = Vector3.Lerp(MainCardPack.transform.localScale, MoveDecPos.localScale, (CurrentCardDesMovePos / JokerCardDeckTime));

                    SecoundDeck.transform.position = Vector3.Lerp(SecoundDeck.transform.position, MoveDecPos.position, (CurrentCardDesMovePos / JokerCardDeckTime));
                    SecoundDeck.transform.localScale = Vector3.Lerp(SecoundDeck.transform.localScale, MoveDecPos.localScale, (CurrentCardDesMovePos / JokerCardDeckTime));
                    //Debug.LogError("Timer " + (CurrentCardDesMovePos / JokerCardDeckTime));
                    if((CurrentCardDesMovePos / JokerCardDeckTime) > 0.3)
                    {
                        if(Once3)
                        {
                            MainCardPack.gameObject.SetActive(false);
                            SecoundDeck.gameObject.SetActive(false);
                            JokerCardPack.SetActive(true);
                            MoveJokerCards = true;

                        }
                    }
                }
            }

            if(MoveJokerCards && JokerCardPack.activeInHierarchy)
            {
                if(CurrntJokerMovePos < JokerCardsPositionsTime)
                {
                    if(Once4)
                    {
                        for (int i = 0; i < JokerCards.Count; i++)
                        {
                            JokerCards[i].ButtonSpirteImage.sprite = JokerCardPackData[i].CardImage;
                            JokerCards[i].CardButtonValue = JokerCardPackData[i].CardValue;
                            JokerCards[i].FaceCardType = JokerCardPackData[i].FaceCardType;
                            //JokerCards[i].ButtonSpirteImage.rectTransform.sizeDelta = FlipCard.sizeDelta;
                            JokerCards[i].CardButton.interactable = true;

                        }
                        Once4 = false;
                    }
                    CurrntJokerMovePos += (Time.deltaTime);

                    
                    //Debug.LogError("TImer " + (CurrntJokerMovePos / JokerCardsPositionsTime));
                    for (int i = 0; i < JokerCurrentPositions.Length; i++)
                    {
                        //Debug.LogError("Index " + i + " Timer " + (CurrntJokerMovePos / JokerCardsPositionsTime));
                        JokerCurrentPositions[i].position = Vector3.Lerp(JokerCurrentPositions[i].position, JokerMainPositions[i].position, (CurrntJokerMovePos / JokerCardsPositionsTime));
                        JokerCurrentPositions[i].rotation = Quaternion.Lerp(JokerCurrentPositions[i].rotation, JokerMainPositions[i].rotation, (CurrntJokerMovePos / JokerCardsPositionsTime));
                    }

                    if((CurrntJokerMovePos / JokerCardsPositionsTime) > 0.24f)
                    {
                        JokerAnimationsCompleted = true;
                    }
                }
            }
        }
            else
        {
            if(!JokerActivated)
            {
                MainCardPack.gameObject.SetActive(true);
            }
        }
        if(BackJokerCards)
        {
            CurrntJokerMovePosBack += Time.deltaTime;
            for (int i = 0; i < JokerCurrentPositions.Length; i++)
            {
                JokerCurrentPositions[i].position = Vector3.Lerp(JokerCurrentPositions[i].position, JokerInitialPositions[i].position, (CurrntJokerMovePosBack / (JokerCardsPositionsTime)));
                JokerCurrentPositions[i].rotation = Quaternion.Lerp(JokerCurrentPositions[i].rotation, JokerInitialPositions[i].rotation, (CurrntJokerMovePosBack / (JokerCardsPositionsTime)));
                if (CurrntJokerMovePosBack / (JokerCardsPositionsTime) > 0.1f)
                {
                    JokerCards[i].ButtonSpirteImage.sprite = JokerDeckCard;
                }
            }

            //Debug.LogError("Timer " + (CurrntJokerMovePosBack / JokerCardsPositionsTime));
            if((CurrntJokerMovePosBack /JokerCardsPositionsTime) > 0.25f)
            {
                CurrntMainDeckMovePosBack += Time.deltaTime;
                MainCardPack.gameObject.SetActive(true);
                JokerCardPack.SetActive(false);
                MainCardPack.transform.position = Vector3.Lerp(MainCardPack.transform.position, InitalMainCardPosition.position, (CurrntMainDeckMovePosBack / JokerCardsPositionsTime));
                MainCardPack.transform.localScale = Vector3.Lerp(MainCardPack.transform.localScale, new Vector3(CardMainDeckScale, CardMainDeckScale , CardMainDeckScale), (CurrntMainDeckMovePosBack / JokerCardsPositionsTime));


            }

            if ((CurrntMainDeckMovePosBack / JokerCardsPositionsTime) > 0.2f)
            {
                ResetJokerValuesAndVariables();
            }

                //Debug.LogError("Timer " + (CurrntMainDeckMovePosBack / JokerCardsPositionsTime));
        }


        if (OnlyOnceHitApi)
        {
            if (levelFailPanel.activeInHierarchy)
            {
                switch (gameType)
                {
                    case GameType.freeplay:
                        {
                            Debug.Log("Sending stats levelfail freeplay");
                            NewDate = System.DateTime.Now;
                            //Debug.Log(NewDate.ToString("dd-MM-yyyy"));

                           
                                
                            //ApiCode.Instance.setDataToApiLevelWinFail("FreePlay", 0, 1, NewDate.ToString("dd-MM-yyyy"), "0:00", currnetTens);
                            ApiCode.Instance.setDataToApiLevelWinFail("FreePlay", 0, 1, NewDate.ToString("MM-dd-yyyy"), "0:00", currnetTens);
                            break;
                        }
                    case GameType.timed:
                        {
                            Debug.Log("Sending stats levelfail Timed");
                            NewDate = System.DateTime.Now;
                            //Debug.Log(NewDate.ToString("dd-MM-yyyy"));
                            ApiCode.Instance.setDataToApiLevelWinFail("Timed", 0, 1, NewDate.ToString("MM-dd-yyyy"), TimerText.text, currnetTens);
                            break;
                        }
                    case GameType.challenges:
                        {
                            Debug.Log("Sending stats levelfail Challenge");
                            NewDate = System.DateTime.Now;
                            //Debug.Log(NewDate.ToString("dd-MM-yyyy"));
                            if(MainMenuController.Instance.HasCurrentChallengeId)
                            {
                                ApiCode.Instance.setDataToApiLevelWinFail("Challenge", 0, 1, NewDate.ToString("MM-dd-yyyy"), TimerText.text, currnetTens, true, MainMenuController.Instance.CurrentChallengeId);
                            }
                            else
                            {

                            }
                           
                            break;
                        }
                }
                OnlyOnceHitApi = false;
                CurrentLose++;
            }
            if (LevelWinPanel.activeInHierarchy)
            {
                MainDeck.SetActive(false);
                switch (gameType)
                {
                    case GameType.freeplay:
                        {
                            NewDate = System.DateTime.Now;
                            Debug.Log("Sending stats levelwin freeplay");

                            //Debug.Log(NewDate.ToString("dd-MM-yyyy"));
                            ApiCode.Instance.setDataToApiLevelWinFail("FreePlay", 1, 0, NewDate.ToString("MM-dd-yyyy"), "0:00", currnetTens);
                            break;
                        }
                    case GameType.timed:
                        {
                            Debug.Log("Sending stats levelwin timed");
                            NewDate = System.DateTime.Now;
                            //Debug.Log(NewDate.ToString("dd-MM-yyyy"));
                            ApiCode.Instance.setDataToApiLevelWinFail("Timed", 1, 0, NewDate.ToString("MM-dd-yyyy"), TimerText.text, currnetTens);
                            break;
                        }
                    case GameType.challenges:
                        {
                            Debug.Log("Sending stats levelwin Challenge");
                            NewDate = System.DateTime.Now;
                            //Debug.Log(NewDate.ToString("dd-MM-yyyy"));
                            if(MainMenuController.Instance.HasCurrentChallengeId)
                            {
                               // Debug.Log("New Date : " + NewDate.ToString("MM-dd-yyyy"));
                               if(MainMenuController.Instance.IsChallengeHardCoded)
                                {
                                    if(CheckhardcodeChallengeCompleted())
                                    {
                                        ApiCode.Instance.setDataToApiLevelWinFail("Challenge", 1, 0, NewDate.ToString("MM-dd-yyyy"), TimerText.text, currnetTens, true, MainMenuController.Instance.CurrentChallengeId , 1, 1);
                                    }
                                    else
                                    {
                                        ApiCode.Instance.setDataToApiLevelWinFail("Challenge", 1, 0, NewDate.ToString("MM-dd-yyyy"), TimerText.text, currnetTens, true, MainMenuController.Instance.CurrentChallengeId , 1, 0);
                                    }

                                }
                               else
                                {
                                    ApiCode.Instance.setDataToApiLevelWinFail("Challenge", 1, 0, NewDate.ToString("MM-dd-yyyy"), TimerText.text, currnetTens, true, MainMenuController.Instance.CurrentChallengeId);

                                }
                                
                            }
                            else
                            {
                                Debug.Log("No Challenge ID Found");
                            }
                            
                            break;
                        }
                }
                OnlyOnceHitApi = false;
                CurrentLose = 0;
            }
           
        }
    }


    private bool CheckhardcodeChallengeCompleted()
    {
        if(MainMenuController.Instance.CurrentChallengeString== "Win a game with no reshuffles")
        {
            if(ShuffleUsed)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        if (MainMenuController.Instance.CurrentChallengeString == "Play and win 10 games straight with no upgrades")
        {
            if (!ShuffleUsed && !JokeruUsed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (MainMenuController.Instance.CurrentChallengeString == "Win a game with no upgrades")
        {
            if (!ShuffleUsed && !JokeruUsed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (MainMenuController.Instance.CurrentChallengeString == "Win the game with all face cards starting on the table")
        {
            if (AllFaceCards)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        if (MainMenuController.Instance.CurrentChallengeString == "Win a game starting with no face cards on the table")
        {
            if (NoFaceCards)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (MainMenuController.Instance.CurrentChallengeString == "Get 4 queens in a row for extra upgrades")
        {
            if (CheckFourQueensInRow())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
    private void ResetJokerValuesAndVariables()
    {
        JokerCardSelectedMoveAnimationsInProcess = false;
        JokerAnimationsCompleted = false;
        JokerActivated = false;
        JokerCardSelected = false;
        Once1 = true;
        Once2 = true;
        Once3 = true;
        Once4 = true;
        Once5 = true;
        CurrentCardMovePos = 0f;
        CurrentCardDesMovePos = 0f;
        CurrntJokerMovePos = 0f;
        BackJokerCards = false;
        CurrntJokerMovePosBack = 0f;
        CurrntMainDeckMovePosBack = 0f;
        SecoundDeckAnimator.enabled = true;
        StoreButton.SetActive(true);
        SecoundDeck.transform.position = SecoundDeckInitialPos.position;
        SecoundDeck.transform.rotation = SecoundDeckInitialPos.rotation;
        WhatStoreCardsToEnable();
        CheckLevelWinLevelFail();
    }


    public void SetCurrentItemIDAndQuantity(int id , int quantity)
    {
        Debug.Log("Item Id : " + id);
        if(id==1)
        {
            CurrentItemIdShuffle = id;
            CurrentItemQuantityShuffle = quantity;
        }
        if(id==2)
        {
            CurrentItemIdJoker = id;
            CurrentItemQuantityJoker = quantity;
        }
      
    }
    public void WhatStoreCardsToEnable()
    {
        ShuffleCard.SetActive(true);
        Jokercard.SetActive(true);

        CurrentJokerCardsValue = CurrentItemQuantityJoker;
        JokerCardText.text = CurrentItemQuantityJoker.ToString("00");

        CurrentShuffleCardsValue = CurrentItemQuantityShuffle;
        ShuffleCardText.text = CurrentItemQuantityShuffle.ToString("00");
        switch (cardtypeactive)
        {
            
            case CardTypeActive.none:
                {
                   // ShuffleCard.SetActive(false);
                  //  Jokercard.SetActive(false);
                    break;
                }
            case CardTypeActive.joker:
                {
                   // ShuffleCard.SetActive(false);
                   // Jokercard.SetActive(true);
                   
                    break;
                }

            case CardTypeActive.shuffle:
                {
                   // ShuffleCard.SetActive(true);
                   // Jokercard.SetActive(false);
                    
                    break;
                }
        }
    }

    public void resetSelectedImages()
    {
        foreach(Image CurrentImage in SelectedImages)
        {
            CurrentImage.gameObject.SetActive(false);
        }
    }
    public void CheckForTens(int index)
    {

        if(CanClick)
        {
            if (JokerActivated)
            {
                if (JokerCardSelected)
                {
                    MainMenuController.Instance.CardSoundClick();
                    CurrentCardSelected = index;
                    JokerCardSelectedMoveAnimationsInProcess = true;
                    ///CardsToReplace.Clear();
                    // CardsToReplace.Add(index);
                    JokerCardSelectedMoveAnimationsInProcess = true;
                    StartCoroutine(SetValuesAndSpirteFromJokerCard());
                }
            }
            else
            {
                if (CurrnetIndex != index || turn == 0)
                {
                    turn++;
                    CardsToReplace.Add(index);
                    CurrnetIndex = index;
                    if (CardButtonDataValues[index].CardButtonValue != -1)
                    {
                        sum += CardButtonDataValues[index].CardButtonValue;
                    }

                    if (turn >= 2)
                    {
                        if (sum == 10)
                        {
                            SelectedImages[index].gameObject.SetActive(true);
                            SelectedImages[index].color = NormalSelectedColor;
                            MainMenuController.Instance.CorrectCardSound();
                            CanClear = false;
                            currnetTens++;
                            TensScoreText.text = currnetTens.ToString();
                            ///ReplaceCardsAfterSummingToTen();
                            ///

                            if (ShuffleData.Count > 1)
                            {
                                if (CardsToReplace.Count > 1)
                                {
                                    GameObject n1 = Instantiate(Prefab01, Parent);
                                    n1.GetComponent<MoveToDestination>().totalTime = TotalTime01;
                                    n1.GetComponent<MoveToDestination>().MovingDestination = CardButtonDataValues[CardsToReplace[0]].ButtonSpirteImage.transform;


                                    GameObject n2 = Instantiate(Prefab02, Parent);
                                    n2.GetComponent<MoveToDestination>().totalTime = TotalTime02;
                                    n2.GetComponent<MoveToDestination>().MovingDestination = CardButtonDataValues[CardsToReplace[1]].ButtonSpirteImage.transform;
                                }
                                else
                                {
                                    GameObject n1 = Instantiate(Prefab01, Parent);
                                    n1.GetComponent<MoveToDestination>().totalTime = TotalTime01;
                                    n1.GetComponent<MoveToDestination>().MovingDestination = CardButtonDataValues[CardsToReplace[0]].ButtonSpirteImage.transform;
                                }
                            }
                        }
                        sum = 0;
                        turn = 0;
                        if (CanClear)
                        {
                            SelectedImages[index].gameObject.SetActive(true);
                            SelectedImages[index].color = WrongSelectedColor;
                            Invoke(nameof(resetSelectedImages), 0.6f);
                            MainMenuController.Instance.WrongCardSound();
                            CardsToReplace.Clear();
                        }
                    }
                    else
                    {
                        SelectedImages[index].gameObject.SetActive(true);
                        SelectedImages[index].color = NormalSelectedColor;
                        MainMenuController.Instance.CardSoundClick();
                        if (sum == 10)
                        {
                            MainMenuController.Instance.CorrectCardSound();
                            CanClear = false;
                            currnetTens++;
                            TensScoreText.text = currnetTens.ToString();
                            ///ReplaceCardsAfterSummingToTen();
                            ///

                            if (ShuffleData.Count > 1)
                            {
                                if (CardsToReplace.Count > 1)
                                {
                                    GameObject n1 = Instantiate(Prefab01, Parent);
                                    n1.GetComponent<MoveToDestination>().totalTime = TotalTime01;
                                    n1.GetComponent<MoveToDestination>().MovingDestination = CardButtonDataValues[CardsToReplace[0]].ButtonSpirteImage.transform;


                                    GameObject n2 = Instantiate(Prefab02, Parent);
                                    n2.GetComponent<MoveToDestination>().totalTime = TotalTime02;
                                    n2.GetComponent<MoveToDestination>().MovingDestination = CardButtonDataValues[CardsToReplace[1]].ButtonSpirteImage.transform;
                                }
                                else
                                {
                                    GameObject n1 = Instantiate(Prefab01, Parent);
                                    n1.GetComponent<MoveToDestination>().totalTime = TotalTime01;
                                    n1.GetComponent<MoveToDestination>().MovingDestination = CardButtonDataValues[CardsToReplace[0]].ButtonSpirteImage.transform;
                                }
                            }

                        }
                    }
                }
            }
        }







       
        
    }

    IEnumerator SetValuesAndSpirteFromJokerCard()
    {
        resetSelectedImages();
        CurrentJokerCardSetValuesAndSprite = 0;
        while (CurrentJokerCardSetValuesAndSprite < JokerCardSetValuesAndSprite)
        {
            CurrentJokerCardSetValuesAndSprite += Time.deltaTime;

            JokerCards[CurrentJokerCardSelected].CardButton.transform.position = Vector3.Lerp(JokerCards[CurrentJokerCardSelected].CardButton.transform.position, CardButtonDataValues[CurrentCardSelected].CardButton.transform.position, 
                (CurrentJokerCardSetValuesAndSprite/ JokerCardSetValuesAndSprite));

            JokerCards[CurrentJokerCardSelected].CardButton.transform.rotation = Quaternion.Lerp(JokerCards[CurrentJokerCardSelected].CardButton.transform.rotation, CardButtonDataValues[CurrentCardSelected].CardButton.transform.rotation,
                (CurrentJokerCardSetValuesAndSprite / JokerCardSetValuesAndSprite));

            JokerCards[CurrentJokerCardSelected].CardButton.transform.localScale = Vector3.Lerp(JokerCards[CurrentJokerCardSelected].CardButton.transform.localScale, new Vector3(CardScaleMax, CardScaleMax, CardScaleMax),
                (CurrentJokerCardSetValuesAndSprite / JokerCardSetValuesAndSprite));

            Color tempcolor = JokerCards[CurrentJokerCardSelected].ButtonSpirteImage.color;
            tempcolor.a = Mathf.Lerp(1, 0.3f, (CurrentJokerCardSetValuesAndSprite / JokerCardSetValuesAndSprite));
            JokerCards[CurrentJokerCardSelected].ButtonSpirteImage.color = tempcolor;

            if ((CurrentJokerCardSetValuesAndSprite / JokerCardSetValuesAndSprite) > 0.40)
            {
                if(Once5)
                {
                    JokerCards[CurrentJokerCardSelected].CardButton.gameObject.SetActive(false);
                    JokerCardsCopy = new List<CardButtonData>(JokerCards);
                    CardButtonDataValues[CurrentCardSelected].ButtonSpirteImage.sprite = JokerCardsCopy[CurrentJokerCardSelected].ButtonSpirteImage.sprite;
                    CardButtonDataValues[CurrentCardSelected].CardButtonValue = JokerCardsCopy[CurrentJokerCardSelected].CardButtonValue;

                    CardButtonDataValues[CurrentCardSelected].FaceCardType = JokerCardsCopy[CurrentJokerCardSelected].FaceCardType;

                    if (CardButtonDataValues[CurrentCardSelected].CardButtonValue == -1)
                    {
                        CardButtonDataValues[CurrentCardSelected].CardButton.interactable = false;
                    }
                    else
                    {
                        CardButtonDataValues[CurrentCardSelected].CardButton.interactable = true;
                    }

                    BackJokerCards = true;
                    Once5 = false;
                }
               
            }
            yield return null;
        }
    }

    IEnumerator SetJokerSelected(int index)
    {
        CurrentJokerTimer = 0f;
        while (CurrentJokerTimer < JokerCardSelectedTime)
        {
            CurrentJokerTimer += Time.deltaTime;
           // Debug.LogError("Timer " + (CurrentJokerTimer / JokerCardSelectedTime));
            for (int i = 0; i < JokerCurrentPositions.Length; i++)
            {
                if (i != index)
                {
                    JokerCurrentPositions[i].position = Vector3.Lerp(JokerCurrentPositions[i].position, JokerMainPositions[i].position, (CurrentJokerTimer / JokerCardSelectedTime));
                }
            }
            JokerCurrentPositions[index].position = Vector3.Lerp(JokerCurrentPositions[index].position, JokerSelectPositions[index].position, (CurrentJokerTimer / JokerCardSelectedTime));
            
            yield return null;
        }
        
    }

    public void ReplaceCardsAfterSummingToTen()
    {
        if(gameType == GameType.challenges)
        {
            resetSelectedImages();
            for (int i = 0; i < CardsToReplace.Count; i++)
            {
                CardButtonDataValues[CardsToReplace[i]].ButtonSpirteImage.sprite = ShuffleData[i].CardImage;
                CardButtonDataValues[CardsToReplace[i]].CardButtonValue = ShuffleData[i].CardValue;
                CardButtonDataValues[CardsToReplace[i]].FaceCardType = ShuffleData[i].FaceCardType;
                CardButtonDataValues[CardsToReplace[i]].ButtonSpirteImage.rectTransform.sizeDelta = FlipCard.sizeDelta;
                if (CardButtonDataValues[CardsToReplace[i]].CardButtonValue == -1)
                {
                    CardButtonDataValues[CardsToReplace[i]].CardButton.interactable = false;
                }
                ShuffleData.Remove(ShuffleData[i]);

            }
            CardsToReplace.Clear();
            CanClear = true;
            turn = 0;
            sum = 0;
            CheckLevelWinLevelFail();
        }
        else
        {
            resetSelectedImages();
            for (int i = 0; i < CardsToReplace.Count; i++)
            {
                CardButtonDataValues[CardsToReplace[i]].CardButtonValue = ShuffleData[i].CardValue;


                if (CardButtonDataValues[CardsToReplace[i]].CardButtonValue == -1)
                {
                    CardButtonDataValues[CardsToReplace[i]].CardButton.interactable = false;
                    CardButtonDataValues[CardsToReplace[i]].ButtonSpirteImage.sprite = FaceCardsOnly[Face_Cards_Counter].CardImage;
                    CardButtonDataValues[CardsToReplace[i]].FaceCardType = FaceCardsOnly[Face_Cards_Counter].FaceCardType;
                    Face_Cards_Counter++;
                    if (Face_Cards_Counter >= FaceCardsOnly.Count)
                    {
                        Face_Cards_Counter = 0;
                    }
                }
                else
                {
                    CardButtonDataValues[CardsToReplace[i]].ButtonSpirteImage.sprite = ShuffleData[i].CardImage;
                    CardButtonDataValues[CardsToReplace[i]].FaceCardType = ShuffleData[i].FaceCardType;
                }


                CardButtonDataValues[CardsToReplace[i]].ButtonSpirteImage.rectTransform.sizeDelta = FlipCard.sizeDelta;

                ShuffleData.Remove(ShuffleData[i]);

            }
            CardsToReplace.Clear();
            CanClear = true;
            turn = 0;
            sum = 0;
            CheckLevelWinLevelFail();
        }
      
    }
    void Shuffle(List<CardData> array)
    {
        int p = array.Count;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(0, n);
            CardData t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }
    int rand = 0;
    int CurrentLose = 0;
    private void ShuffleArray()
    {

        


        if (CurrentLose >= 4)
        {
            CurrentLose = 0;
            ShuffleData = new List<CardData>(WinCombinations[Random.Range(2, WinCombinations.Count)].ComboList);

        }
        else
        {
            rand = Random.Range(0, 100);
            // ShuffleData = new List<CardData>(WinCombinations[Random.Range(0, WinCombinations.Count)].ComboList);
            // ShuffleData =new List<CardData>(WinCombinations[Random.Range(2, WinCombinations.Count)].ComboList);
            // ShuffleData = new List<CardData>(WinCombinations[Random.Range(2, WinCombinations.Count)].ComboList);

            if (rand < 13)
            {
                //ShuffleData = new List<CardData>(WinCombinations[Random.Range(0, WinCombinations.Count)].ComboList);

                ShuffleData = new List<CardData>(WinCombinations[Random.Range(2, WinCombinations.Count)].ComboList);
                //ShuffleData = new List<CardData>(WinCombinations[1].ComboList);
            }
            else
            {
                ShuffleData = new List<CardData>(EnterCardData);
                // ShuffleData = EnterCardData;
                Shuffle(ShuffleData);
            }
        }


        //ShuffleData = new List<CardData>(WinCombinations[1].ComboList);
        //ShuffleData = EnterCardData;
    }
    private void AddCardSpirtesAndValuesToButtons()
    {
        for (int i = 0; i < CardButtonDataValues.Count; i++)
        {
            CardButtonDataValues[i].ButtonSpirteImage.sprite = CardSprite;
            CardButtonDataValues[i].CardButtonValue = 0;
            CardButtonDataValues[i].FaceCardType = 'n';
            CardButtonDataValues[i].ButtonSpirteImage.rectTransform.sizeDelta = NormalCard.sizeDelta;
            CardButtonDataValues[i].CardButton.interactable = true;
        }
    }


    private void AddCardSpirtesAndValuesToButtonsFaceUpCards(bool noFacecards = false)
    {
        int countNoOffaceCards = 0;


        if(noFacecards)
        {
            for (int i = 0; i < CardButtonDataValues.Count; i++)
            {
                CardButtonDataValues[i].ButtonSpirteImage.sprite = EnterCardData[i].CardImage;
                CardButtonDataValues[i].CardButtonValue = EnterCardData[i].CardValue;
                CardButtonDataValues[i].FaceCardType = EnterCardData[i].FaceCardType;
                CardButtonDataValues[i].ButtonSpirteImage.rectTransform.sizeDelta = FlipCard.sizeDelta;
                CardButtonDataValues[i].CardButton.interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < FaceCardsOnly.Count; i++)
            {
                CardButtonDataValues[i].ButtonSpirteImage.sprite = FaceCardsOnly[i].CardImage;
                CardButtonDataValues[i].CardButtonValue = -1;
                CardButtonDataValues[i].FaceCardType = FaceCardsOnly[i].FaceCardType;
                CardButtonDataValues[i].ButtonSpirteImage.rectTransform.sizeDelta = FlipCard.sizeDelta;
                CardButtonDataValues[i].CardButton.interactable = true;


                if (CardButtonDataValues[i].CardButtonValue == -1)
                {
                    countNoOffaceCards++;
                }
            }
        }
        

        
        if (countNoOffaceCards == (CardButtonDataValues.Count))
        {
            AllFaceCards = true;
        }
        else
        {
            AllFaceCards = false;
        }

        if (countNoOffaceCards == 0)
        {
            NoFaceCards = true;
        }
        else
        {
            NoFaceCards = false;
        }
    }
    private void AddSpirtesAndValuesToButtons()
    {
        for (int i = 0; i < CardButtonDataValues.Count; i++)
        {

            CardButtonDataValues[i].ButtonSpirteImage.sprite = ShuffleData[i].CardImage;
            CardButtonDataValues[i].CardButtonValue = ShuffleData[i].CardValue;
            //Debug.Log("Data : " + ShuffleData[i].CardValue);
            CardButtonDataValues[i].FaceCardType = ShuffleData[i].FaceCardType;
            CardButtonDataValues[i].ButtonSpirteImage.rectTransform.sizeDelta = FlipCard.sizeDelta;
            CardButtonDataValues[i].CardButton.interactable = true;
            if (ShuffleData[i].CardValue!=-1)
            {
                ShuffleData.Remove(ShuffleData[i]);
            }
        }

         
        Invoke(nameof(DelayAndChangeCards), 1f);
    }


    private void AddSpirtesAndValuesToButtonsV2()
    {
        for (int i = 0; i < CardButtonDataValues.Count; i++)
        {

            CardButtonDataValues[i].ButtonSpirteImage.sprite = ShuffleData[i].CardImage;
            CardButtonDataValues[i].CardButtonValue = ShuffleData[i].CardValue;
            //Debug.Log("Data : " + ShuffleData[i].CardValue);
            CardButtonDataValues[i].FaceCardType = ShuffleData[i].FaceCardType;
            CardButtonDataValues[i].ButtonSpirteImage.rectTransform.sizeDelta = FlipCard.sizeDelta;
            CardButtonDataValues[i].CardButton.interactable = true;
            if (ShuffleData[i].CardValue != -1)
            {
                ShuffleData.Remove(ShuffleData[i]);
            }
        }


        Invoke(nameof(DelayAndChangeCardsv2), 1F);
    }

    private void DelayAndChangeCardsv2()
    {

        //check for face cards then replace
        for (int k = 0; k < CardButtonDataValues.Count; k++)
        {
            if (CardButtonDataValues[k].CardButtonValue == -1)
            {

                CardButtonDataValues[k].ButtonSpirteImage.sprite = CardSprite;
                CardButtonDataValues[k].ButtonSpirteImage.rectTransform.sizeDelta = NormalCard.sizeDelta;
            }
        }



        Invoke(nameof(ChangeFaceCardsWithNumbers), 0.1f);
    }
    private void DelayAndChangeCards()
    {
       
        //check for face cards then replace
        for (int k = 0; k < CardButtonDataValues.Count; k++)
        {
            if (CardButtonDataValues[k].CardButtonValue == -1)
            {
               
                CardButtonDataValues[k].ButtonSpirteImage.sprite = CardSprite;
                CardButtonDataValues[k].ButtonSpirteImage.rectTransform.sizeDelta = NormalCard.sizeDelta;
            }
        }

       

        Invoke(nameof(ChangeFaceCardsWithNumbers), 1f);
    }

    private void ChangeFaceCardsWithNumbers()
    {
        for (int k = 0; k < CardButtonDataValues.Count; k++)
        {
            if (CardButtonDataValues[k].CardButtonValue == -1)
            {
                for(int j = 0;j<ShuffleData.Count;j++)
                {
                    if(ShuffleData[j].CardValue!=-1)
                    {
                        CardButtonDataValues[k].ButtonSpirteImage.sprite = ShuffleData[j].CardImage;
                        CardButtonDataValues[k].CardButtonValue = ShuffleData[j].CardValue;
                        CardButtonDataValues[k].FaceCardType = ShuffleData[j].FaceCardType;
                        CardButtonDataValues[k].ButtonSpirteImage.rectTransform.sizeDelta = FlipCard.sizeDelta;
                        ShuffleData.Remove(ShuffleData[j]);
                        break;
                    }
                }
            }
        }

        CanClick = true;
    }
    int CurrentFaceupCards = 0;
    bool levelfailed = true;
    private bool OnlyOnceHitApi = true;
    private bool CanClick;
    private void CheckLevelWinLevelFail()
    {
        CurrentFaceupCards = 0;
        levelfailed = true;
        for (int i = 0;i<CardButtonDataValues.Count;i++)
        {
            if(CardButtonDataValues[i].CardButtonValue==-1)
            {
                CurrentFaceupCards++;
            }
        }
            for (int i = 0; i < CardButtonDataValues.Count; i++)
            {
                for (int j = i + 1; j < CardButtonDataValues.Count; j++)
                {
                    int sum = CardButtonDataValues[i].CardButtonValue + CardButtonDataValues[j].CardButtonValue;
                    if (sum == 10 || CardButtonDataValues[i].CardButtonValue == 10 || CardButtonDataValues[j].CardButtonValue == 10)
                    {
                        levelfailed = false;
                    }
                }
            }
        if(CurrentFaceupCards== CardButtonDataValues.Count)
        {
            LevelWinPanel.SetActive(true);
            EndTimer = true;
            if (gameType == GameType.challenges)
            {

            }
            else
            {
                if(LevelWinPanel.activeInHierarchy)
                {
                    StartCoroutine(DelayAndBackToMainMenu());
                }
               
            }
           
            
        }
        if(levelfailed && !LevelWinPanel.activeInHierarchy)
        {
            EndTimer = true;
            levelFailPanel.SetActive(true);
            StartCoroutine(DelayAndBackToMainMenu());

        }
       
    }



    IEnumerator DelayAndBackToMainMenu()
    {
        yield return new WaitForSecondsRealtime(3f);
        OnStartGame();
        //MainMenuController.Instance.BackToMainMenu();
    }
    public void Flip()
    {
        StartCoroutine(Flip90(transform, 0.25f, true));
    }

    private IEnumerator Flip90(Transform thisTransform, float time, bool changeSprite)
    {
        Quaternion startRotation = thisTransform.rotation;
        Quaternion endRotation = thisTransform.rotation * Quaternion.Euler(new Vector3(0, 90, 0));
        float rate = 1.0f / time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            thisTransform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            yield return null;
        }
       
    }

    [System.Serializable]
    public class CardData
    {
        public Sprite CardImage;
        public int CardValue;
        public char FaceCardType;
    }

    [System.Serializable]
    public class CardButtonData
    {
        public Image ButtonSpirteImage;
        public Button CardButton;
        public int CardButtonValue;
        public char FaceCardType;
    }

    [System.Serializable]
    public class WinCombo
    {
        public List<CardData> ComboList = new List<CardData>(); 
    }


   
    public void JokerCardButtonPressed()
    {
        if(CurrentItemQuantityJoker> 0)
        {
            JokeruUsed = true;
            CurrentItemQuantityJoker--;
            
            if(CurrentItemQuantityJoker <= 0)
            {
                cardtypeactive = CardTypeActive.none;
            }
            if(CurrentItemQuantityJoker > 0)
            {
                JokerCardText.text = CurrentItemQuantityJoker.ToString("00");
            }

            ApiCode.Instance.SetDataToAPIItemUse(2);
            SetAllJokerCardsNormal();
            JokerCardPackData = new List<CardData>(EnterCardData);
           
            Shuffle(JokerCardPackData);
            JokerActivated = true;
        }
    }

    private void SetAllJokerCardsNormal()
    {
        for(int i = 0;i <JokerCards.Count;i++)
        {
            JokerCards[i].CardButton.transform.localScale = new Vector3(CardScaleMin, CardScaleMin, CardScaleMin);
            JokerCards[i].CardButton.transform.position = JokerInitialPositions[i].position;
            JokerCards[i].CardButton.gameObject.SetActive(true);
            Color tempcolor = JokerCards[i].ButtonSpirteImage.color;
            tempcolor.a = 1f;
            JokerCards[i].ButtonSpirteImage.color = tempcolor;
        }
    }

    public void JokerCardIndexButtons(int index)
    {
        if(!JokerCardSelectedMoveAnimationsInProcess)
        {
            CurrentJokerCardSelected = index;
            JokerCardSelected = true;
            CurrentJokerTimer = 0f;
            StopAllCoroutines();
            StartCoroutine(SetJokerSelected(index));
        }
       
    }
    public void ShuffleModule()
    {
        if (CurrentItemQuantityShuffle > 0)
        {
            ShuffleUsed = true;
            ShuffleCard.gameObject.SetActive(false);
            Jokercard.SetActive(false);
            CurrentItemQuantityShuffle--;
            if (CurrentItemQuantityShuffle <= 0)
            {
                cardtypeactive = CardTypeActive.none;
            }
            if (CurrentItemQuantityShuffle > 0)
            {
                ShuffleCardText.text = CurrentItemQuantityShuffle.ToString("00");
            }
            ApiCode.Instance.SetDataToAPIItemUse(1);
            Shuffle(ShuffleData);
            AddDataAndImagesToListShuffleModule();
            Invoke(nameof(AddCardSpirtesAndValuesToButtonsForShuffleModule), 0.1f);
            Invoke(nameof(AddBackDataAndImages), 1f);
        }
    }

    private void AddCardSpirtesAndValuesToButtonsForShuffleModule()
    {
        for (int i = 0; i < CardButtonDataValues.Count; i++)
        {
            CardButtonDataValues[i].ButtonSpirteImage.sprite = CardSprite;
           
            //CardButtonDataValues[i].CardButtonValue = 0;
            CardButtonDataValues[i].ButtonSpirteImage.rectTransform.sizeDelta = NormalCard.sizeDelta;
            CardButtonDataValues[i].CardButton.interactable = true;
        }

       
    }


    private void AddDataAndImagesToListShuffleModule()
    {

        ShuffleDataShuffleModule.Clear();
        for (int i = 0;i<CardButtonDataValues.Count;i++)
        {
            ShuffleDataShuffleModule.Add(ShuffleData[i]);
            ShuffleDataShuffleModule[i].CardValue = CardButtonDataValues[i].CardButtonValue;
            ShuffleDataShuffleModule[i].CardImage = CardButtonDataValues[i].ButtonSpirteImage.sprite;
           
        }
        Shuffle(ShuffleDataShuffleModule);
    }

    private void AddBackDataAndImages()
    {
        for (int i = 0; i < CardButtonDataValues.Count; i++)
        {

            //Debug.Log("Sdata " + ShuffleDataShuffleModule[i].CardValue);
            //Debug.Log("Cdata " + CardButtonDataValues[i].CardButtonValue);

          CardButtonDataValues[i].CardButtonValue = ShuffleDataShuffleModule[i].CardValue;
            CardButtonDataValues[i].FaceCardType = ShuffleDataShuffleModule[i].FaceCardType;
            CardButtonDataValues[i].ButtonSpirteImage.sprite = ShuffleDataShuffleModule[i].CardImage;
            CardButtonDataValues[i].ButtonSpirteImage.rectTransform.sizeDelta = FlipCard.sizeDelta;
            if (CardButtonDataValues[i].CardButtonValue <0)
            {
                CardButtonDataValues[i].CardButton.interactable = false;
            }
           else
            {
                CardButtonDataValues[i].CardButton.interactable = true;
            }

        }

        WhatStoreCardsToEnable();
    }
}
