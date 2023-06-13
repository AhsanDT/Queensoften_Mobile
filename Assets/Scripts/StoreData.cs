using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StoreData : MonoBehaviour
{
    // Start is called before the first frame update



    public string JsonDataStore;
    //public Text[] ItemNames;
    //public Text[] ItemQuantity;
    public int[] CurrentQuantity;
    public string[] ItemPrices;
    public string[] ItemNames;
    public int[] ItemIds;
    public Button[] ItemButtons;
    public Sprite[] ItemSprites;
    //public Image[] ItemImages;


    public GameObject HorizontalPrefab;
    public Transform ContentParent;
    public ScrollRect scrollRect;
    public GameObject PurchaseObject;

    public Text PurchaseText;
    public float ContentMinValue = -600f;
    private int TotalNumberOFObjects;
    private int CurrentNumberOfObjects = 0;

    private int CurrentIndex;
    public List<StoreHorizontalGroup> storeGroup = new List<StoreHorizontalGroup>();
    public GameObject StorePanel;
    public bool UseLocalImages;
    public GameObject PayMentFromStore;
    private static StoreData instance;




    public static StoreData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StoreData>();
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

    
    public class NewJSON
    {
        public Data[] data;
    }

    public void ResetContentsValues()
    {
        ContentParent.GetComponent<RectTransform>().offsetMax = new Vector2(ContentParent.GetComponent<RectTransform>().offsetMax.x, (ItemNames.Length * ContentMinValue));
    }
    public class Data
    {
        public int id;
        public string name;
        public string image;
        public string price;
        public int quantity;
        public string currency;
        public Sprite ImageSprite;

        public string item_name;
        public int item_id;
        public string type;
    }
    public void GetDataFromJSonStore(string jsonwithData)
    {
       
        //AddImagesAndData(MyJsonClass);
        Debug.Log("Succes Store");

    }
    //private void Update()
    //{
    //    //if(StorePanel.activeInHierarchy)
    //    //{
           
    //    //}
    //}
    public void LoadImagesOnce(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<NewJSON>(jsonwithData);
        NewJSON MyJsonClass = JsonConvert.DeserializeObject<NewJSON>(jsonwithData);
       
       
        CurrentQuantity = new int[MyJsonClass.data.Length + 1];
        ItemPrices = new string[MyJsonClass.data.Length + 1];
        ItemNames = new string[MyJsonClass.data.Length + 1];
        ItemIds = new int[MyJsonClass.data.Length + 1];
        ItemButtons = new Button[MyJsonClass.data.Length + 1];
        // ItemSprites = new Sprite[MyJsonClass.data.Length + 1];
        TotalNumberOFObjects = (MyJsonClass.data.Length / 2);
       

        if(UseLocalImages)
        {
            AddImagesAndData(MyJsonClass);
        }
        else
        {
            ItemSprites = new Sprite[MyJsonClass.data.Length + 1];
            StartCoroutine(LoadOnlyImages(MyJsonClass));
        }
        
    }

    IEnumerator LoadOnlyImages(NewJSON MyJsonClass)
    {
        while (currentIndexImage < MyJsonClass.data.Length)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(MyJsonClass.data[currentIndexImage].image);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to get store image no " + currentIndexImage);
            }
            else
            {
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Texture2D tex2D = (Texture2D)myTexture;
                MyJsonClass.data[currentIndexImage].ImageSprite = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0, 0));
                ItemSprites[currentIndexImage] = MyJsonClass.data[currentIndexImage].ImageSprite;
            }
            currentIndexImage++;
        }
        AddImagesAndData(MyJsonClass);
    }
    int currentIndexImage = 0;
    void AddImagesAndData(NewJSON MyJsonClass)
    {
      
            while (CurrentNumberOfObjects < MyJsonClass.data.Length)
            {
                
                GameObject HorizonP = Instantiate(HorizontalPrefab, ContentParent);
                StoreHorizontalGroup Cgroup = HorizonP.GetComponent<StoreHorizontalGroup>();
                storeGroup.Add(Cgroup);

                if ((CurrentNumberOfObjects % 2) == 0)
                {
                    Cgroup.Item1Title.text = MyJsonClass.data[CurrentNumberOfObjects].name.ToString();
                    Cgroup.Item1Quantity.text = MyJsonClass.data[CurrentNumberOfObjects].quantity.ToString();
                //Cgroup.Item1Image.sprite = MyJsonClass.data[CurrentNumberOfObjects].ImageSprite;
                    Cgroup.Item1Image.sprite = ItemSprites[CurrentNumberOfObjects];

                    SetButtonOnPurchase(Cgroup.Item1Button, CurrentNumberOfObjects);
                   // Cgroup.Item1Button.onClick.AddListener(() => GetPurchase(CurrentNumberOfObjects));
                    CurrentQuantity[CurrentNumberOfObjects] = MyJsonClass.data[CurrentNumberOfObjects].quantity;
                    ItemPrices[CurrentNumberOfObjects] = MyJsonClass.data[CurrentNumberOfObjects].price;
                    ItemNames[CurrentNumberOfObjects] = MyJsonClass.data[CurrentNumberOfObjects].name;
                    ItemIds[CurrentNumberOfObjects] = MyJsonClass.data[CurrentNumberOfObjects].id;
                ItemButtons[CurrentNumberOfObjects] = Cgroup.Item1Button;


                if ((CurrentNumberOfObjects + 1) < MyJsonClass.data.Length)
                    {
                        if (((CurrentNumberOfObjects + 1) % 2) == 1)
                        {
                            Cgroup.Item2Title.text = MyJsonClass.data[CurrentNumberOfObjects + 1].name.ToString();
                            Cgroup.Item2Quantity.text = MyJsonClass.data[CurrentNumberOfObjects + 1].quantity.ToString();
                        //Cgroup.Item2Image.sprite = MyJsonClass.data[CurrentNumberOfObjects + 1].ImageSprite;
                        Cgroup.Item2Image.sprite = ItemSprites[CurrentNumberOfObjects + 1];
                        SetButtonOnPurchase(Cgroup.Item2Button, CurrentNumberOfObjects + 1);
                        //Cgroup.Item2Button.onClick.AddListener(() => GetPurchase(CurrentNumberOfObjects + 1));
                        CurrentQuantity[CurrentNumberOfObjects + 1] = MyJsonClass.data[CurrentNumberOfObjects + 1].quantity;
                        ItemPrices[CurrentNumberOfObjects + 1] = MyJsonClass.data[CurrentNumberOfObjects + 1].price;
                        ItemNames[CurrentNumberOfObjects + 1] = MyJsonClass.data[CurrentNumberOfObjects + 1].name;
                        ItemIds[CurrentNumberOfObjects + 1] = MyJsonClass.data[CurrentNumberOfObjects + 1].id;
                        ItemButtons[CurrentNumberOfObjects + 1] = Cgroup.Item2Button;

                    }
                    }
                    else
                    {
                        Cgroup.Item2.SetActive(false);
                    }
                }
                CurrentNumberOfObjects+=2;
            }
        CurrentSkin();
        CurrentSkinImageSet();
        ResetContentsValues();
        CheckIfSkinPurchased();
    }

   
    

    public void CheckIfSkinPurchased()
    {
        for(int i = 0; i < ApiCode.Instance.MyItemsData.ItemIdsOnlyCards.Count;i++)
        {
            for(int j = 0; j <ItemIds.Length;j++)
            {
                    if(ItemIds[j]== ApiCode.Instance.MyItemsData.ItemIdsOnlyCards[i])
                    {
                        ItemButtons[j].interactable = false;
                    }
            }
        }
    }


   

    // m_YourThirdButton.onClick.AddListener(() => ButtonClicked(42));

    void SetButtonOnPurchase(Button button, int value)
    {
        button.onClick.AddListener(() => GetPurchase(value));
    }

    private void GetPurchase(int index)
    {
        //Debug.LogError("Index " + index);
        MainMenuController.Instance.ButtonClickSound();
        CurrentIndex = index;
        PurchaseObject.SetActive(true);
        PurchaseText.text = "Are you sure you want to purchase this feature? It cost $" + ItemPrices[index] + " For " + CurrentQuantity[index] + " " + ItemNames[index];
       

    }

    public void ActivePaymentMethods()
    {
        PurchaseObject.SetActive(false);
        PayMentFromStore.SetActive(true);
    }
    public void BuyButtonFromPurchasePanel()
    {
        PurchaseObject.SetActive(false);
        ApiCode.Instance.SetDataToAPIPurchase(ItemIds[CurrentIndex], CurrentQuantity[CurrentIndex]);
    }


    public void GooglePlayPurchase()
    {
        MainMenuController.Instance.ButtonClickSound();
        switch (CurrentIndex)
        {
            case 0:
                {
                    IAPManger.Instance.IAPShuffle();
                    break;
                }
            case 1:
                {
                    IAPManger.Instance.IAPJoker();
                    break;
                }
            case 2:
                {
                    IAPManger.Instance.IAPEarth();
                    break;
                }
            case 3:
                {
                    IAPManger.Instance.IAPRose();
                    break;
                }
            case 4:
                {
                    IAPManger.Instance.IAPSky();
                    break;
                }
            case 5:
                {
                    IAPManger.Instance.IAPRoyal();
                    break;
                }

        }
    }

    public void SetDataToApiAfterPurchase()
    {
        ApiCode.Instance.SetDataToAPIPurchase(ItemIds[CurrentIndex], CurrentQuantity[CurrentIndex]);
    }
    public void CurrentSkinImageSet()
    {
        for(int i = 0;i< ItemIds.Length;i++)
        {
            //Debug.LogError("Skin No Out " + ApiCode.Instance.CurrenCardSkin);
            if (ItemIds[i] == ApiCode.Instance.CurrenCardSkin)
            {
               // Debug.LogError("Skin No in" + ApiCode.Instance.CurrenCardSkin);
                MainMenuController.Instance.freePlaySetting.CurrentId = ApiCode.Instance.CurrenCardSkin;
                MainMenuController.Instance.freePlaySetting.ImageToReplaceAllCards = ItemSprites[i];
                MainMenuController.Instance.freePlaySetting.ReplaceAllCardsImages();
            }
        }
    }

    public void CurrentSkinImageSetDefault()
    {
        MainMenuController.Instance.freePlaySetting.CurrentId = 0;
        //MainMenuController.Instance.freePlaySetting.ImageToReplaceAllCards = ItemSprites[i];
        MainMenuController.Instance.freePlaySetting.ReplaceAllCardsImages();
    }

    public void CurrentSkin()
    {
        bool notFound = true;

        if(ApiCode.Instance.CurrenCardSkin ==0)
        {
            CurrentSkinImageSetDefault();
        }
        else
        {
            for (int i = 0; i < ApiCode.Instance.MyItemsData.ItemIds.Length; i++)
            {
                if (ApiCode.Instance.MyItemsData.ItemIds[i] == ApiCode.Instance.CurrenCardSkin)
                {
                    notFound = false;
                }
            }
            if (notFound)
            {
                ApiCode.Instance.SetDataToAPIPurchase(ApiCode.Instance.CurrenCardSkin, 1);
            }
        }
       
        
    }
}
