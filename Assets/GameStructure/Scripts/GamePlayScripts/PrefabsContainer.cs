using UnityEngine;

[CreateAssetMenu(fileName = "PrefabsContainer", menuName = "ScriptableObjects/PrefabsContainer", order = 1)]
public class PrefabsContainer : ScriptableObject
{
    public string prefabName;
     
    public GameObject[] playerPrefabs; 
    public GameObject[] environmentPrefabs; 
    public GameObject[] ENV_1_levels;
    public GameObject[] ENV_2_levels;       
     
}
