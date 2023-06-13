using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDataPrefabScript : MonoBehaviour
{

    [SerializeField]
    public ContainerData container_data;


    [System.Serializable]
    public class ContainerData
    {
        public ContainerTextComponents containertextCoponents;

    }


    [System.Serializable]
    public class ContainerTextComponents
    {
        public Text data_played;
        public Text Game_Played;
        public Text Game_Won;
        public Text Game_Last;
        public Text Win_Percentage;
        public Text Current_Winning_Streak;
        public Text Longest_Winning_Streak;
        public Text Longest_Losing_Streak;
        public Text Average_Score_per_game;
        public Text Best_Time;
        public Text Title;

    }

}
