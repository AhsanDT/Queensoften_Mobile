using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AppleLogin : MonoBehaviour
{
    private IAppleAuthManager appleAuthManager;

    public string AppleUserIdKey { get; private set; }


    public string AppleUserEmail { get; private set; }

    public string AppleUserFullName { get; private set; }

    private static AppleLogin instance;
    public static AppleLogin Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AppleLogin>();
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


    void Start()
    {
#if UNITY_IOS
      // If the current platform is supported
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
            var deserializer = new PayloadDeserializer();
            // Creates an Apple Authentication manager with the deserializer
            this.appleAuthManager = new AppleAuthManager(deserializer);
        }
#endif


    }

    void Update()
    {

        // Updates the AppleAuthManager instance to execute
        // pending callbacks inside Unity's execution loop
#if UNITY_IOS
if (this.appleAuthManager != null)
        {
            this.appleAuthManager.Update();
        }
#endif


    }


    public void AppleLoginButton()
    {
        MainMenuController.Instance.ButtonClickSound();
        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

        this.appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
        // Obtained credential, cast it to IAppleIDCredential
        var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
            // Apple User ID
            // You should save the user ID somewhere in the device
            var userId = appleIdCredential.User;
                    

            // Email (Received ONLY in the first login)
            var email = appleIdCredential.Email;
                    
                    // Full name (Received ONLY in the first login)
                    var fullName = appleIdCredential.FullName;
                    

                    // Identity token
                    var identityToken = Encoding.UTF8.GetString(
                        appleIdCredential.IdentityToken,
                        0,
                        appleIdCredential.IdentityToken.Length);

            // Authorization code
            var authorizationCode = Encoding.UTF8.GetString(
                        appleIdCredential.AuthorizationCode,
                        0,
                        appleIdCredential.AuthorizationCode.Length);


                    if(!string.IsNullOrEmpty(email)  && !string.IsNullOrEmpty(fullName.ToString()))
                    {
                        PlayerPrefs.SetString(AppleUserIdKey, userId);
                        PlayerPrefs.SetString(AppleUserEmail, email);
                        PlayerPrefs.SetString(AppleUserFullName, fullName.ToString());
                        
                    }
                    ApiCode.Instance.SetDataToAPI(AppleUserEmail, AppleUserFullName, "https://www.pinterest.com/pin/apple-logo-white-and-black--855613629217803524/", "apple", "apple");
                    PlayerPrefsHandler.AutoAppleLoginIn = 1;
                    // And now you have all the information to create/login a user in your system
                }
            },
            error =>
            {
        // Something went wrong
        var authorizationErrorCode = error.GetAuthorizationErrorCode();
            });
    }


    public void AppleAutoLogin()
    {
        ApiCode.Instance.SetDataToAPI(AppleUserEmail, AppleUserFullName, "https://www.pinterest.com/pin/apple-logo-white-and-black--855613629217803524/", "apple", "apple");
    }
}
