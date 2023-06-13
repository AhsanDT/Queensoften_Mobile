using GooglePlayGames;
using Newtonsoft.Json;
using SignInSample;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ApiCode : MonoBehaviour
{

    public enum LoginType { none ,google,facebook, apple };
    public LoginType login_type;
    [Header("Login API")]
    public string URL;
    public string Header_Token;
    [Header("LevelWin/Fail API")]
    public string LevelWinFailApi;
    public string LevelWinFailApiConcatEnd;
    [Header("GuideLines API")]
    public string TermsAndConditonsApi;

    public TextMeshProUGUI TermsAndConditionsTextPro;
    public Text TermsAndConditionsText;
    public TextMeshProUGUI AboutUsTextPro;
    public Text AboutUsText;
    public TextMeshProUGUI FaqTextPro;
    public Text FaqText;
    [Header("Store API")]
    public string StoreAPI;
    public StoreData store_data;
    [Header("UserItems API")]
    public string UserItemID;
    public string UserItemIDEnd;
    public MyItemsData MyItemsData;
    public GameObject ItemSelected;
    [Header("Purchase API")]
    public string PurchaseAPI;
    public string PurchaseAPIEnd;
    public GameObject PurchaseMade;
    [Header("Challenges And Achievement API")]
    public string AchievementAPI;
    public string AchievementAPIEnd;
    public string AchievementCheck;
    public string AchievementUpdate;
    public GameObject NewAchivementUnlocked;
    public GameObject ChallengeCompletedOnlogin;
    public GameObject ChallengeCompleteAchievementTextObject;
    public Text ChallengeCompleteAchievementTextObjectText;
    public string ChallengeAPI;
    public string ChallengeAPIEnd;
    
    public MyAchievements myAchievements;
    [Header("Item Use API")]
    public string ItemUseAPI;
    public string ItemUseAPIEnd;

    [Header("Skin Api")]
    public string SkinApi;
    public string SkinApiEnd;

    [Header("Stats Get API")]
    public string StatsApi;
    public string StatApiEnd;
    public MyStats mystats;
    [Header("Share Link API")]
    public string ShareLinkAPI;
    public string ShareLinkURL;
    [Header("Support API")]
    public string SupportAPI;
    [Header("Delete API")]
    public string DeleteAPI;
    [Header("No InternetPanel")]
    public GameObject NoInternetPanel;
    public Text PanelText;
    public GameObject LoginPanel;
    public GameObject FreePlayPannel;
    public GameObject StorePanel;
    public GameObject MyItemsPanel;
    public GameObject NoEmailFound;
   
    public GameObject LoaderObject;
    public string NoInternetText;
    public string LoadingText;
    public string FacebookEmailError;

    [Header("Login Fail")]
    public GameObject LoginFail;
    public Text LoginFailText;
    public string DefaultLoginFailText;
    [Header("LogOut")]
    public GameObject[] PanelsToDisable;
    public FacebookScript facebookscript;
    public SigninSampleScript signinsamplescript;
    public AppleLogin appleloginscript;
    public string json;
    public string USerId = "1";

    [HideInInspector]
    public string UrlImage;
    [HideInInspector]
    public string EmailOfUser = "LoginFromFaceBook";
    [HideInInspector]
    public string UserNameFacebook;

    public string FaceBookid;
    [HideInInspector]
    public int CurrenCardSkin;


    [Header("Debug")]
    public bool UseDebug;
    public int DebugJokerValues;
    public int DebugShuffleValues;


    private string FullLevelWinFailApi;
    private string FullMyItemsApi;
    private string FullPurchaseApi;
    private string FullItemUseApi;
    private string FullSkinUseApi;
    private string FullMyStatsUseApi;
    private string FullMyAchievementsUseApi;
    private string FullMyChallengeUseApi;
    private bool noInternet;
    private string Type;
    private bool IsInternetAvailable;
    private bool LoginOnce = true;
    private bool StoreOnce = true;
    private bool MyItemsOnce = true;

    
    private static ApiCode instance;


    string FreePlay = "FreePlay";
    string Timed = "Timed";

    public static ApiCode Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ApiCode>();
            }
            return instance;
        }
    }

   public void LogOutUSer()
    {
        try
        {
            if (PlayerPrefsHandler.AutoLogin == 1)
            {
                signinsamplescript.OnSignOut();
            }

            if (PlayerPrefsHandler.AutoLoginFacebook == 1)
            {
                facebookscript.FaceBookLogOut();
            }

            if (PlayerPrefsHandler.AutoAppleLoginIn == 1)
            {
                PlayerPrefsHandler.AutoAppleLoginIn = 0;
            }
            LoaderObject.SetActive(true);
            Invoke(nameof(DeactiveLoader), 3f);
            MainMenuController.Instance.mainMenuPannel.SetActive(true);
            for (int i = 0; i < PanelsToDisable.Length; i++)
            {
                PanelsToDisable[i].SetActive(false);
            }

            PlayerPrefsHandler.AutoAppleLoginIn = 0;
            PlayerPrefsHandler.AutoLogin = 0;
            PlayerPrefsHandler.AutoLoginFacebook = 0;
        }
        catch
        {
            Debug.Log("LogOut Issue");
        }
       
    }
    private void DeactiveLoader()
    {
        LoaderObject.SetActive(false);
    }

    private void Awake()
    {
#if UNITY_ANDROID
        MainMenuController.Instance.GoogleLoginButton.SetActive(true);
        MainMenuController.Instance.AppleLoginButton.SetActive(false);

#elif UNITY_IOS
         MainMenuController.Instance.GoogleLoginButton.SetActive(false);
        MainMenuController.Instance.AppleLoginButton.SetActive(true);
#endif
        if (PlayerPrefsHandler.AutoLogin == 1)
        {
            login_type = LoginType.google;
        }
        if (PlayerPrefsHandler.AutoLoginFacebook == 1)
        {
            login_type = LoginType.facebook;
        }

        if(PlayerPrefsHandler.AutoAppleLoginIn==1)
        {
            login_type = LoginType.apple;
        }
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

       
        //SetDataToAPITermsAndConditions();
       
    }
    
    private void AddJokerAndShuffles()
    {
        SetDataToAPIPurchaseDebug(1, DebugJokerValues);
        SetDataToAPIPurchaseDebug(2, DebugShuffleValues);
        
    }
    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            IsInternetAvailable = false;
            NoInternetPanel.SetActive(true);
            PanelText.text = NoInternetText;
            LoginOnce = true;
        }
        else
        {
            if (LoginPanel.activeInHierarchy  && !FreePlayPannel.activeInHierarchy)
            {
                if(LoginOnce && !LoaderObject.activeInHierarchy)
                {
                    switch (login_type)
                    {
                        case LoginType.google:
                            {
                                if (PlayerPrefsHandler.AutoLogin == 1)
                                {
                                   //signinsamplescript.AutoLoginGmail();
                                    LoginOnce = false;
                                }

                                break;
                            }
                        case LoginType.facebook:
                            {
                                if (PlayerPrefsHandler.AutoLoginFacebook == 1)
                                {
                                   // facebookscript.checkSignIn();
                                    LoginOnce = false;
                                }

                                break;
                            }
                        case LoginType.apple:
                            {
                                if (PlayerPrefsHandler.AutoAppleLoginIn == 1)
                                {
                                    //appleloginscript.AppleAutoLogin();
                                    LoginOnce = false;
                                }

                                break;
                            }
                    }

                }
                
            }

            if (StorePanel.activeInHierarchy)
            {
                if (store_data.storeGroup.Count == 0)
                {
                    //NoInternetPanel.SetActive(true);
                    //PanelText.text = LoadingText;
                    LoaderObject.SetActive(true);
                    StoreOnce = true;
                }
                else
                {
                    if(StoreOnce)
                    {
                        LoaderObject.SetActive(false);

                        //NoInternetPanel.SetActive(false);
                        //PanelText.text = LoadingText;

                        StoreOnce = false;
                    }
                   
                }
            }
            
            if (MyItemsPanel.activeInHierarchy)
            {
                if (MyItemsData.UserItemsGroup.Count == 0)
                {
                    //NoInternetPanel.SetActive(true);
                    //PanelText.text = LoadingText;
                    LoaderObject.SetActive(true);
                    MyItemsOnce = true;
                }
                else
                {
                    if (MyItemsOnce)
                    {
                        //NoInternetPanel.SetActive(false);
                        //PanelText.text = LoadingText;
                        LoaderObject.SetActive(false);
                        MyItemsOnce = false;
                    }
                }
            }
            
            if(!StorePanel.activeInHierarchy && !MyItemsPanel.activeInHierarchy)
            {
                NoInternetPanel.SetActive(false);
            } 
            if (!IsInternetAvailable)
            {
                NoInternetPanel.SetActive(false);
                IsInternetAvailable = true;
            }
        }


    }
    public void setDataToApiLevelWinFail(string Game_Type, int won, int lost, string date, string time, int score , bool isChallenge =false , int challengeid=-1 ,int hardcoded = 0 , int challenge_win_hardcoded = 0)
    {
        try
        {
            StartCoroutine(SetDataCouroutineLevelWinFailAPi(Game_Type, won, lost, date, time, score, isChallenge, challengeid, hardcoded, challenge_win_hardcoded));
        }
        catch
        {
            Debug.Log("Level Win Fail APi Issue");
        }
        
    }

    public void SetDataToAPI(string Email, string Name, string Picture, string driver, string driver_Id)
    {
        try
        {
            StartCoroutine(SetDataCouroutine(Email, Name, Picture, driver, driver_Id));
        }
        catch
        {
            Debug.Log("Login API Issue");
        }
       
    }

    public void SetDataToAPIPurchase(int item_id, int quantity)
    {
        try
        {
            StartCoroutine(SetDataCouroutinePurchase(item_id, quantity));
        }
        catch
        {
            Debug.Log("Purchase API Issue");
        }
      
    }
    public void SetDataToAPIPurchaseDebug(int item_id, int quantity)
    {
        try
        {
            StartCoroutine(SetDataCouroutineDebugPurchases(item_id, quantity));
        }
        catch
        {
            Debug.Log("Purchase Debug API Issue");
        }
        
    }

    public void SetDataToAPIPurchaseOnChallenge(int item_id, int quantity)
    {
        try
        {
            StartCoroutine(SetDataCouroutinePurchaseChallenge(item_id, quantity));
        }
        catch
        {
            Debug.Log("Set Data To API Purchase On Challenge Debug API Issue");
        }
        
    }

    public void SetDataToAPIItemUse(int item_id, int quantity = 1)
    {
        try
        {
            StartCoroutine(SetDataCouroutineItemUse(item_id, quantity));
        }
        catch
        {
            Debug.Log("SetDataToAPIItemUse API Issue");
        }
        
    }

    public void SetDataToAPIItemSkinUse(int item_id, int quantity = 1)
    {
        try
        {
            StartCoroutine(SetDataCouroutineItemSkinUse(item_id, quantity));
        }
        catch
        {
            Debug.Log("SetDataToAPIItemSkinUse API Issue");
        }
       
    }


    public void SetDataToAPISupportMessage(string subject, string message)
    {
        try
        {
            StartCoroutine(SetDataCouroutineSupport(subject, message));
        }
        catch
        {
            Debug.Log("SetDataToAPISupportMessage API Issue");
        }
        
    }

    public void SetDataToAPIAchievementCheck()
    {
        try
        {
            StartCoroutine(SetDataCouroutineAchievementCheck());
        }
        catch
        {
            Debug.Log("SetDataToAPIAchievementCheck API Issue");
        }
       
    }

    public void SetDataToAPIAchievementUpdate()
    {
        try
        {
            StartCoroutine(SetDataCouroutineAchievementUpdate());
        }
        catch
        {
            Debug.Log("SetDataToAPIAchievementUpdate API Issue");
        }
       
    }

    IEnumerator SetDataCouroutineAchievementUpdate()
    {
        LoaderObject.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("user_id", USerId);

        using (UnityWebRequest www = UnityWebRequest.Post(AchievementUpdate, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
               
                Debug.Log("Failed To check Achievement Update");
                LoaderObject.SetActive(false);
            }
            else
            {
                noInternet = false;
                json = www.downloadHandler.text;
                Debug.Log("JsonData Achievement Update: " + json);
                LoaderObject.SetActive(false);

                Debug.Log("Success Achievement Update");
            }
        }
    }

    IEnumerator SetDataCouroutineAchievementCheck()
    {
        LoaderObject.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("user_id", USerId);
        
        using (UnityWebRequest www = UnityWebRequest.Post(AchievementCheck, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
               
                Debug.Log("Failed To check Achievement unlock");
                LoaderObject.SetActive(false);
            }
            else
            {
                noInternet = false;
                json = www.downloadHandler.text;
                CheckAchievementUnlockedJson(json);
                Debug.Log("JsonData Achievement Check: " + json);
                //LoaderObject.SetActive(false);
               
                Debug.Log("Success Achievement Check");
            }
        }
    }
    IEnumerator SetDataCouroutineSupport(string subject, string message)
    {
        LoaderObject.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("user_id", USerId);
        form.AddField("subject", subject);
        form.AddField("message", message);

        using (UnityWebRequest www = UnityWebRequest.Post(SupportAPI, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                SupportMessageScript.Instance.MessageSend.SetActive(true);
                SupportMessageScript.Instance.MessageObjectText.text = "Failed To Send Message";
                Debug.Log("Failed To Send Support Message");
                LoaderObject.SetActive(false);
            }
            else
            {
                noInternet = false;
                json = www.downloadHandler.text;
                Debug.Log("JsonData Support: " + json);
                LoaderObject.SetActive(false);
                SupportMessageScript.Instance.MessageSend.SetActive(true);
                SupportMessageScript.Instance.MessageObjectText.text = "Support Message Send";
                SupportMessageScript.Instance.resetTexts();
                Debug.Log("Success Support Message Send");
            }
        }
    }

    IEnumerator SetDataCouroutineItemSkinUse(int item_id, int quantity)
    {
        LoaderObject.SetActive(true);
        WWWForm form = new WWWForm();
        // Debug.Log(form.data);
        form.AddField("item_id", item_id);
        form.AddField("quantity", 1);
        FullSkinUseApi = SkinApi + USerId + SkinApiEnd;
        using (UnityWebRequest www = UnityWebRequest.Post(FullSkinUseApi, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                Debug.Log("Failed Item Skin Use ");
                LoaderObject.SetActive(false);
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Item Skin Use " + json);
                ItemSelected.SetActive(true);
                LoaderObject.SetActive(false);
                SetDataTOAPIMyItems();
                Debug.Log("Success Item Skin Use Made");
            }
        }
    }
    IEnumerator SetDataCouroutineItemUse(int item_id, int quantity)
    {
        WWWForm form = new WWWForm();
        // Debug.Log(form.data);
        form.AddField("item_id", item_id);
        form.AddField("quantity", 1);
        FullItemUseApi = ItemUseAPI + USerId + ItemUseAPIEnd;
        using (UnityWebRequest www = UnityWebRequest.Post(FullItemUseApi, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                Debug.Log("Failed Item Use ");
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Item Use " + json);
                SetDataTOAPIMyItems();
                Debug.Log("Success Item Use Made");
            }
        }
    }
    IEnumerator SetDataCouroutineDebugPurchases(int item_id, int quantity)
    {
        LoaderObject.SetActive(true);
        WWWForm form = new WWWForm();
        // Debug.Log(form.data);
        form.AddField("item_id", item_id);
        form.AddField("quantity", quantity);
        FullPurchaseApi = PurchaseAPI + USerId + PurchaseAPIEnd;
        using (UnityWebRequest www = UnityWebRequest.Post(FullPurchaseApi, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                Debug.Log("Failed Purchase ");
                LoaderObject.SetActive(false);
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Purchase " + json);
               // PurchaseMade.SetActive(true);
                SetDataTOAPIMyItems();
                Debug.Log("Success Purchase Made");
            }
        }
    }
    IEnumerator SetDataCouroutinePurchase(int item_id, int quantity)
    {
        LoaderObject.SetActive(true);
        WWWForm form = new WWWForm();
       // Debug.Log(form.data);
        form.AddField("item_id", item_id);
        form.AddField("quantity", quantity);
        FullPurchaseApi = PurchaseAPI + USerId + PurchaseAPIEnd;
        using (UnityWebRequest www = UnityWebRequest.Post(FullPurchaseApi, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                Debug.Log("Failed Purchase ");
                LoaderObject.SetActive(false);
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Purchase " + json);
                PurchaseMade.SetActive(true);
                SetDataTOAPIMyItems();
                Debug.Log("Success Purchase Made");
            }
        }
    }


    IEnumerator SetDataCouroutinePurchaseChallenge(int item_id, int quantity)
    {
        LoaderObject.SetActive(true);
        WWWForm form = new WWWForm();
        // Debug.Log(form.data);
        form.AddField("item_id", item_id);
        form.AddField("quantity", quantity);
        FullPurchaseApi = PurchaseAPI + USerId + PurchaseAPIEnd;
        using (UnityWebRequest www = UnityWebRequest.Post(FullPurchaseApi, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                Debug.Log("Failed Purchase ");
                LoaderObject.SetActive(false);
            }
            else
            {

                noInternet = false;
                LoaderObject.SetActive(false);
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Purchase Challenge " + json);
                PurchaseMade.SetActive(false);
                SetDataTOAPIMyItems();
                Debug.Log("Success Purchase Made Challenge");
            }
        }
    }
    IEnumerator SetDataCouroutine(string Email, string Name, string Picture, string driver, string driver_Id)
    {
        WWWForm form = new WWWForm();
       // Debug.Log(form.data);
        form.AddField("email", Email);
        form.AddField("name", Name);
        form.AddField("picture", Picture);
        form.AddField("driver", driver);
        form.AddField("driver_id", driver_Id);
        int leftParenthesis = Email.IndexOf("@");
        string UserName = Email.Substring(0, leftParenthesis);
        form.AddField("username", UserName);
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                LoaderObject.SetActive(false);
                if(www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("Failed Login due to internet");
                    LoginFailText.text = DefaultLoginFailText;
                    LoginFail.gameObject.SetActive(true);
                }
                if (www.result == UnityWebRequest.Result.ProtocolError)
                {
                    LogOutUSer();
                    LoginFail.gameObject.SetActive(true);
                    //switch(login_type)
                    //{
                    //    case LoginType.google:
                    //        {
                    //            signinsamplescript.OnSignOut();
                    //            break;
                    //        }
                    //    case LoginType.facebook:
                    //        {
                    //            facebookscript.FaceBookLogOut();
                    //            break;
                    //        }
                    //    case LoginType.apple:
                    //        {
                    //            PlayerPrefsHandler.AutoAppleLoginIn = 0;
                    //            break;
                    //        }
                    //}
                    Debug.Log("Failed Login due to Api");
                    noInternet = true;
                    json = www.downloadHandler.text;
                    Debug.Log("JsonData Login Fail" + json);
                    Debug.Log("Failed Login");
                    GetDataFromJsonOnLoginFail(json);
                }
                
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Login Success" + json);
                SetActiveLoader();
               
                Invoke(nameof(SetDeActiveLoader), 2f);
                facebookscript. PanelToEnable.SetActive(true);
                facebookscript. PanelToDisable.SetActive(false);
                GetDataFromJson(json);

                if (UseDebug)
                {
                    if(PlayerPrefsHandler.DebugIAP==0)
                    {
                        PlayerPrefsHandler.DebugIAP = 1;
                        Invoke(nameof(AddJokerAndShuffles), 2f);
                    }
                    
                }
                Debug.Log("Success");
            }
        }
    }

    public void ForTesting()
    {
        Invoke(nameof(SetDeActiveLoader), 2f);
        facebookscript.PanelToEnable.SetActive(true);
        facebookscript.PanelToDisable.SetActive(false);
    }
    private void SetDeActiveLoader()
    {
        LoaderObject.SetActive(false);
    }
    private void SetActiveLoader()
    {
        LoaderObject.SetActive(true);
    }

    public void SetDataToAPIImageUrl(string UrlNew)
    {
        StartCoroutine(SetDataCouroutineForUrl(UrlNew));
    }

    public void SetDataToAPITermsAndConditions()
    {
        LoaderObject.SetActive(true);
        StartCoroutine(SetDataCouroutineForTermsAndConditions(TermsAndConditonsApi));
    }


    public void SetDataTOAPIStore()
    {
        StartCoroutine(SetDataCouroutineForStore());
    }
    public void SetDataTOAPIMyItems()
    {
        StartCoroutine(SetDataCouroutineForMyItems());
    }

    public void SetDataTOAPIMyAchievements()
    {
        StartCoroutine(SetDataCouroutineForMyAchivements());
    }



    public void SetDataTOAPICurrentChallenge()
    {
        LoaderObject.SetActive(true);
        StartCoroutine(SetDataCouroutineCurrentChallenge());
    }

    IEnumerator SetDataCouroutineForMyAchivements()
    {
        WWWForm form = new WWWForm();
        FullMyAchievementsUseApi = AchievementAPI + USerId + AchievementAPIEnd;
        using (UnityWebRequest www = UnityWebRequest.Get(FullMyAchievementsUseApi))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                LoaderObject.SetActive(false);
                Debug.Log("Failed Achievements ");
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Achievements " + json);
                //store_data.JsonDataStore = json;

                myAchievements.GetDataFromJSonAchievement(json);
                //GetDataFromJSonMyItems(json);
                //GetDataFromJsonURL(json);
                SetDataToAPIAchievementCheck();
                Debug.Log("Achievement Success");
            }
        }
    }


    IEnumerator SetDataCouroutineCurrentChallenge()
    {
        WWWForm form = new WWWForm();
        FullMyChallengeUseApi = ChallengeAPI + USerId + ChallengeAPIEnd;
        using (UnityWebRequest www = UnityWebRequest.Get(FullMyChallengeUseApi))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                LoaderObject.SetActive(false);
                MainMenuController.Instance.TodaysChallenge.SetActive(true);
                MainMenuController.Instance.TodaysChallengeTitle.text = "No Challenge Today";
                Debug.Log("Failed To Get Current Challenge ");
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Current Challenge " + json);
                //store_data.JsonDataStore = json;

                MainMenuController.Instance.GetDataFromJSonChallenge(json);
                //GetDataFromJSonMyItems(json);
                //GetDataFromJsonURL(json);
                LoaderObject.SetActive(false);
                Debug.Log("Current Challenge Success");
            }
        }
    }





    public void SetDataTOAPIMyStats(string gametype)
    {
        StartCoroutine(SetDataCouroutineForMyStats(gametype));
    }


   

    IEnumerator SetDataCouroutineForMyStats(string GameType)
    {
        LoaderObject.SetActive(true);
        WWWForm form = new WWWForm();

        FullMyStatsUseApi = StatsApi + USerId + StatApiEnd + GameType;
        using (UnityWebRequest www = UnityWebRequest.Get(FullMyStatsUseApi))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                Debug.Log("Failed Stats Retrive ");
                LoaderObject.SetActive(false);
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Stats " + json);
                //store_data.JsonDataStore = json;

                if(GameType== "FreePlay")
                {
                    mystats.GetDataFromJson(json , true);
                    Debug.Log("Stats Success");
                }
                if (GameType == "Timed")
                {
                    mystats.GetDataFromJson(json, false);
                    Debug.Log("Stats Success");
                }

                if (GameType == " Challenge")
                {
                    GetDataFromChallenge(json);
                    Debug.Log("Challenge Stats Success");
                    // mystats.GetDataFromJson(json, false);
                }

                Debug.Log("Overall Stats Success");

                LoaderObject.SetActive(false);
            }
        }
    }
    IEnumerator SetDataCouroutineForMyItems()
    {
        WWWForm form = new WWWForm();

        FullMyItemsApi = UserItemID + USerId + UserItemIDEnd;
        using (UnityWebRequest www = UnityWebRequest.Get(FullMyItemsApi))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                Debug.Log("Failed ");
                LoaderObject.SetActive(false);
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData MyItems " + json);
                //store_data.JsonDataStore = json;
                LoaderObject.SetActive(false);
                MyItemsData.GetDataFromJSonUserItems(json);
                //GetDataFromJSonMyItems(json);
                //GetDataFromJsonURL(json);

                store_data.CheckIfSkinPurchased();
                Debug.Log("MyItems Success");
            }
        }
    }
    public void LoadImagesToStoreOnly()
    {
        StartCoroutine(SetDataCouroutineForStoreOnce());
    }
    IEnumerator SetDataCouroutineForStoreOnce()
    {
        WWWForm form = new WWWForm();
        using (UnityWebRequest www = UnityWebRequest.Get(StoreAPI))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                Debug.Log("Failed ");
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Store " + json);
                store_data.JsonDataStore = json;
                store_data.LoadImagesOnce(json);
                //GetDataFromJsonURL(json);

                Debug.Log("Success Store");
            }
        }
    }
    IEnumerator SetDataCouroutineForStore()
    {
        WWWForm form = new WWWForm();


        using (UnityWebRequest www = UnityWebRequest.Get(StoreAPI))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                Debug.Log("Failed ");
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Store " + json);
                store_data.JsonDataStore = json;
                store_data.GetDataFromJSonStore(json);
                //GetDataFromJsonURL(json);

                Debug.Log("Success Store");
            }
        }
    }
    IEnumerator SetDataCouroutineForUrl(string UrlNew)
    {
        WWWForm form = new WWWForm();


        using (UnityWebRequest www = UnityWebRequest.Get(UrlNew))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;
                Debug.Log("Failed Login ");
               
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData Facebook URL " + json);
                GetDataFromJsonURL(json);

                Debug.Log("Success URl");
            }
        }
    }

    IEnumerator SetDataCouroutineForTermsAndConditions(string UrlNew)
    {
        WWWForm form = new WWWForm();


        using (UnityWebRequest www = UnityWebRequest.Get(UrlNew))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                LoaderObject.SetActive(false);
                noInternet = true;
                Debug.Log("Failed ");
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData " + json);
                GetDataFromJsonTermsAndConditons(json);

                Debug.Log("Success Terms And Conditons");
            }
        }
    }

    public void GetDataShareLink()
    {
        StartCoroutine(SetDataCouroutineForShareLink());
    }

    IEnumerator SetDataCouroutineForShareLink()
    {
        WWWForm form = new WWWForm();


        using (UnityWebRequest www = UnityWebRequest.Get(ShareLinkAPI))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                //LoaderObject.SetActive(false);
                noInternet = true;
                Debug.Log("Failed  To Get Share Link");
            }
            else
            {

                noInternet = false;
                // Show results as text
                json = www.downloadHandler.text;
                Debug.Log("JsonData ShareLink" + json);
                GetDataFromShareLink(json);

                Debug.Log("Success Share Link Data");
            }
        }
    }
    IEnumerator SetDataCouroutineLevelWinFailAPi(string Game_Type, int won, int lost, string date, string time, int score, bool isChallenge = false, int challengeid = -1, int hardcoded = 0, int challenge_win_hardcoded = 0)
    {
        LoaderObject.SetActive(true);
        WWWForm form = new WWWForm();
        //Debug.Log(form.data);

        if(isChallenge)
        {
            form.AddField("game_type", Game_Type);
            form.AddField("won", won);
            form.AddField("lost", lost);
            form.AddField("date", date);
            form.AddField("time", time);
            form.AddField("score", score);
            form.AddField("challenge_id", challengeid);
            form.AddField("hardcoded", hardcoded);
            form.AddField("challenge_win_hardcoded", challenge_win_hardcoded);
        }
        else
        {
            form.AddField("game_type", Game_Type);
            form.AddField("won", won);
            form.AddField("lost", lost);
            form.AddField("date", date);
            form.AddField("time", time);
            form.AddField("score", score);
        }
       
        FullLevelWinFailApi = LevelWinFailApi + USerId + LevelWinFailApiConcatEnd;
        using (UnityWebRequest www = UnityWebRequest.Post(FullLevelWinFailApi, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("Failed due to ConnentionError ");
                }
                if (www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log("Failed due to ProtocolError ");
                }
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Failed due to Api  ");
                }
                noInternet = true;
                LoaderObject.SetActive(false);
                Debug.Log("Failed ");
            }
            else
            {
               // SetDataTOAPIMyStats(Game_Type);
                noInternet = false;
                // Show results as text
               
                json = www.downloadHandler.text;
                Debug.Log("Success Response : " + json);
                SetActiveAchievementUi(json);
                Debug.Log("Success stats");
                LoaderObject.SetActive(false);
            }
        }
    }



    public void DeleteUserData()
    {
        StartCoroutine(SetDataCouroutineDeleteAPI());
    }

    IEnumerator SetDataCouroutineDeleteAPI()
    {
        LoaderObject.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("user_id", USerId);

        using (UnityWebRequest www = UnityWebRequest.Post(DeleteAPI, form))
        {
            if (Header_Token != null)
            {
                www.SetRequestHeader("Authorization", Header_Token);
            }
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success) //if (www.isNetworkError || www.isHttpError)
            {
                noInternet = true;

                Debug.Log("Failed To Delete Customer Data");
                LoaderObject.SetActive(false);
                MainMenuController.Instance.SomeThingWentWrong.SetActive(true);
            }
            else
            {
                noInternet = false;
                json = www.downloadHandler.text;
                Debug.Log("Delete Json Data: " + json);
                LoaderObject.SetActive(false);
                MainMenuController.Instance.mainMenuPannel.SetActive(true);
                MainMenuController.Instance.Profile.SetActive(false);
                MainMenuController.Instance.FreePlayPannel.SetActive(false);
                MainMenuController.Instance.MainScreen.SetActive(false);
                if(login_type == LoginType.google)
                {
                    PlayGamesPlatform.Instance.SignOut();
                }
                if(login_type==LoginType.facebook)
                {
                    facebookscript.FaceBookLogOut();
                }

                login_type = LoginType.none;
                Debug.Log("Success Delete API");
            }
        }
    }

    public void SetActiveAchievementUi(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<GetMessageFromChallengeAPI>(jsonwithData);
        GetMessageFromChallengeAPI MyJsonClass = JsonConvert.DeserializeObject<GetMessageFromChallengeAPI>(jsonwithData);

        Debug.Log("Challenge Level WIN Message: " + MyJsonClass.message);
        if(MyJsonClass.message== "Achievement Unlocked")
        {
            Debug.Log("Success getting Achievement Completed");
            FreePlayGameLogic.Instance.LevelWinPanel.SetActive(false);
            FreePlayGameLogic.Instance.AchievementUnlocked.SetActive(true);


            if(MainMenuController.Instance.CurrentChallengeString== "Play 300 games to change the deck color to red or blue")
            {
                ApiCode.Instance.CurrenCardSkin = 4;
                ApiCode.Instance.store_data.CurrentSkinImageSet();
                ApiCode.Instance.SetDataToAPIItemSkinUse(4);
            }

        }
        Invoke(nameof(BackToMainMenu), 3f);
       
    }
    private void BackToMainMenu()
    {
        if(FreePlayGameLogic.Instance.LevelWinPanel.activeInHierarchy)
        {
            MainMenuController.Instance.BackToMainMenu();
            FreePlayGameLogic.Instance.AchievementUnlocked.SetActive(false);
        }
       
    }
    public class GetMessageFromChallengeAPI
    {
        public string message;
    }
    [Serializable]
    public class Data
    {
        public int id;
        public string url;
        public string terms_and_condition;
        public string about_us;
        public string faqs;
        public string name;
        public string image;
        public int price;
       
        public string currency;
     
        public int skin;
        public int achievement;
        public string ios_link;
        public string android_link;
        public string challenge_title;
        public string prize;
        public int item_id;
        public int quantity;



    }
    [Serializable]
    public class LoginData
    {
        public User user;
        public Achievement achievement;
        public string challenge_title;
        public string prize;
        public int item_id;
        public int quantity;
        public string token;

    }
    [Serializable]
    public class Picture
    {
        public Data data;
    }



    public class User
    {
        public int id;
        public string email;
        public string name;
        public int skin;
        public string picture;
    }

    public class Achievement
    {
        public string challenge_title;
        public string prize;
        public int item_id;
        public int quantity;
    }
    [Serializable]
    public class MyJSON
    {
        public string name;
        public Picture picture;
        public Data data;
        public string success;
        public string message;
    }

    public class JSONLogin
    {
        public string name;
        public Picture picture;
        public LoginData data;
        public string success;
        public string message;
    }
    [Serializable]
    public class NewJSON
    {
        public Data[] data;

    }

    [Serializable]
    public class AchievementUnlockCheck
    {
        public Data data;

    }

    [Serializable]
    public class GetShareLinkJson
    {
        public Data data;

    }
    private void GetDataFromJSonMyItems(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<NewJSON>(jsonwithData);
        NewJSON MyJsonClass = JsonConvert.DeserializeObject<NewJSON>(jsonwithData);
        //MyItemsData.IdFromServer.Clear();
        //for (int i = 0;i<MyJsonClass.data.Length;i++)
        //{
        //    MyItemsData.IdFromServer.Add(MyJsonClass.data[i].item_id);
        //}
        //USerId = DataFromJson.data.id.ToString();
        // Debug.Log("USerId " + USerId);
    }

    private void GetDataFromShareLink(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<GetShareLinkJson>(jsonwithData);
        GetShareLinkJson MyJsonClass = JsonConvert.DeserializeObject<GetShareLinkJson>(jsonwithData);

#if UNITY_ANDROID
        ShareLinkURL = MyJsonClass.data.android_link;

#elif UNITY_IOS
        ShareLinkURL = MyJsonClass.data.ios_link;
#endif

        //MyItemsData.IdFromServer.Clear();
        //for (int i = 0;i<MyJsonClass.data.Length;i++)
        //{
        //    MyItemsData.IdFromServer.Add(MyJsonClass.data[i].item_id);
        //}
        //USerId = DataFromJson.data.id.ToString();
        // Debug.Log("USerId " + USerId);
    }

    private void CheckAchievementUnlockedJson(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<AchievementUnlockCheck>(jsonwithData);
        AchievementUnlockCheck MyJsonClass = JsonConvert.DeserializeObject<AchievementUnlockCheck>(jsonwithData);

        if(MyJsonClass.data!=null)
        {
            if(MyJsonClass.data.achievement==1)
            {
                NewAchivementUnlocked.SetActive(true);
                SetDataToAPIAchievementUpdate();
                Invoke(nameof(SetDeActiveNewAchievement), 3f);
            }
            else
            {
                
                LoaderObject.SetActive(false);
            }
        }
    }

    private void SetDeActiveNewAchievement()
    {
        NewAchivementUnlocked.SetActive(false);
    }
    private void GetDataFromJSonStore(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<NewJSON>(jsonwithData);
        NewJSON MyJsonClass = JsonConvert.DeserializeObject<NewJSON>(jsonwithData);
        //USerId = DataFromJson.data.id.ToString();
       // Debug.Log("USerId " + USerId);
    }

    private void GetDataFromJsonOnLoginFail(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<MyJSON>(jsonwithData);
        MyJSON MyJsonClass = JsonConvert.DeserializeObject<MyJSON>(jsonwithData);

        Debug.Log("LoginFail Json :" + MyJsonClass);
        LoginFailText.text = MyJsonClass.message.ToString();
        // SetDataTOAPIStore();
       
    }


    private void GetDataFromChallenge(string ChallengeJson)
    {
        var DataFromJson = JsonUtility.FromJson<JSONLogin>(ChallengeJson);
        JSONLogin MyJsonClass = JsonConvert.DeserializeObject<JSONLogin>(ChallengeJson);

        if(!string.IsNullOrEmpty(MyJsonClass.data.challenge_title))
        {
            if(MyJsonClass.data.prize!=null)
            {
                SetDataCouroutinePurchaseChallenge(MyJsonClass.data.item_id , MyJsonClass.data.quantity);
            }
        }
    }
    private void GetDataFromJson(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<JSONLogin>(jsonwithData);
        JSONLogin MyJsonClass = JsonConvert.DeserializeObject<JSONLogin>(jsonwithData);
        USerId = MyJsonClass.data.user.id.ToString();
        CurrenCardSkin = MyJsonClass.data.user.skin;

        if (MyJsonClass.data.token != null)
        {
            //Header_Token = MyJsonClass.data.token;
            Header_Token = "Bearer " + MyJsonClass.data.token;
            // Debug.Log("Header Token " + Header_Token);
        }
        if (MyJsonClass.data.achievement!=null)
        {
            if(MyJsonClass.data.achievement.challenge_title== "Connect your Facebook account and receive a free reshuffle")
            {
                if(PlayerPrefsHandler.AutoLoginFacebook==1)
                {
                    
                    ChallengeCompletedOnlogin.SetActive(true);
                    ChallengeCompleteAchievementTextObject.SetActive(false);
                    Invoke(nameof(DeActiveChallengeOnLogin), 5f);
                    if (MyJsonClass.data.achievement.prize== "shuffle")
                    {
                        ChallengeCompleteAchievementTextObject.SetActive(true);
                        ChallengeCompleteAchievementTextObjectText.text = "Shuffle";
                        SetDataToAPIPurchaseOnChallenge(MyJsonClass.data.achievement.item_id, MyJsonClass.data.achievement.quantity);
                    }
                    if (MyJsonClass.data.achievement.prize == "joker")
                    {
                        ChallengeCompleteAchievementTextObject.SetActive(true);
                        ChallengeCompleteAchievementTextObjectText.text = "Joker";
                        SetDataToAPIPurchaseOnChallenge(MyJsonClass.data.achievement.item_id, MyJsonClass.data.achievement.quantity);
                    }
                }
            }
            Debug.Log("has achievement ");
        }
       // SetDataTOAPIStore();
        SetDataTOAPIMyItems();
        //  SetDataTOAPIMyStats(FreePlay);
        // SetDataTOAPIMyStats(Timed);
        //SetDataTOAPIMyAchievements();


        try
        {
            GetDataShareLink();
        }
        catch
        {
            Debug.Log("Share Link Issue");
        }

        try
        {
            LoadImagesToStoreOnly();
        }
        catch
        {
            Debug.Log("Store Data Issue");
        }


        try
        {
            SetDataToAPITermsAndConditions();
        }
        catch
        {
            Debug.Log("GuideLines Issue");
        }
        store_data.CurrentSkinImageSet();
        Debug.Log("USerId " + USerId);
        Debug.Log("Login Success ");
    }


    private void DeActiveChallengeOnLogin()
    {
        ChallengeCompletedOnlogin.SetActive(false);
    }

    public void GetDataFromJsonURL(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<MyJSON>(jsonwithData);
        MyJSON MyJsonClass = JsonConvert.DeserializeObject<MyJSON>(jsonwithData);
        UrlImage =  MyJsonClass.picture.data.url.ToString();
        Debug.Log("Image URL Facebook: " + UrlImage);
        UserNameFacebook = MyJsonClass.name.ToString();
        //EmailOfUser = "LoginFromFaceBook2";

        PlayerPrefsHandler.FaceBook_Email = EmailOfUser;
        PlayerPrefsHandler.FaceBook_Name = UserNameFacebook;
        PlayerPrefsHandler.FaceBook_ImageUrl = UrlImage;

        PlayerPrefsHandler.AutoLoginFacebook = 1;
        SetDataToAPI(EmailOfUser, UserNameFacebook, UrlImage, "facebook", PlayerPrefsHandler.FaceBook_ID);
        Debug.Log("Succes URl Json and API Hit");
        //USerId = DataFromJson.data.id.ToString();
        //Debug.Log("USerId " + USerId);
    }

    public void GetDataFromJsonTermsAndConditons(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<MyJSON>(jsonwithData);
        MyJSON MyJsonClass = JsonConvert.DeserializeObject<MyJSON>(jsonwithData);


        //TermsAndConditionsText.text = MyJsonClass.data.terms_and_condition.ToString();
        //AboutUsText.text = MyJsonClass.data.about_us.ToString();
        //FaqText.text = MyJsonClass.data.faqs.ToString();

        TermsAndConditionsTextPro.text = MyJsonClass.data.terms_and_condition.ToString();
        AboutUsTextPro.text = MyJsonClass.data.about_us.ToString();
        FaqTextPro.text = MyJsonClass.data.faqs.ToString();

        LoaderObject.SetActive(false);
        Debug.Log("Succes Terms and Conditions Json");
       
    }

}
