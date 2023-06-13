using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject playerPositionToInstantiate;
    public int enemiesToKill;

    public int levelCompleteReward = 1000;
    public int enemyKillReward = 200;
    public bool UseLightBakedEnv;
    private void Awake()
    {
        int playerSelected = PlayerPrefsHandler.PlayerSelected;
        GameObjectsContainer.Instance.playerGameObject = Instantiate(GameObjectsContainer.Instance.prefabsContainer.playerPrefabs[playerSelected], playerPositionToInstantiate.transform.position, playerPositionToInstantiate.transform.rotation);

        GameStateHandler.Instance.enemiesToKills_Total = enemiesToKill;
        GameStateHandler.Instance.enemiesToKillRemaining = enemiesToKill;

    }

    public void EnemiesDeathCallBack()
    {
        if (CheckIfLevelWin())
        {
            LevelWin();
        }
    }

    public void PlayerDeathCallBack()
    {

        if (CheckIfLevelFailed())
        {
            LevelFail();
        }
    }


    bool CheckIfLevelWin()
    {
        if (GameStateHandler.Instance.enemiesToKillRemaining <= 0 && GameStateHandler.Instance.isGameFailed == false && GameStateHandler.Instance.isGameWin == false)
        {
            GameStateHandler.Instance.isGameWin = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    bool CheckIfLevelFailed()
    {
        if (GameStateHandler.Instance.isGameWin == false && GameStateHandler.Instance.isGameFailed == false)
        {
            GameStateHandler.Instance.isGameFailed = true;
            return true;
        }
        else
        {
            return false;
        }
    }



    public void LevelWin()
    {
        if (PlayerPrefsHandler.SelectedLevelWRTEnv == PlayerPrefsHandler.SelectedUnlockedLevelWRTEnv - 1)
        {
            PlayerPrefsHandler.SelectedUnlockedLevelWRTEnv++;
        }
        StartCoroutine(LevelWin_Coroutine());
    }
    IEnumerator LevelWin_Coroutine()
    {
        yield return new WaitForSeconds(Time.deltaTime);

        UiObjectsContainer.Instance.playerControlsPanel.SetActive(false);
        UiObjectsContainer.Instance.gamePlayHudPanel.SetActive(false);

        yield return new WaitForSeconds(1f);

        UiObjectsContainer.Instance.levelWinPanel.SetActive(true);


    }
    public void LevelFail()
    {
        StartCoroutine(LevelFailed_Coroutine());
    }
    IEnumerator LevelFailed_Coroutine()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        UiObjectsContainer.Instance.playerControlsPanel.SetActive(false);
        UiObjectsContainer.Instance.gamePlayHudPanel.SetActive(false);

        yield return new WaitForSeconds(1f);

        UiObjectsContainer.Instance.levelFailPanel.SetActive(true);
    }
}
