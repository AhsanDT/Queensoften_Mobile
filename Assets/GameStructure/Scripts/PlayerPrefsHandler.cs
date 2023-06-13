using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsHandler : MonoBehaviour
{
    static string PP_coins = "Coins";
    static string PP_playerSelected = "playerSelected";
    static string PP_selectedLevel = "SelectedLevel";
    static string PP_selectedEnvironment = "SelectedEnvironment";
    static string PP_selectedLevelWRTEnv = "SelectedLevelWRTEnv";
    static string PP_unlockedLevelWRTEnv = "UnlockLevelWRTEnv";
    static string PP_NoPopsUps = "PP_NoPopsUps";
    static string PP_CharacterUnlocked = "PP_CharacterUnlocked";
    static string PP_UnlockCharacters = "PP_UnlockCharacter";
    static string PP_UnlockLevels = "PP_UnlockLevels";
    static string PP_UnlockEverthing = "PP_UnlockEverthing";
    static string PP_DateTime = "PP_DateTime";
    static string PP_DateTimeForAds = "PP_DateTimeForAds";
    static string PP_StreakDays = "PP_StreakDays";
    static string PP_DailyRewardOnce = "PP_DailyRewardOnceInDay";
    static string PP_StreakRewardOnce = "PP_StreakRewardOnce";
    static string PP_Adtype = "PP_Adtype";
    static string PP_Banner = "PP_Banner";
    static string PP_InterstitialID = "PP_InterstitialID";
    static string PP_RewardId = "PP_RewardId";
    static string PP_AutoLogin = "PP_AutoLogin";
    static string PP_AutoFaceBook = "PP_AutoFaceBook";
    static string PP_AutoAppleLoginIn = "PP_AutoAppleLoginIn";
    static string PP_AppleDataOnce = "PP_AppleDataOnce";


    static string PP_Google_Email = "PP_Google_Email";
    static string PP_Google_Name = "PP_Google_Name";
    static string PP_Google_ImageUrl = "PP_Google_ImageUrl";


    static string PP_FaceBook_Email = "PP_FaceBook_Email";
    static string PP_FaceBook_Name = "PP_FaceBook_Name";
    static string PP_FaceBook_ImageUrl = "PP_FaceBook_ImageUrl";
    static string PP_FaceBook_ID = "PP_FaceBook_ID";
    static string PP_DebugIAPs = "PP_DebugIAPs";
    //static string

    static string PP_CurrentMusicVolume = "PP_CurrentMusicVolume";
    //static string


    public static float CurrentMusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(PP_CurrentMusicVolume, 1);
        }
        set
        {

            PlayerPrefs.SetFloat(PP_CurrentMusicVolume, value);
        }
    }
    public static int DebugIAP
    {
        get
        {
            return PlayerPrefs.GetInt(PP_DebugIAPs, 0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_DebugIAPs, value);
        }
    }


    public static string FaceBook_ID
    {
        get
        {
            return PlayerPrefs.GetString(PP_FaceBook_ID, "");
        }
        set
        {

            PlayerPrefs.SetString(PP_FaceBook_ID, value);
        }
    }
    public static string FaceBook_Email
    {
        get
        {
            return PlayerPrefs.GetString(PP_FaceBook_Email, "");
        }
        set
        {

            PlayerPrefs.SetString(PP_FaceBook_Email, value);
        }
    }
    public static string FaceBook_Name
    {
        get
        {
            return PlayerPrefs.GetString(PP_FaceBook_Name, "");
        }
        set
        {

            PlayerPrefs.SetString(PP_FaceBook_Name, value);
        }
    }
    public static string FaceBook_ImageUrl
    {
        get
        {
            return PlayerPrefs.GetString(PP_FaceBook_ImageUrl, "");
        }
        set
        {

            PlayerPrefs.SetString(PP_FaceBook_ImageUrl, value);
        }
    }
    public static string Google_ImageUrl
    {
        get
        {
            return PlayerPrefs.GetString(PP_Google_ImageUrl, "");
        }
        set
        {

            PlayerPrefs.SetString(PP_Google_ImageUrl, value);
        }
    }
    public static string Google_Name
    {
        get
        {
            return PlayerPrefs.GetString(PP_Google_Name, "");
        }
        set
        {

            PlayerPrefs.SetString(PP_Google_Name, value);
        }
    }
    public static string Google_Email
    {
        get
        {
            return PlayerPrefs.GetString(PP_Google_Email, "");
        }
        set
        {

            PlayerPrefs.SetString(PP_Google_Email, value);
        }
    }
    public static int AppleDataOnce
    {
        get
        {
            return PlayerPrefs.GetInt(PP_AppleDataOnce, 0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_AppleDataOnce, value);
        }
    }
    public static int AutoAppleLoginIn
    {
        get
        {
            return PlayerPrefs.GetInt(PP_AutoAppleLoginIn, 0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_AutoAppleLoginIn, value);
        }
    }
    public static int AutoLoginFacebook
    {
        get
        {
            return PlayerPrefs.GetInt(PP_AutoFaceBook, 0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_AutoFaceBook, value);
        }
    }
    public static int AutoLogin
    {
        get
        {
            return PlayerPrefs.GetInt(PP_AutoLogin,0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_AutoLogin, value);
        }
    }
    public static string RewardId
    {
        get
        {
            return PlayerPrefs.GetString(PP_RewardId, "ca-app-pub-3940256099942544/5224354917");
        }
        set
        {

            PlayerPrefs.SetString(PP_RewardId, value);
        }
    }
    public static string InterstitialID
    {
        get
        {
            return PlayerPrefs.GetString(PP_InterstitialID, "ca-app-pub-3940256099942544/1033173712");
        }
        set
        {

            PlayerPrefs.SetString(PP_InterstitialID, value);
        }
    }

    public static string BannerId
    {
        get
        {
            return PlayerPrefs.GetString(PP_Banner, "ca-app-pub-3940256099942544/630097811");
        }
        set
        {

            PlayerPrefs.SetString(PP_Banner, value);
        }
    }
    public static string AdType
    {
        get
        {
            return PlayerPrefs.GetString(PP_Adtype, "");
        }
        set
        {

            PlayerPrefs.SetString(PP_Adtype, value);
        }

    }
    public static int StreakRewardOnce
    {
        get
        {
            return PlayerPrefs.GetInt(PP_StreakRewardOnce, 1);
        }
        set
        {

            PlayerPrefs.SetInt(PP_StreakRewardOnce, value);
        }
    }
    public static int DailyRewardOnce
    {
        get
        {
            return PlayerPrefs.GetInt(PP_DailyRewardOnce, 1);
        }
        set
        {

            PlayerPrefs.SetInt(PP_DailyRewardOnce, value);
        }
    }
    public static int GetSetStreakDay
    {
        get
        {
            return PlayerPrefs.GetInt(PP_StreakDays,0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_StreakDays, value);
        }
    }
    public static string GetDateTime
    {
        get
        {
            return PlayerPrefs.GetString(PP_DateTime, "");
        }
        set
        {

            PlayerPrefs.SetString(PP_DateTime, value);
        }
    }


    public static string GetDateTimeForAds
    {
        get
        {
            return PlayerPrefs.GetString(PP_DateTimeForAds, "");
        }
        set
        {

            PlayerPrefs.SetString(PP_DateTimeForAds, value);
        }
    }
    public static int UnlockEverthing
    {
        get
        {
            return PlayerPrefs.GetInt(PP_UnlockEverthing, 0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_UnlockEverthing, value);
        }
    }
    public static int UnlockLevels
    {
        get
        {
            return PlayerPrefs.GetInt(PP_UnlockLevels, 0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_UnlockLevels, value);
        }
    }
    public static int UnlockCharacters
    {
        get
        {
            return PlayerPrefs.GetInt(PP_UnlockCharacters, 0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_UnlockCharacters, value);
        }
    }
    public static int GetUnlockedCharacters(int CharNo)
    {
        return PlayerPrefs.GetInt(PP_CharacterUnlocked + CharNo, 0);
    }
    public static void SetUnlockedCharacters(int CharNo, int value)
    {
        PlayerPrefs.SetInt(PP_CharacterUnlocked + CharNo, value);
    }
    

    public static int NoPopUps
    {
        get
        {
            return PlayerPrefs.GetInt(PP_NoPopsUps, 0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_NoPopsUps, value);
        }
    }
    public static int Coins
    {
        get
        {
            return PlayerPrefs.GetInt(PP_coins, 0);
        }
        set
        {

            PlayerPrefs.SetInt(PP_coins, value);
        }
    } 
    public static int PlayerSelected
    {
        get
        {
            return PlayerPrefs.GetInt(PP_playerSelected, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PP_playerSelected, value);
        }
    } 

    public static int SelectedEnvironment
    {
        get
        {
            return PlayerPrefs.GetInt(PP_selectedEnvironment, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PP_selectedEnvironment, value);
        }
    }
    //public static int GetSelectedLevelWRTEnv(int envNo)
    //{
    //    return PlayerPrefs.GetInt(PP_selectedLevelWRTEnv + envNo, 0);
    //}
    //public static void SetSelectedLevelWRTEnv(int envNo, int value)
    //{
    //    PlayerPrefs.SetInt(PP_selectedLevelWRTEnv + envNo, value);
    //}
    public static int SelectedLevelWRTEnv
    {
        get
        {
            return PlayerPrefs.GetInt(PP_selectedLevelWRTEnv + PlayerPrefsHandler.SelectedEnvironment, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PP_selectedLevelWRTEnv + PlayerPrefsHandler.SelectedEnvironment, value);
        }
    }






    public static int GetUnlockedLevelWRTEnv(int envNo)
    {
        return PlayerPrefs.GetInt(PP_unlockedLevelWRTEnv + envNo, 0);
    }
    public static void SetUnlockedLevelWRTEnv(int envNo, int value)
    {
        PlayerPrefs.SetInt(PP_unlockedLevelWRTEnv + envNo, value);
    }
    public static int SelectedUnlockedLevelWRTEnv
    {
        get
        {
            return PlayerPrefs.GetInt(PP_unlockedLevelWRTEnv + PlayerPrefsHandler.SelectedEnvironment, 1);
        }
        set
        {
            PlayerPrefs.SetInt(PP_unlockedLevelWRTEnv + PlayerPrefsHandler.SelectedEnvironment, value);
        }
    }
    public static int GetSelectedUnlockedLevelWRTEnvFunc(int env)
    {
        return PlayerPrefs.GetInt(PP_unlockedLevelWRTEnv + env, 1);
    }
    public static void SetSelectedUnlockedLevelWRTEnvFunc(int env, int no)
    {
        PlayerPrefs.SetInt(PP_unlockedLevelWRTEnv + env, no);
    }
}
