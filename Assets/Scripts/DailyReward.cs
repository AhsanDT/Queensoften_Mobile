using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    // Start is called before the first frame update

    public int _DailyReward = 5;
    public int StreakDay = 7;
    public int StreakReward = 50;

    private int CurrentReward;

    public Button DailyButton;
    public Button StreakButton;

    public GameObject DailyRewardPopup;
    public Text CurrentDay;
    public Text PopUpReward;
    public Text DailyRewardText;
    public Text StreakRewardText;


    private static DailyReward instance;

    public static DailyReward Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DailyReward>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    void OnEnable()
    {
       // Debug.LogError(DateTime.Now);
        ////DateTime a = DateTime.Now;
        ////DateTime b = a.AddDays(1).Date;
        ////Debug.LogError("Day01 " + a + " Day02 " + b);
        ////double secondsUntilMidnight = (b - a).TotalHours;
        ////Debug.LogError("secondsUntilMidnight " + secondsUntilMidnight);
        //////// Console.WriteLine(b.Subtract(a).TotalMinutes);
        ////// Debug.LogError(b.Subtract(a).TotalHours);

        CheckDailyReward();
    }



   


    private void CheckDailyReward()
    {
        DailyButton.interactable = false;
        StreakButton.interactable = false;
        string SaveDateTime = PlayerPrefsHandler.GetDateTime;
        if (SaveDateTime == "")
        {
            SetCurretTime();
            PlayerPrefsHandler.GetSetStreakDay++;
            CurrentReward = _DailyReward;
            DailyRewardPopup.SetActive(true);
            DailyButton.interactable = true;
        }
        else
        {
            DateTime SaveTime;
            DateTime.TryParse(SaveDateTime, out SaveTime);
            DateTime CurrentTime = DateTime.Now;
            Double Hours = CurrentTime.Subtract(SaveTime).Hours;
            DateTime UntilMidNight = SaveTime.AddDays(1).Date;
            double MinutesUntilMidnight = (UntilMidNight - CurrentTime).TotalMinutes;
            if (MinutesUntilMidnight <= 1)
            {
                SetCurretTime();
                PlayerPrefsHandler.GetSetStreakDay++;
                PlayerPrefsHandler.DailyRewardOnce = 1;
                PlayerPrefsHandler.StreakRewardOnce = 1;
                DailyRewardPopup.SetActive(true);
            }
            if (Hours >= 24)
            {
                PlayerPrefsHandler.GetSetStreakDay = 1;
            }
            if (PlayerPrefsHandler.GetSetStreakDay == StreakDay)
            {
                if(PlayerPrefsHandler.StreakRewardOnce==1)
                {
                    StreakButton.interactable = true;
                }
                else
                {
                    StreakButton.interactable = false;
                }
                CurrentReward = StreakReward;
            }
            else
            {
                if (PlayerPrefsHandler.DailyRewardOnce == 1)
                {
                    DailyButton.interactable = true;
                }
                else
                {
                    DailyButton.interactable = false;
                }
                CurrentReward = _DailyReward;
            }
        }
        CurrentDay.text = "Day " + PlayerPrefsHandler.GetSetStreakDay;
        PopUpReward.text = "Coins " + CurrentReward;
        DailyRewardText.text = "Coins " + _DailyReward.ToString();
        StreakRewardText.text = "Coins " + StreakReward.ToString();
        if (PlayerPrefsHandler.GetSetStreakDay == StreakDay)
        {
            PlayerPrefsHandler.GetSetStreakDay = 1;
        }
    }
    private void SetCurretTime()
    {
        string SaveDateTime;
        //DateTime NewDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
        DateTime NewDate = DateTime.Now;
        SaveDateTime = NewDate.ToString();
        PlayerPrefsHandler.GetDateTime = SaveDateTime;
    }


    public void ClaimRewardFromPopup()
    {
        if(CurrentReward==_DailyReward)
        {
            PlayerPrefsHandler.DailyRewardOnce = 0;
            DailyButton.interactable = false;
        }
        else
        {
            PlayerPrefsHandler.StreakRewardOnce = 0;
            StreakButton.interactable = false;
        }
        PlayerPrefsHandler.Coins += CurrentReward;
    }

    public void ClaimDailyReward()
    {
        PlayerPrefsHandler.Coins += _DailyReward;
        DailyButton.interactable = false;
        PlayerPrefsHandler.DailyRewardOnce = 0;
    }

    public void ClaimStreakReward()
    {
        PlayerPrefsHandler.Coins += StreakReward;
        StreakButton.interactable = false;
        PlayerPrefsHandler.StreakRewardOnce = 0;
    }

    public void AdToDoubleReward()
    {
        PlayerPrefsHandler.Coins += (CurrentReward * 2);
    }
}
    

