//using Invector.vItemManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiObjectsContainer : MonoBehaviour
{
    private static UiObjectsContainer instance;
    public static UiObjectsContainer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiObjectsContainer>();
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
    }
    public GameObject gameProgressionHud;
    public Camera gameProgressionHud_Camera;


    public GameObject playerControlsPanel;
    public GameObject gamePlayHudPanel;

    public GameObject pausePanel;
    public GameObject levelWinPanel;
    public GameObject levelFailPanel;
    public GameObject loadingPanel;
    public GameObject LightBakedEnv;
    

}
