using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static ApiCode;

public class MyAchievements : MonoBehaviour
{

    public GameObject HorizontalPrefab;
    public Transform ContentParent;
    public float ContentMinValue = -600f;
    public List<AchieveMentsHorizontalGroup> storeGroup = new List<AchieveMentsHorizontalGroup>();

    public GameObject TimeTrailObject;
    public GameObject FreePlayObject;
    // public GameObject AchievementUnlocked;

    private int currentCount = 1;

    private int CurrentNumberOfObjects = 0;
    private int storeindex = 0;
    public class NewJSONAchievement
    {
        public Data data;
    }
    public class Data
    {
       public int totalChallenges;
       public AllChallenges[] achievements;
    }


    public class AllChallenges
    {
        public string title;
        public string date;
        public string hour;
        public  string minute;
        public  string games;
        public  string occurrence;
        public  string active;
        public string prize;


    }
    public void ResetContentsValues()
    {
        ContentParent.GetComponent<RectTransform>().offsetMax = new Vector2(ContentParent.GetComponent<RectTransform>().offsetMax.x, (currentCount * ContentMinValue));
        //ApiCode.Instance.LoaderObject.SetActive(false);
    }

    public void GetDataFromJSonAchievement(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<NewJSONAchievement>(jsonwithData);
        NewJSONAchievement MyJsonClass = JsonConvert.DeserializeObject<NewJSONAchievement>(jsonwithData);
        currentCount = MyJsonClass.data.totalChallenges;
        CurrentNumberOfObjects = 0;
        storeindex = 0;
        AddingEmptyChallenges(MyJsonClass);
        Debug.Log("Succes Achievements");
    }
    private void AddingEmptyChallenges(NewJSONAchievement MyJsonClass)
    {
        if(storeGroup.Count>0)
        {
            for(int i = 0;i<storeGroup.Count;i++)
            {
                Destroy(storeGroup[i].gameObject);
            }
        }

        storeGroup.Clear();

        for(int j = 0;j< MyJsonClass.data.totalChallenges; j+=3)
        {
            GameObject HorizonP = Instantiate(HorizontalPrefab, ContentParent);
            AchieveMentsHorizontalGroup Cgroup = HorizonP.GetComponent<AchieveMentsHorizontalGroup>();
            storeGroup.Add(Cgroup);
            if ((j % 3) == 0)
            {
                if ((j + 1) < MyJsonClass.data.totalChallenges)
                {
                    if (((j + 1) % 3) == 1)
                    {

                    }
                }
                else
                {
                    Cgroup.Item2.SetActive(false);
                }

                if ((j + 2) < MyJsonClass.data.totalChallenges)
                {
                    if (((j + 2) % 3) == 1)
                    {

                    }
                }
                else
                {
                    Cgroup.Item3.SetActive(false);
                }
            }
        }
        AddImagesAndDataOfCompletedAchievements(MyJsonClass);
    }
    void AddImagesAndDataOfCompletedAchievements(NewJSONAchievement MyJsonClass)
    {
        while (CurrentNumberOfObjects < MyJsonClass.data.achievements.Length)
        {

            //GameObject HorizonP = Instantiate(HorizontalPrefab, ContentParent);
            //AchieveMentsHorizontalGroup Cgroup = HorizonP.GetComponent<AchieveMentsHorizontalGroup>();
            //storeGroup.Add(Cgroup);
          
            AchieveMentsHorizontalGroup Cgroup = storeGroup[storeindex];
            if ((CurrentNumberOfObjects % 3) == 0)
            {
                
                Cgroup.Item1Title.text = MyJsonClass.data.achievements[CurrentNumberOfObjects].title.ToString();
                Cgroup.Item1Button.interactable = true;

                if ((CurrentNumberOfObjects + 1) < MyJsonClass.data.achievements.Length)
                {
                    Cgroup.Item2Title.text = MyJsonClass.data.achievements[CurrentNumberOfObjects + 1].title.ToString();
                    Cgroup.Item2Button.interactable = true;
                    //if (((CurrentNumberOfObjects + 1) % 3) == 1)
                    //{
                    //    Cgroup.Item2Title.text = MyJsonClass.data.achievements[CurrentNumberOfObjects + 1].title.ToString();
                    //    Cgroup.Item2Button.interactable = true;
                    //}
                }
                else
                {
                    //Cgroup.Item2.SetActive(false);
                }

                if ((CurrentNumberOfObjects + 2) < MyJsonClass.data.achievements.Length)
                {
                    Cgroup.Item3Title.text = MyJsonClass.data.achievements[CurrentNumberOfObjects + 2].title.ToString();
                    Cgroup.Item3Button.interactable = true;
                    //if (((CurrentNumberOfObjects + 2) % 3) == 1)
                    //{
                       
                    //}
                }
                else
                {
                    //Cgroup.Item3.SetActive(false);
                }
            }
            CurrentNumberOfObjects += 3;
            storeindex+=1;
            //CurrentNumberOfObjects += 1;
        }
        ResetContentsValues();
    }

    public void CallMyAchievementsAPI()
    {
        ApiCode.Instance.LoaderObject.SetActive(true);
        ApiCode.Instance.SetDataTOAPIMyAchievements();
    }

    public void FreePlayButtonClicked()
    {
        TimeTrailObject.SetActive(false);
        FreePlayObject.SetActive(true);
        CallMyAchievementsAPI();
        MainMenuController.Instance.ButtonClickSound();
    }
    public void TimedPlayButtonClicked()
    {
        TimeTrailObject.SetActive(true);
        FreePlayObject.SetActive(false);
        CallMyAchievementsAPI();
        MainMenuController.Instance.ButtonClickSound();
    }
}
