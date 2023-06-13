using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWinHandler : MonoBehaviour
{
    private int oneEnemiesKillReward;
    private int levelCompleteReward;
    public Text killsCountText;
    public Text killsRewardText;
    public Text levelCompleteRewardText;
    public Text totalRewardText;

    public GameObject[] buttonsToDisable;

    private void OnEnable()
    {
        oneEnemiesKillReward = GameObjectsContainer.Instance.currentLevelController_Script.enemyKillReward;
        levelCompleteReward = GameObjectsContainer.Instance.currentLevelController_Script.levelCompleteReward;
        killsCountText.text = 0 + "";
        killsRewardText.text = 0 + "";
        levelCompleteRewardText.text = 0 + "";
        totalRewardText.text = 0 + "";
        StartCoroutine(StartCounter());
    }

    IEnumerator StartCounter()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < buttonsToDisable.Length; i++)
        {
            buttonsToDisable[i].SetActive(false);
        }

        int enemiesKillCount = GameStateHandler.Instance.enemiesKilled;
        for (int i = 0; i < enemiesKillCount; i++)
        {
            killsCountText.text =  i + "";
            yield return new WaitForSeconds(Time.deltaTime * 2); 
        }
        killsCountText.text = enemiesKillCount + "";


        float enemiesKillReward = enemiesKillCount * oneEnemiesKillReward;
        for (float i = 0; i < enemiesKillReward; i+= enemiesKillReward /20)
        {
            killsRewardText.text = ((int)i) + "";
            yield return new WaitForSeconds(Time.deltaTime * 2);
        }
        killsRewardText.text = ((int)enemiesKillReward) + "";



        for (float i = 0; i < levelCompleteReward; i += levelCompleteReward / 20)
        {
            levelCompleteRewardText.text = ((int)i) + "";
            yield return new WaitForSeconds(Time.deltaTime * 2);
        }
        levelCompleteRewardText.text = ((int)levelCompleteReward) + "";



        float totalReward = levelCompleteReward + enemiesKillReward;
        for (float i = 0; i < totalReward; i += totalReward / 20)
        {
            totalRewardText.text = ((int)i) + "";
            yield return new WaitForSeconds(Time.deltaTime * 2);
        }
        totalRewardText.text = ((int)totalReward) + "";


        for (int i = 0; i < buttonsToDisable.Length; i++)
        {
            buttonsToDisable[i].SetActive(true);
        }

        PlayerPrefsHandler.Coins += (int)totalReward;

    }

}
