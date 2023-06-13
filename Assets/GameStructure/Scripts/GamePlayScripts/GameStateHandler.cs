using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{ 
    private static GameStateHandler instance;
    public static GameStateHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameStateHandler>();
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

    public int enemiesToKillRemaining;
    public int enemiesToKills_Total;
    public int enemiesKilled;


    public bool isGameStarted;
    public bool isGameInPauseState;
    public bool isGameWin;
    public bool isGameFailed; 

}
