using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FacebookScript : MonoBehaviour
{
   // public GameObject Panel_Add;
   public Text FB_userName;
   public Image FB_useerDp;
    public GameObject PanelToEnable;
    public GameObject PanelToDisable;

    private bool CalledInit;
    private void Awake()
    {
       
        CalledInit = false;
        FB.Init(SetInit, onHidenUnity);
        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            CalledInit = true;
          
        }
           
       // Panel_Add.SetActive(false);
    }

    public void checkSignIn()
    {
        FB.Init(SetInit, onHidenUnity);
        if (!CalledInit)
        {
            
        }
       
    }


    public void FaceBookAutoLogin()
    {
        checkSignIn();
        //ApiCode.Instance.SetDataToAPI()

        // ApiCode.Instance. SetDataToAPI(PlayerPrefsHandler.FaceBook_Email, PlayerPrefsHandler.FaceBook_Name, PlayerPrefsHandler.FaceBook_ImageUrl, "facebook", ApiCode.Instance.FaceBookid);
    }
    void SetInit()
    {
        if (!(Application.internetReachability == NetworkReachability.NotReachable))
        {
            if (FB.IsLoggedIn)
            {
                //Debug.Log("Facebook is Login!");
               
            }
            else
            {
                //Debug.Log("Facebook is not Logged in!");
            }
           // DealWithFbMenus(FB.IsLoggedIn);
        }
           
    }

    void onHidenUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void FBLogin()
    {
        MainMenuController.Instance.ButtonClickSound();
        //FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends","user_link" }, AuthCallBack);

       
        List<string> permissions = new List<string>();
        permissions.Add("email");
        permissions.Add("public_profile");
        permissions.Add("gaming_profile");
        
        //permissions.Add("user_link");

        //FB.LogInWithPublishPermissions(permissions, AuthCallBack);
        //var perms = new List<string>() { "public_profile", "email" };
        //FB.Mobile.LoginWithTrackingPreference(LoginTracking.LIMITED, perms, "nonce123", OnLoginResult);



        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }
    private static void OnLoginResult(ILoginResult result)
    {
        var profile = FB.Mobile.CurrentProfile();
        if (profile != null)
        {
            ApiCode.Instance.EmailOfUser = profile.Email;
            Debug.Log("Email " + profile.Email);
            Debug.Log("ImageUrl " + profile.ImageURL);
        }
        Debug.Log("FB OnLoginResult");
    }

    // Start is called before the first frame update
    void AuthCallBack(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
                if (FB.IsLoggedIn)
                {
                
                var aToken = AccessToken.CurrentAccessToken;
                //// Print current access token's User ID
                //Debug.Log("UserID " + aToken.UserId);
                //// Print current access token's granted permissions
                foreach (string perm in aToken.Permissions)
                {
                    Debug.Log("Permissions " + perm);
                }
                //Debug.Log("Facebook is Login!");
                // Panel_Add.SetActive(true);
            }
            else
            {
                FaceBookLogOut();
                //Debug.Log("Facebook is not Logged in!");
            }
           
            
            DealWithFbMenus(FB.IsLoggedIn);
           
        }
    }

    void DealWithFbMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
           
            Debug.Log("Facebook Sdk Loged In");
            ApiCode.Instance.LoaderObject.SetActive(true);


            // https://graph.facebook.com/me?access_token=MY_CORRECT_TOKEN&fields=id,name,email


            // "https://graph.facebook.com/" + AccessToken.CurrentAccessToken + "?fields=first_name,last_name,email"


            
            
                 
                 



            FB.API("/" + AccessToken.CurrentAccessToken.UserId + "?fields=first_name", HttpMethod.GET,DisplayUsername);
            FB.API("/" + AccessToken.CurrentAccessToken.UserId + "/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
            FB.API("/" + AccessToken.CurrentAccessToken.UserId + "?fields=first_name,last_name,email", HttpMethod.GET, FetchProfileCallback, new Dictionary<string, string>() { });


            //FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            //FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
            //FB.API("/me?fields=first_name,last_name,email", HttpMethod.GET, FetchProfileCallback, new Dictionary<string, string>() { });

            //FB.API("/me?fields=user_link", HttpMethod.GET, FetchProfileCallback);
            //FB.API("/me?fields=first_name,last_name", HttpMethod.GET, FetchProfileCallback);

            Debug.Log("URL Json " + "https://graph.facebook.com/" + AccessToken.CurrentAccessToken.UserId + "?fields=id,name,email,picture&" + "access_token=" + AccessToken.CurrentAccessToken.TokenString);
            
          
            
            //PanelToEnable.SetActive(true);
            //PanelToDisable.SetActive(false);
            ApiCode.Instance.login_type = ApiCode.LoginType.facebook;

        }
        else
        {
            Debug.Log("Facebook Sdk not Loged In");
        }

        var profile = FB.Mobile.CurrentProfile();
        if (profile != null)
        {
            ApiCode.Instance.EmailOfUser = profile.Email;
            Debug.Log("Email " + profile.Email);
            Debug.Log("ImageUrl " + profile.ImageURL);
        }
    }

    public void FaceBookLogOut()
    {
        PlayerPrefsHandler.AutoLoginFacebook = 0;
        FB.LogOut();
        ApiCode.Instance.LoaderObject.SetActive(false);
    }

    private void FetchProfileCallback(IGraphResult result)
    {
        //result.
        //Debug.Log("RawResult " + result.RawResult);

        //IDictionary data = result.ResultDictionary["data"] as IDictionary; //create a new data dictionary
        //photoURL = data["url"].ToString();
        // Debug.Log("Profile: first name: " + result.ResultDictionary["first_name"]);
        //Debug.Log("Profile: last name: " + result.ResultDictionary["last_name"]);
        //Debug.Log("Profile: id: " + result.ResultDictionary["id"]);
        //Debug.Log("Profile: ImageURL: " + result.ResultDictionary["user_link"]);
        //Debug.Log("Profile: email: " + result.ResultDictionary["email"]);
        //Debug.Log("Profile: User: " + result.ResultDictionary["picture"]);
        //Debug.Log("Profile: URL: " + result.ResultDictionary["url"]);
        //ApiCode.Instance.USerId = result.ResultDictionary["id"].ToString();

        //ApiCode.Instance.SetDataToAPIImageUrl("https://graph.facebook.com/" + AccessToken.CurrentAccessToken.UserId + "?fields=id,name,email,picture&" + "access_token=" + AccessToken.CurrentAccessToken.TokenString);
        // Debug.LogError("Url " + "https://graph.facebook.com/" + AccessToken.CurrentAccessToken.UserId +"?" + "access_token=" + AccessToken.CurrentAccessToken.TokenString);
        // Debug.LogError("Url2 " + "https://graph.facebook.com/" + AccessToken.CurrentAccessToken.UserId  + "?fields=id,name,email,picture&" + "access_token=" + AccessToken.CurrentAccessToken.TokenString);
        //"https://graph.facebook.com/USER-ID?fields=id,name,email,picture&access_token=ACCESS-TOKEN"
        //ApiCode.Instance.SetDataToAPI(result.ResultDictionary["email"].ToString(), result.ResultDictionary["first_name"].ToString(), result.ResultDictionary["id"] + "/me/picture?type=square&height=128&width=128", "facebook", "facebook");
        //ApiCode.Instance.SetDataToAPI("LoginFromFaceBook", result.ResultDictionary["first_name"].ToString(),ApiCode.Instance.UrlImage, "facebook", "facebook");
        // ApiCode.Instance.SetDataToAPI("CurrentlyNotAvailable.com", result.ResultDictionary["first_name"].ToString(), "https" + "://graph.facebook.com/" + "/me/picture?type=square&height=128&width=128", "facebook", "facebook");
        //Debug.Log("Profile: emailNew: " + result.ResultDictionary["email"]);

       

        if (result.ResultDictionary.ContainsKey("email"))
        {
            ApiCode.Instance.EmailOfUser = result.ResultDictionary["email"].ToString();
            ApiCode.Instance.FaceBookid = result.ResultDictionary["id"].ToString();
            PlayerPrefsHandler.FaceBook_ID = result.ResultDictionary["id"].ToString();
            Debug.Log("Profile: email: " + result.ResultDictionary["email"]);
            ApiCode.Instance.SetDataToAPIImageUrl("https://graph.facebook.com/" + AccessToken.CurrentAccessToken.UserId + "?fields=id,name,email,picture&" + "access_token=" + AccessToken.CurrentAccessToken.TokenString);

        }
        else
        {

            ApiCode.Instance.NoInternetPanel.SetActive(true);
            ApiCode.Instance.PanelText.text = ApiCode.Instance.FacebookEmailError;
            FaceBookLogOut();
            ApiCode.Instance.NoEmailFound.SetActive(true);
            Debug.Log("No Email Found Logging Out");
        }
        foreach (KeyValuePair<string, object> kvp in result.ResultDictionary)
        {
            Debug.Log("Key " + kvp.Key + " Value " + kvp.Value);
            //Debug.Log("Key = {0} + Value = {1}" + kvp.Key + kvp.Value);
        }

        //Debug.Log("Profile: id: " + result.ResultDictionary["id"]);
        //ApiCode.Instance.EmailOfUser = result.ResultDictionary["id"].ToString();
       

    }
    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            string name = ""+result.ResultDictionary["first_name"];
            FB_userName.text = name;
            
            Debug.Log(""+name);
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
    string photoURL;
    void DisplayProfilePic(IGraphResult result)
    {
        
        if (result.Texture != null)
        {
            //result.ResultDictionary[]
            Debug.Log("Profile Pic");
            FB_useerDp.sprite = Sprite.Create(result.Texture,new Rect(0,0, result.Texture.width, result.Texture.height),new Vector2());
        }
        else
        {
            Debug.Log(result.Error);
        }
    }


}
