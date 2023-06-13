using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyStats : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject TimeTrailObject;
    public GameObject FreePlayObject;


    public GameObject ScrollObjectFreePaly;
    public GameObject ScrollObjectTimed;

    public GameObject NoDataFound;
    public List<StatsDataPrefabScript> AllDataFreePlay = new List<StatsDataPrefabScript>();
    public List<StatsDataPrefabScript> AllDataTimed = new List<StatsDataPrefabScript>();

    public Transform ContentParentFreeplay;
    public Transform ContentParentTimed;
    public float ContentMinValue = -600f;

    public GameObject StatsPrefab;
    public GameObject Panel;

    int LengthFreePlay = 0;
    int LengthTimed = 0;

    //bool Once;
    
    //private void Start()
    //{
    //    Once = true;
     
    //}

    //private void Update()
    //{
    //    if(Once)
    //    {
    //        if(Panel.activeInHierarchy)
    //        {
    //            Once = false;
               
    //        }
    //    }
    //}
    public class NewJSON
    {
        public Data[] data;
    }

    public void ResetContentsValues()
    {
        ContentParentFreeplay.GetComponent<RectTransform>().offsetMax = new Vector2(ContentParentFreeplay.GetComponent<RectTransform>().offsetMax.x, ((LengthFreePlay + LengthTimed) * ContentMinValue));
        ContentParentTimed.GetComponent<RectTransform>().offsetMax = new Vector2(ContentParentTimed.GetComponent<RectTransform>().offsetMax.x, ((LengthFreePlay + LengthTimed) * ContentMinValue));
    }
    public class Data
    {
        public string date;
        public int total_played;
        public int total_won;
        public int total_lost;
        public string win_percentage;
        public int current_winning_streak;
        public int longest_winning_streak;
        public int longest_losing_streak;
        public int average_score;
        public string best_time;
    }

    public void GetDataFromJson(string jsonwithData, bool isFreePlay = true)
    {
        //var MyJsonClass2 = JsonUtility.FromJson<NewJSON>(jsonwithData);
        Debug.Log("In Stats New : " + jsonwithData);
        NewJSON MyJsonClass = JsonConvert.DeserializeObject<NewJSON>(jsonwithData);

        if(isFreePlay)
        {
            LengthFreePlay = MyJsonClass.data.Length;
        }
        else
        {
            LengthTimed = MyJsonClass.data.Length;
        }
       
        GetDataNow(MyJsonClass , isFreePlay);
    }
    void GetDataNow(NewJSON MyJsonClass , bool IsFreePlay)
    {
      if(IsFreePlay)
        {
            if (AllDataFreePlay.Count > 0)
            {
                foreach (StatsDataPrefabScript items in AllDataFreePlay)
                {
                    Destroy(items.gameObject);
                }

                AllDataFreePlay.Clear();
            }
            for (int i = 0; i< MyJsonClass.data.Length;i++)
            {
                GameObject FreePlayData = Instantiate(StatsPrefab, ContentParentFreeplay);
                StatsDataPrefabScript Cgroup = FreePlayData.GetComponent<StatsDataPrefabScript>();
                Cgroup.container_data.containertextCoponents.data_played.text = MyJsonClass.data[i].date.ToString();
                Cgroup.container_data.containertextCoponents.Game_Played.text = MyJsonClass.data[i].total_played.ToString();
                Cgroup.container_data.containertextCoponents.Game_Won.text = MyJsonClass.data[i].total_won.ToString();
                Cgroup.container_data.containertextCoponents.Game_Last.text = MyJsonClass.data[i].total_lost.ToString();
                Cgroup.container_data.containertextCoponents.Win_Percentage.text = MyJsonClass.data[i].win_percentage.ToString();
                Cgroup.container_data.containertextCoponents.Current_Winning_Streak.text = MyJsonClass.data[i].current_winning_streak.ToString();
                Cgroup.container_data.containertextCoponents.Longest_Winning_Streak.text = MyJsonClass.data[i].longest_winning_streak.ToString();
                Cgroup.container_data.containertextCoponents.Longest_Losing_Streak.text = MyJsonClass.data[i].longest_losing_streak.ToString();
                Cgroup.container_data.containertextCoponents.Average_Score_per_game.text = MyJsonClass.data[i].average_score.ToString();
                Cgroup.container_data.containertextCoponents.Best_Time.text = MyJsonClass.data[i].best_time.ToString();
                Cgroup.container_data.containertextCoponents.Title.text = "FreePlay " + (i + 1).ToString("00");
                AllDataFreePlay.Add(Cgroup);

            }
            if (MyJsonClass.data.Length <= 0)
            {
                NoDataFound.SetActive(true);
            }
            else
            {
                NoDataFound.SetActive(false);
            }
            //  FreePlayButtonClicked();


        }
      else
        {
            if(MyJsonClass.data.Length <=0)
            {
                NoDataFound.SetActive(true);
            }
            else
            {
                NoDataFound.SetActive(false);
            }
           
            if (AllDataTimed.Count > 0)
            {
                foreach (StatsDataPrefabScript items in AllDataTimed)
                {
                    Destroy(items.gameObject);
                }

                AllDataTimed.Clear();
            }

            for (int i = 0; i < MyJsonClass.data.Length; i++)
            {
                Debug.Log("Timed");
                GameObject FreePlayData = Instantiate(StatsPrefab, ContentParentTimed);
                StatsDataPrefabScript Cgroup = FreePlayData.GetComponent<StatsDataPrefabScript>();
                Cgroup.container_data.containertextCoponents.data_played.text = MyJsonClass.data[i].date.ToString();
                Cgroup.container_data.containertextCoponents.Game_Played.text = MyJsonClass.data[i].total_played.ToString();
                Cgroup.container_data.containertextCoponents.Game_Won.text = MyJsonClass.data[i].total_won.ToString();
                Cgroup.container_data.containertextCoponents.Game_Last.text = MyJsonClass.data[i].total_lost.ToString();
                Cgroup.container_data.containertextCoponents.Win_Percentage.text = MyJsonClass.data[i].win_percentage.ToString();
                Cgroup.container_data.containertextCoponents.Current_Winning_Streak.text = MyJsonClass.data[i].current_winning_streak.ToString();
                Cgroup.container_data.containertextCoponents.Longest_Winning_Streak.text = MyJsonClass.data[i].longest_winning_streak.ToString();
                Cgroup.container_data.containertextCoponents.Longest_Losing_Streak.text = MyJsonClass.data[i].longest_losing_streak.ToString();
                Cgroup.container_data.containertextCoponents.Average_Score_per_game.text = MyJsonClass.data[i].average_score.ToString();
                Cgroup.container_data.containertextCoponents.Best_Time.text = MyJsonClass.data[i].best_time.ToString();
                Cgroup.container_data.containertextCoponents.Title.text = "Timed " + (i + 1).ToString("00");
                AllDataTimed.Add(Cgroup);

            }

            //TimedPlayButtonClicked();
        }
        Debug.Log("Stats Entered");
        ResetContentsValues();
    }



    public void FreePlayButtonClicked()
    {
        ApiCode.Instance.SetDataTOAPIMyStats("FreePlay");
        MainMenuController.Instance.ButtonClickSound();
      

        TimeTrailObject.SetActive(false);
        FreePlayObject.SetActive(true);
        ScrollObjectFreePaly.SetActive(true);
        ScrollObjectTimed.SetActive(false);
    }
    public void TimedPlayButtonClicked()
    {
        ApiCode.Instance.SetDataTOAPIMyStats("Timed");
        MainMenuController.Instance.ButtonClickSound();
       

        TimeTrailObject.SetActive(true);
        FreePlayObject.SetActive(false);
        ScrollObjectFreePaly.SetActive(false);
        ScrollObjectTimed.SetActive(true);
    }
}
