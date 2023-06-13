using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static StoreData;

public class MyItemsData : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject[] AllItems;
    //public int[] AllItemsID;

    //public List<int> IdFromServer = new List<int>();

    public int[] CurrentQuantity;
    public string[] ItemNames;
    public int[] ItemIds;
    public List<int> ItemIdsOnlyCards = new List<int>();
    private int TotalNumberOFObjects;
    public GameObject HorizontalPrefab;
    public Transform ContentParent;
    public ScrollRect scrollRect;
    public List<Button> AllButtons = new List<Button>();
    public float ContentMinValue = -600f;
    public List<StoreHorizontalGroup> UserItemsGroup = new List<StoreHorizontalGroup>();

    private bool SomeButtonIsDeactivated = false;
    private int CurrentButtonIndex;
    private bool IsDefaultCardClicked = false;


    public int CurrentJokerValue = 0;
    public int CurrentShuffleValue = 0;

    private static MyItemsData instance;
    public static MyItemsData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MyItemsData>();
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
    public void ResetContentsValues()
    {
        ContentParent.GetComponent<RectTransform>().offsetMax = new Vector2(ContentParent.GetComponent<RectTransform>().offsetMax.x, (ItemNames.Length * ContentMinValue));
    }

    public void GetDataFromJSonUserItems(string jsonwithData)
    {
        var DataFromJson = JsonUtility.FromJson<NewJSON>(jsonwithData);
        NewJSON MyJsonClass = JsonConvert.DeserializeObject<NewJSON>(jsonwithData);
        CurrentQuantity = new int[MyJsonClass.data.Length + 1];
        ItemNames = new string[MyJsonClass.data.Length + 1];
        ItemIds = new int[MyJsonClass.data.Length + 1];
        TotalNumberOFObjects = (MyJsonClass.data.Length / 2);
        currentIndexImage = 0;
        CurrentNumberOfObjects = 0;
        if(UserItemsGroup.Count>0)
        {
            foreach(StoreHorizontalGroup items in UserItemsGroup)
            {
                Destroy(items.gameObject);
            }

            UserItemsGroup.Clear();
        }
       
        AddImagesAndData(MyJsonClass);
        Debug.Log("Succes UserItems");

    }
    int currentIndexImage = 0;
    private int CurrentNumberOfObjects = 0;
    void AddImagesAndData(NewJSON MyJsonClass)
    {
        AllButtons.Clear();
        ItemIdsOnlyCards.Clear();
        while (CurrentNumberOfObjects < MyJsonClass.data.Length)
        {
            GameObject HorizonP = Instantiate(HorizontalPrefab, ContentParent);
            StoreHorizontalGroup Cgroup = HorizonP.GetComponent<StoreHorizontalGroup>();
            UserItemsGroup.Add(Cgroup);

            if ((CurrentNumberOfObjects % 2) == 0)
            {
                Cgroup.Item1Title.text = MyJsonClass.data[CurrentNumberOfObjects].item_name.ToString();


                for (int i = 0 ; i < ApiCode.Instance.store_data.ItemIds.Length;i++)
                {
                    if(MyJsonClass.data[CurrentNumberOfObjects].item_id== ApiCode.Instance.store_data.ItemIds[i])
                    {
                        Cgroup.Item1Image.sprite = ApiCode.Instance.store_data.ItemSprites[i];
                    }
                }

               

                if (MyJsonClass.data[CurrentNumberOfObjects].type=="card" )
                {
                    Cgroup.Item1Quantity.text = MyJsonClass.data[CurrentNumberOfObjects].quantity.ToString();
                    Cgroup.Item1Button.GetComponent<Image>().sprite = Cgroup.CoinButton;
                    if(MyJsonClass.data[CurrentNumberOfObjects].item_id==1)
                    {
                        CurrentShuffleValue = MyJsonClass.data[CurrentNumberOfObjects].quantity;
                    }
                    if (MyJsonClass.data[CurrentNumberOfObjects].item_id == 2)
                    {
                        CurrentJokerValue = MyJsonClass.data[CurrentNumberOfObjects].quantity;
                    }

                    MainMenuController.Instance.freePlayGameLogicScript.SetQuantititiesOfItems();
                    SetButtonOnItem(CurrentNumberOfObjects, Cgroup.Item1Button, MyJsonClass.data[CurrentNumberOfObjects].item_name.ToString(), MyJsonClass.data[CurrentNumberOfObjects ].item_id, MyJsonClass.data[CurrentNumberOfObjects].quantity);
                }
                else
                {
                    AllButtons.Add(Cgroup.Item1Button);
                    Cgroup.Item1Quantity.text = "Use";
                    Cgroup.Item1Button.GetComponent<Image>().sprite = Cgroup.SimpleButton;
                    SetButtonOnItemSkin(CurrentNumberOfObjects , Cgroup.Item1Button, MyJsonClass.data[CurrentNumberOfObjects].item_id);

                    ItemIdsOnlyCards.Add(MyJsonClass.data[CurrentNumberOfObjects].item_id);
                }
                //SetButtonOnPurchase(Cgroup.Item1Button, CurrentNumberOfObjects);
                // Cgroup.Item1Button.onClick.AddListener(() => GetPurchase(CurrentNumberOfObjects));
                CurrentQuantity[CurrentNumberOfObjects] = MyJsonClass.data[CurrentNumberOfObjects].quantity;
               // ItemPrices[CurrentNumberOfObjects] = MyJsonClass.data[CurrentNumberOfObjects].price;
                ItemNames[CurrentNumberOfObjects] = MyJsonClass.data[CurrentNumberOfObjects].item_name;
                ItemIds[CurrentNumberOfObjects] = MyJsonClass.data[CurrentNumberOfObjects].item_id;
                if ((CurrentNumberOfObjects + 1) < MyJsonClass.data.Length)
                {
                    if (((CurrentNumberOfObjects + 1) % 2) == 1)
                    {
                        Cgroup.Item2Title.text = MyJsonClass.data[CurrentNumberOfObjects + 1].item_name.ToString();

                        for (int i = 0; i < ApiCode.Instance.store_data.ItemIds.Length; i++)
                        {
                            if (MyJsonClass.data[CurrentNumberOfObjects + 1].item_id == ApiCode.Instance.store_data.ItemIds[i])
                            {
                                Cgroup.Item2Image.sprite = ApiCode.Instance.store_data.ItemSprites[i];
                            }
                        }

                        
                        if (MyJsonClass.data[CurrentNumberOfObjects + 1].type == "card")
                        {
                            Cgroup.Item2Quantity.text = MyJsonClass.data[CurrentNumberOfObjects + 1].quantity.ToString();
                            Cgroup.Item2Button.GetComponent<Image>().sprite = Cgroup.CoinButton;
                            if (MyJsonClass.data[CurrentNumberOfObjects + 1].item_id == 1)
                            {
                                CurrentShuffleValue = MyJsonClass.data[CurrentNumberOfObjects + 1].quantity;
                            }
                            if (MyJsonClass.data[CurrentNumberOfObjects + 1].item_id == 2)
                            {
                                CurrentJokerValue = MyJsonClass.data[CurrentNumberOfObjects + 1].quantity;
                            }
                            MainMenuController.Instance.freePlayGameLogicScript.SetQuantititiesOfItems();
                            SetButtonOnItem(CurrentNumberOfObjects + 1 , Cgroup.Item2Button, MyJsonClass.data[CurrentNumberOfObjects + 1].item_name.ToString(), MyJsonClass.data[CurrentNumberOfObjects + 1].item_id , MyJsonClass.data[CurrentNumberOfObjects + 1].quantity);
                        }
                        else
                        {
                            AllButtons.Add(Cgroup.Item2Button);
                            Cgroup.Item2Quantity.text = "Use";
                            Cgroup.Item2Button.GetComponent<Image>().sprite = Cgroup.SimpleButton;
                            SetButtonOnItemSkin(CurrentNumberOfObjects + 1, Cgroup.Item2Button, MyJsonClass.data[CurrentNumberOfObjects + 1].item_id);
                            ItemIdsOnlyCards.Add(MyJsonClass.data[CurrentNumberOfObjects + 1].item_id);
                        }
                        //SetButtonOnPurchase(Cgroup.Item2Button, CurrentNumberOfObjects + 1);
                        //Cgroup.Item2Button.onClick.AddListener(() => GetPurchase(CurrentNumberOfObjects + 1));
                        CurrentQuantity[CurrentNumberOfObjects + 1] = MyJsonClass.data[CurrentNumberOfObjects + 1].quantity;
                       
                        ItemNames[CurrentNumberOfObjects + 1] = MyJsonClass.data[CurrentNumberOfObjects + 1].item_name;
                        ItemIds[CurrentNumberOfObjects + 1] = MyJsonClass.data[CurrentNumberOfObjects + 1].item_id;
                    }
                }
                else
                {
                    //Cgroup.Item2.SetActive(false);

                    Cgroup.Item2Title.text = "Default";
                    AllButtons.Add(Cgroup.Item2Button);
                    Cgroup.Item2Image.sprite = MainMenuController.Instance.freePlaySetting.DefaulImage;
                    Cgroup.Item2Quantity.text = "Use";
                    Cgroup.Item2Button.GetComponent<Image>().sprite = Cgroup.SimpleButton;
                    SetButtonOnItemSkinDefault(CurrentNumberOfObjects + 1, Cgroup.Item2Button, 0);
                   // Debug.LogError("Here11");
                    //Cgroup.Item2.SetActive(false);
                }
            }
           
            CurrentNumberOfObjects += 2;
        }

        if(MyJsonClass.data.Length % 2 ==0)
        {
            //Adding Default Skin
            GameObject HorizonPnew = Instantiate(HorizontalPrefab, ContentParent);
            StoreHorizontalGroup Cgroupnew = HorizonPnew.GetComponent<StoreHorizontalGroup>();
            UserItemsGroup.Add(Cgroupnew);
            AllButtons.Add(Cgroupnew.Item1Button);
            Cgroupnew.Item1Title.text = "Default";

            Cgroupnew.Item1Image.sprite = MainMenuController.Instance.freePlaySetting.DefaulImage;
            Cgroupnew.Item1Quantity.text = "Use";
            Cgroupnew.Item1Button.GetComponent<Image>().sprite = Cgroupnew.SimpleButton;
            //Debug.LogError("Here");
            SetButtonOnItemSkinDefault((AllButtons.Count - 1), Cgroupnew.Item1Button, 0);
            Cgroupnew.Item2.SetActive(false);
        }

        if(SomeButtonIsDeactivated)
        {
            if(IsDefaultCardClicked)
            {
                AllButtons[(AllButtons.Count-1)].interactable = false;
            }
            else
            {
                for (int i = 0; i < ItemIdsOnlyCards.Count; i++)
                {

                    if (ItemIdsOnlyCards[i] == CurrentButtonIndex)
                    {
                        Debug.Log("Current ItemId Card " + CurrentButtonIndex);
                        Debug.Log("Current Index Card " + i);
                        AllButtons[i].interactable = false;
                    }

                    
                        
                }

               

               
            }
           
        }

        for (int i = 0; i < ItemIdsOnlyCards.Count; i++)
        {
            //Debug.LogError("Item Id : " + ItemIdsOnlyCards[i] + " CurrentSkin : " + ApiCode.Instance.CurrenCardSkin);
            if(ApiCode.Instance.CurrenCardSkin ==0)
            {
                AllButtons[(AllButtons.Count - 1)].interactable = false;
            }
            else
            {
                if (ItemIdsOnlyCards[i] == ApiCode.Instance.CurrenCardSkin)
                {
                    AllButtons[i].interactable = false;
                }
            }
            
        }
        //ItemIdsOnlyCards.Add(0);
        //if(CurrentButtonPressed)
        //{
        //    DeActiveButtons(CurrentButtonPressed);
        //}



    }

    void SetButtonOnItem(int buttonIndex ,  Button button, string value , int itemId , int quantity)
    {
        button.onClick.AddListener(() => GetItemValue(value , itemId , quantity , button , buttonIndex));
    }

    void SetButtonOnItemSkin(int buttonIndex, Button button,int itemId)
    {
        button.onClick.AddListener(() => GetItemValueSkin(itemId, button , itemId));
    }

    void SetButtonOnItemSkinDefault(int buttonIndex, Button button, int itemId)
    {
        button.onClick.AddListener(() => GetItemValueSkinDefault(itemId, button , buttonIndex));
    }


    void DeActiveButtons(Button buttonname, int indexButton , bool DefaultCard = false)
    {
        if(DefaultCard)
        {
            CurrentButtonIndex = (AllButtons.Count-1);
            IsDefaultCardClicked = true;
        }
        else
        {
            CurrentButtonIndex = indexButton;
            IsDefaultCardClicked = false;
        }
        SomeButtonIsDeactivated = true;
       
        for (int i = 0;i<AllButtons.Count;i++)
        {
            AllButtons[i].interactable = true;
        }
        buttonname.interactable = false;
    }
    private void GetItemValue(string Name , int itemid , int quantity , Button CButton , int indexButton)
    {
        //Debug.Log("Card  Name " + Name);
        MainMenuController.Instance.ButtonClickSound();
        switch (Name)
        {
            case "Joker":
                {
                    MainMenuController.Instance.freePlayGameLogicScript.SetCurrentItemIDAndQuantity(itemid, quantity);
                    MainMenuController.Instance.freePlayGameLogicScript.cardtypeactive = FreePlayGameLogic.CardTypeActive.joker;
                    MainMenuController.Instance.freePlayGameLogicScript.WhatStoreCardsToEnable();
                    ApiCode.Instance.ItemSelected.SetActive(true);
                    ApiCode.Instance.LoaderObject.SetActive(true);
                    Invoke(nameof(SetDeactiveLoaderObject), 2f);
                    break;
                }
            case "Shuffles":
                {
                    MainMenuController.Instance.freePlayGameLogicScript.SetCurrentItemIDAndQuantity(itemid, quantity);
                    MainMenuController.Instance.freePlayGameLogicScript.cardtypeactive = FreePlayGameLogic.CardTypeActive.shuffle;
                    MainMenuController.Instance.freePlayGameLogicScript.WhatStoreCardsToEnable();
                    ApiCode.Instance.ItemSelected.SetActive(true);
                    ApiCode.Instance.LoaderObject.SetActive(true);
                    Invoke(nameof(SetDeactiveLoaderObject), 2f);
                    break;
                }
        }
    }
    private void SetDeactiveLoaderObject()
    {
        ApiCode.Instance.LoaderObject.SetActive(false);
    }
    private void GetItemValueSkin(int id , Button CButton, int indexButton)
    {
        MainMenuController.Instance.ButtonClickSound();
        DeActiveButtons(CButton, id , false);
        ApiCode.Instance.CurrenCardSkin = id;
        ApiCode.Instance.store_data.CurrentSkinImageSet();
        ApiCode.Instance.SetDataToAPIItemSkinUse(id);
    }

    private void GetItemValueSkinDefault(int id, Button CButton, int indexButton)
    {
        MainMenuController.Instance.ButtonClickSound();
        DeActiveButtons(CButton, id , true);
        ApiCode.Instance.CurrenCardSkin = id;
        ApiCode.Instance.store_data.CurrentSkinImageSetDefault();
        ApiCode.Instance.SetDataToAPIItemSkinUse(id);
    }

}
