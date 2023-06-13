using BeatEmUpTemplate;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private static MainMenuController instance;
    public static MainMenuController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MainMenuController>();
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


   

    private void PlayMainMusic()
    {
        GlobalAudioPlayer.PlayMusic("MainMusic");
    }
    [Header("Panels")]
    public GameObject GoogleLoginButton;
    public GameObject AppleLoginButton;
    public GameObject mainMenuPannel;
    public GameObject MainScreen;
    public GameObject QueenMenu;
    public GameObject Store;
    public GameObject FreePlayPannel;
    public GameObject TodaysChallenge;
    public GameObject Profile;
    public GameObject SomeThingWentWrong;
    public Text TodaysChallengeTitle;
    public FreePlayGameLogic freePlayGameLogicScript;
    public FreePlaySetting freePlaySetting;

    [Header("All Screens")]
    public GameObject[] AllScreens;
    public GameObject QuitPannel;
    public GameObject ItemSelected;
    public GameObject PurchasedMade;
    public GameObject TutorialScreen;
    public bool HasCurrentChallengeId;
    public bool IsChallengeHardCoded;
    public int CurrentChallengeId;
    public string CurrentChallengeString;

    [Header("Audio Settings")]
    public GameObject VolumeOffButton;
    public GameObject VolumeOnButton;

    private AudioSource MusicSource;

    private void Start()
    {
        CheckVolume();
        Invoke(nameof(PlayMainMusic), 0.5f);
        Invoke(nameof(InializeSounds), 1f);
    }

    //private void Update()
    //{
    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        if (mainMenuPannel.activeInHierarchy || MainScreen.activeInHierarchy)
    //        {
    //            if (Input.GetKeyDown(KeyCode.Escape))
    //            {
    //                CheckCurrentScreen();
    //                //Application.Quit();
    //            }
    //        }
    //    }
    //}
    public void InializeSounds()
    {
        if (GameObject.Find("Music"))
        {
            MusicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        }
        //if (MusicSource)
        //{
        //    MusicSource.volume = PlayerPrefsHandler.CurrentMusicVolume;
        //    //AudioListener.volume = PlayerPrefsHandler.CurrentVolume;
        //}
    }

    private void CheckVolume()
    {
        if (PlayerPrefsHandler.CurrentMusicVolume == 0)
        {


            VolumeOnButton.SetActive(true);
            VolumeOffButton.SetActive(false);
        }
        else
        {
            VolumeOnButton.SetActive(false);
            VolumeOffButton.SetActive(true);
        }
    }

    float CurrentSound;
    public void MuteSound()
    {
        ButtonClickSound();
      
       // PlayerPrefsHandler.CurrentMusicVolume = 0;
        if (MusicSource)
        {
            CurrentSound = MusicSource.volume;
            MusicSource.volume = 0;
            //AudioListener.volume = PlayerPrefsHandler.CurrentVolume;

        }

        VolumeOnButton.SetActive(true);
        VolumeOffButton.SetActive(false);
    }

    public void UnMuteSound()
    {
        ButtonClickSound();
        //PlayerPrefsHandler.CurrentMusicVolume = 1;
        if (MusicSource)
        {
            MusicSource.volume = CurrentSound;
            //AudioListener.volume = PlayerPrefsHandler.CurrentVolume;

        }

        VolumeOnButton.SetActive(false);
        VolumeOffButton.SetActive(true);
    }

    private void CheckCurrentScreen()
    {
        if (AllScreens[0].activeInHierarchy  &&  !CheckAllScreensAreActive())
        {
            QuitPannel.SetActive(true);
        }
        else
        {
            for(int i = 1; i<AllScreens.Length;i++)
            {
                if(AllScreens[i].activeInHierarchy)
                {
                    if(AllScreens[i].transform.name=="FreePlay" || AllScreens[i].transform.name == "QueenMenu")
                    {
                        AllScreens[i].SetActive(false);
                        if (!AllScreens[i - 1].activeInHierarchy)
                        {
                            AllScreens[i - 1].SetActive(true);
                        }
                    }
                    else
                    {
                        AllScreens[2].SetActive(true);
                        AllScreens[i].SetActive(false);
                        ItemSelected.SetActive(false);
                        PurchasedMade.SetActive(false);

                    }
                   
                }
            }
        }
    }

    private bool CheckAllScreensAreActive()
    {
        for(int i = 1; i<AllScreens.Length;i++)
        {
            if(AllScreens[i].activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    

   public void FreePlay()
    {
        ButtonClickSound();
        freePlayGameLogicScript.gameType = FreePlayGameLogic.GameType.freeplay;
        mainMenuPannel.SetActive(false);
        FreePlayPannel.SetActive(true);
    }

    public void TimedPlay()
    {
        ButtonClickSound();
        freePlayGameLogicScript.gameType = FreePlayGameLogic.GameType.timed;
        mainMenuPannel.SetActive(false);
        FreePlayPannel.SetActive(true);
    }

    public void ChallengesPlay()
    {
        ButtonClickSound();
        freePlayGameLogicScript.gameType = FreePlayGameLogic.GameType.challenges;
        mainMenuPannel.SetActive(false);
        FreePlayPannel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        ButtonClickSound();
        mainMenuPannel.SetActive(false);
        FreePlayPannel.SetActive(false);
    }
    public void SetActiveQueenMenu()
    {
        ButtonClickSound();
        QueenMenu.SetActive(true);
    }

    public void SetActiveStore()
    {
        ButtonClickSound();
        Store.SetActive(true);
    }

    public class NewJSONAchievement
    {
        public Data data;
    }
    public class Data
    {
        public AllChallenges challenge;
    }
    public class AllChallenges
    {
        public int id;
        public string title;
        public string date;
        public string hour;
        public string minute;
        public string games;
        public string occurrence;
        public string active;
        public string prize;
        public bool hard_coded;
    }

    public void GetCurrentChallenge()
    {
        ApiCode.Instance.SetDataTOAPICurrentChallenge();
    }

    public void GetDataFromJSonChallenge(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<NewJSONAchievement>(jsonwithData);
        NewJSONAchievement MyJsonClass = JsonConvert.DeserializeObject<NewJSONAchievement>(jsonwithData);

        GetChallengeData(MyJsonClass);
        Debug.Log("Success Challenge");
    }


    private void GetChallengeData(NewJSONAchievement MyJsonClass)
    {
        if(MyJsonClass.data.challenge!=null)
        {
            HasCurrentChallengeId = true;
            CurrentChallengeId = MyJsonClass.data.challenge.id;
            CurrentChallengeString = MyJsonClass.data.challenge.title;
            TodaysChallengeTitle.text = CurrentChallengeString;
            IsChallengeHardCoded = MyJsonClass.data.challenge.hard_coded;
        }
        else
        {
            HasCurrentChallengeId = false;
            TodaysChallengeTitle.text = "No Challenge Today";
        }
        

        TodaysChallenge.SetActive(true);
    }

    public void BackTomainScreen()
    {
        MainScreen.SetActive(true);
        FreePlayPannel.SetActive(false);
    }

    public void ButtonClickSound()
    {
     //   AudioPlayer.Instance.playSFX("ButtonClick");
       // GlobalAudioPlayer.PlaySFX("ButtonClick");
    }
    public void CorrectCardSound()
    {
      //  AudioPlayer.Instance.playSFX("Correct_Select");
        //GlobalAudioPlayer.PlaySFX("Correct_Select");
    }
    public void WrongCardSound()
    {
       // AudioPlayer.Instance.playSFX("Wrong_Select");
        //GlobalAudioPlayer.PlaySFX("Wrong_Select");
    }
    public void CardSoundClick()
    {
       // AudioPlayer.Instance.playSFX("Select_Card");
        //  GlobalAudioPlayer.PlaySFX("Select_Card");
    }
}
