using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameController>();
            }
            return instance;
        }
    }
    // Start is called before the first frame update
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

        int modeSelected = PlayerPrefsHandler.SelectedEnvironment;
        
        int levelSelected = PlayerPrefsHandler.SelectedLevelWRTEnv;

        switch (modeSelected)
        {

            case 0:
                GameObjectsContainer.Instance.currentLevelControllerGameObject = Instantiate(GameObjectsContainer.Instance.prefabsContainer.ENV_1_levels[levelSelected]);
                break;
            case 1:
                GameObjectsContainer.Instance.currentLevelControllerGameObject = Instantiate(GameObjectsContainer.Instance.prefabsContainer.ENV_1_levels[levelSelected]);
                break;
        }
        if(!GameObjectsContainer.Instance.currentLevelController_Script.UseLightBakedEnv)
        {
            UiObjectsContainer.Instance.LightBakedEnv.SetActive(false);
            Instantiate(GameObjectsContainer.Instance.prefabsContainer.environmentPrefabs[modeSelected]);
        }
        else
        {
            UiObjectsContainer.Instance.LightBakedEnv.SetActive(true);
        }

    }


    public void MainMenuButtonPressed()
    {
        Time.timeScale = 1f;
        UiObjectsContainer.Instance.loadingPanel.SetActive(true);
        UiObjectsContainer.Instance.loadingPanel.GetComponent<SceneLoader>().LoadScene();
    }

    public void PauseButtonPressed()
    {
        Time.timeScale = 0.0001f;
        GameStateHandler.Instance.isGameInPauseState = true;
        UiObjectsContainer.Instance.pausePanel.SetActive(true);
        UiObjectsContainer.Instance.playerControlsPanel.SetActive(false);

    }
    public void ResumeButtonPressed()
    {
        Time.timeScale = 1f;
        GameStateHandler.Instance.isGameInPauseState = false;
        UiObjectsContainer.Instance.pausePanel.SetActive(false);
        UiObjectsContainer.Instance.playerControlsPanel.SetActive(true);

    }

    public void RestartButtonPressed()
    {
        Time.timeScale = 1f;
        UiObjectsContainer.Instance.loadingPanel.SetActive(true);
        UiObjectsContainer.Instance.loadingPanel.GetComponent<SceneLoader>().sceneName_s = SceneManager.GetActiveScene().name;
        UiObjectsContainer.Instance.loadingPanel.GetComponent<SceneLoader>().LoadScene();
    }

    public void NextButtonPressed()
    {
        PlayerPrefsHandler.SelectedLevelWRTEnv++;
        switch (PlayerPrefsHandler.SelectedEnvironment)
        {
            case 0:
                if (PlayerPrefsHandler.SelectedLevelWRTEnv > (GameObjectsContainer.Instance.prefabsContainer.ENV_1_levels.Length -1))
                {
                    PlayerPrefsHandler.SelectedLevelWRTEnv = GameObjectsContainer.Instance.prefabsContainer.ENV_1_levels.Length - 1;
                }
                break;
            case 1:
                if (PlayerPrefsHandler.SelectedLevelWRTEnv > ( GameObjectsContainer.Instance.prefabsContainer.ENV_2_levels.Length -1))
                {
                    PlayerPrefsHandler.SelectedLevelWRTEnv = GameObjectsContainer.Instance.prefabsContainer.ENV_2_levels.Length - 1;
                }
                break;
        }
         

        Time.timeScale = 1f;
        UiObjectsContainer.Instance.loadingPanel.SetActive(true);
        UiObjectsContainer.Instance.loadingPanel.GetComponent<SceneLoader>().sceneName_s = SceneManager.GetActiveScene().name;
        UiObjectsContainer.Instance.loadingPanel.GetComponent<SceneLoader>().LoadScene();
    }
}
