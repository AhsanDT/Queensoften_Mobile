// <copyright file="SigninSampleScript.cs" company="Google Inc.">
// Copyright (C) 2017 Google Inc. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations

namespace SignInSample {
  using System;
    using System.Collections;
    using System.Collections.Generic;
  using System.Threading.Tasks;
  using Google;
  using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;
    using Firebase.Extensions;
    using GooglePlayGames;
    using GooglePlayGames.BasicApi;
    using UnityEngine.SocialPlatforms;

    public class SigninSampleScript : MonoBehaviour {

        //public Text statusText;
        //public Text DebugImage;
    public Text UserName;
        public Text StatusText;
    public Image ProfileImage;
    public string webClientId = "<your client id here>";
        public GameObject PanelToEnable;
        public GameObject PanelToDisable;
    private GoogleSignInConfiguration configuration;

    // Defer the configuration creation until Awake so the web Client ID
    // Can be set via the property inspector in the Editor.

    Uri profilepic;
    void Awake() {
          
      configuration = new GoogleSignInConfiguration {
            WebClientId = webClientId,
            RequestIdToken = true
      };
    }

        private void OnEnable()
        {
            if (PlayerPrefsHandler.AutoLogin == 1)
            {
                if (!(Application.internetReachability == NetworkReachability.NotReachable))
                {

                   // AutoLoginGmail();
                }
            }
        }


        public void NewGoogleSignIn()
        {
            MainMenuController.Instance.ButtonClickSound();


            // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            //PlayGamesPlatform.InitializeInstance(config);
            //PlayGamesPlatform.Activate(); ;



            ApiCode.Instance.LoaderObject.SetActive(true);

            PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            //.RequestServerAuthCode(true)
            .RequestEmail()
            .Build());


            PlayGamesPlatform.Activate();

            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("Sign-in successful Google Play Services !");

                   
                    UserName.text = Social.localUser.userName;

                    string imageUrl = ((PlayGamesLocalUser)Social.localUser).AvatarURL;

                    // ApiCode.Instance.ForTesting();
                    string email = ((PlayGamesLocalUser)Social.localUser).Email;

                    //StartCoroutine(GetProfileImage(Social.localUser.av));
                    //ApiCode.Instance.login_type = ApiCode.LoginType.google;
                    //  string Email = Social.localUser.userName + "@gmail.com";
                    ApiCode.Instance.login_type = ApiCode.LoginType.google;
                    Debug.Log("G Email : " + email);
                    Debug.Log("UserName " + Social.localUser.userName);
                    Debug.Log("URL : " + ((PlayGamesLocalUser)Social.localUser).AvatarURL);
                    ApiCode.Instance.SetDataToAPI(email, Social.localUser.userName, ((PlayGamesLocalUser)Social.localUser).AvatarURL, "google", "google");

                    StartCoroutine(LoadProfileImage(imageUrl));

                }
                else
                {
                    ApiCode.Instance.LoaderObject.SetActive(false);
                    Debug.Log("Sign-in failed  Google Play Services.");
                }
            });
        }
        private IEnumerator LoadProfileImage(string url)
        {
            var www = new WWW(url);
            yield return www;
            ProfileImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
        }


        public void OnSignIn() {

            MainMenuController.Instance.ButtonClickSound();
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
             GoogleSignIn.Configuration.RequestEmail = true;
            //AddStatusText("Calling SignIn");

            GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(
           OnAuthenticationFinished);

          //  GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished, TaskScheduler.FromCurrentSynchronizationContext());
            //GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(task =>
            //{
            //    if (task.IsCanceled)
            //    {
            //        StatusText.text = "Please Try Again!";
            //        //Debug.Log("Please Try Again!");
            //    }
            //    else if (task.IsFaulted)
            //    {
            //        //Debug.LogError(task.Exception?.Message);
            //        StatusText.text = "Error";
            //    }

            //    else
            //    {
            //        var user = task.Result;
            //        StatusText.text = "Success Email " + user.Email;
            //        //Debug.Log(user.Email);
            //    }
            //});
            // GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
            //OnAuthenticationFinished);
        }

    public void OnSignOut() {
            //AddStatusText("Calling SignOut");
            PlayerPrefsHandler.AutoLogin = 0;
            GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect() {
      //AddStatusText("Calling Disconnect");
      GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task) {
            //Debug.LogError("Authe");
      if (task.IsFaulted) {
                StatusText.text = "Task ISFaulted";
                PlayerPrefsHandler.AutoLogin = 0;
                using (IEnumerator<System.Exception> enumerator =
                task.Exception.InnerExceptions.GetEnumerator())
                {
          if (enumerator.MoveNext()) {
            GoogleSignIn.SignInException error =
                    (GoogleSignIn.SignInException)enumerator.Current;
                        StatusText.text = "Got Error: " + error.Status + " " + error.Message;
            //AddStatusText("Got Error: " + error.Status + " " + error.Message);
          } else {
                        PlayerPrefsHandler.AutoLogin = 0;
                       
                        StatusText.text = "Got Unexpected Exception?!?" + task.Exception;
            //AddStatusText("Got Unexpected Exception?!?" + task.Exception);
          }
        }
      } else if(task.IsCanceled) {
                //Debug.LogError("Task Canceled");
                StatusText.text = "Canceled";
                //AddStatusText("Canceled");
            } else  {
                StatusText.text = "Success";

                ApiCode.Instance.LoaderObject.SetActive(true);
                UserName.text = task.Result.DisplayName;
              
                StartCoroutine(GetProfileImage(task.Result.ImageUrl));
                ApiCode.Instance.login_type = ApiCode.LoginType.google;
                ApiCode.Instance.SetDataToAPI(task.Result.Email, task.Result.DisplayName, task.Result.ImageUrl.ToString(), "google", "google");

                // StartCoroutine(onLoginSuccess(task));

            }
        }



        IEnumerator onLoginSuccess(Task<GoogleSignInUser> task)
        {
            yield return new WaitForEndOfFrame();
            UserName.text = task.Result.DisplayName;

            Invoke(nameof(SetAutoLogin), 3f);
            StartCoroutine(GetProfileImage(task.Result.ImageUrl));
            ApiCode.Instance.login_type = ApiCode.LoginType.google;

            PlayerPrefsHandler.Google_Email = task.Result.Email;
            PlayerPrefsHandler.Google_Name = task.Result.DisplayName;
            PlayerPrefsHandler.Google_ImageUrl = task.Result.ImageUrl.ToString();

            ApiCode.Instance.SetDataToAPI(PlayerPrefsHandler.Google_Email, PlayerPrefsHandler.Google_Name, PlayerPrefsHandler.Google_ImageUrl, "google", "google");
        }

        private void SetAutoLogin()
        {
            PlayerPrefsHandler.AutoLogin = 1;
        }
        public void AutoLoginGmail()
        {
            OnSignIn();
            //ApiCode.Instance.SetDataToAPI(PlayerPrefsHandler.Google_Email, PlayerPrefsHandler.Google_Name, PlayerPrefsHandler.Google_ImageUrl, "google", "google");
        }
        IEnumerator GetProfileImage(Uri profileImage)
        {
            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(profileImage);
           // UnityWebRequest uwr = new UnityWebRequest(profileImage);
            yield return uwr.SendWebRequest();
            Texture2D tex = new Texture2D(264, 264);
            tex = ((DownloadHandlerTexture)uwr.downloadHandler).texture;
            Sprite image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            ProfileImage.sprite = image;
           // PlayerPrefsHandler.AutoLogin = 1;
        }

    public void OnSignInSilently() {
      GoogleSignIn.Configuration = configuration;
      GoogleSignIn.Configuration.UseGameSignIn = false;
      GoogleSignIn.Configuration.RequestIdToken = true;
      GoogleSignIn.Configuration.RequestEmail = true;
            //AddStatusText("Calling SignIn Silently");

            GoogleSignIn.DefaultInstance.SignInSilently()
            .ContinueWith(OnAuthenticationFinished);
    }


    public void OnGamesSignIn() {
      GoogleSignIn.Configuration = configuration;
      GoogleSignIn.Configuration.UseGameSignIn = true;
      GoogleSignIn.Configuration.RequestIdToken = false;

      //AddStatusText("Calling Games SignIn");

      GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
        OnAuthenticationFinished);
    }

    private List<string> messages = new List<string>();
    //void AddStatusText(string text) {
    //  if (messages.Count == 5) {
    //    messages.RemoveAt(0);
    //  }
    //  messages.Add(text);
    //  string txt = "";
    //  foreach (string s in messages) {
    //    txt += "\n" + s;
    //  }
    //  statusText.text = txt;
    //}
  }
}
