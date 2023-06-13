using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreYouReadyScript : MonoBehaviour
{
   public void StartGame()
    {
        FreePlayGameLogic.Instance.OnStartGame();
        gameObject.SetActive(false);
    }
}
