//using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsContainer : MonoBehaviour
{
    private static GameObjectsContainer instance;
    public static GameObjectsContainer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameObjectsContainer>();
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
    public PrefabsContainer prefabsContainer;
    public GameObject currentLevelControllerGameObject;
    private LevelController levelControllerScript_ref;
    public LevelController currentLevelController_Script
    {
        get
        {
            if(levelControllerScript_ref == null)
            {
                levelControllerScript_ref = currentLevelControllerGameObject.GetComponent<LevelController>();
            }
            return levelControllerScript_ref;
        }
    }


    public GameObject playerGameObject;
    //private vThirdPersonController _vThirdPersonController_Script_ref;

    //public vThirdPersonController player_vThirdPersonController_Script
    //{
    //    get
    //    {
    //        if(_vThirdPersonController_Script_ref == null)
    //        {
    //            _vThirdPersonController_Script_ref = playerGameObject.GetComponent<vThirdPersonController>();
    //        }
    //        return _vThirdPersonController_Script_ref;
    //    }
    //}

}
