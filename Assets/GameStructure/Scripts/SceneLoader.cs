using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneName_s;
    public Image loadingBar_I;
    public Text loadingValue_T;

    public bool executeInStart = false;

    // Start is called before the first frame update
    void Start()
    {
        if (executeInStart)
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }
    public void LoadScene(string sceneName)
    {
        sceneName_s = sceneName;
        StartCoroutine(LoadSceneCoroutine());
    }
    IEnumerator LoadAsyncScene()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName_s);
        loadingBar_I.fillAmount = asyncLoad.progress;
        loadingValue_T.text = "LOADING...     " + ((int)asyncLoad.progress * 100) + "";
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName_s);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            loadingValue_T.text = "LOADING... " + (asyncOperation.progress * 100) + "%";
            loadingBar_I.fillAmount = asyncOperation.progress;
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

}
