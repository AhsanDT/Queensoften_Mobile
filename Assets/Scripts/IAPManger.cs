using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManger : MonoBehaviour , IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    [SerializeField]
    public PurchasingType[] IAPData;


    private static IAPManger instance;
    public static IAPManger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<IAPManger>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    public enum PurchaseType
    {
        UnlockAllLevels = 0,
        UnlockAllCars = 1,
        UnLockEveryThing = 2,
        Coins = 3,
        RemoveAds = 4,
        Joker = 5,
        Shuffle = 6 ,
        EarthSuit = 7 ,
        RoseSuit = 8 , 
        SkySuit = 9 , 
        RoyalSuit = 10
    }
    [System.Serializable]
    public class PurchasingType
    {
        public ProductType productType;
        public int ConsumeValue;
        public string StoreId;
        public PurchaseType purchaseType;
        public string PlayerPrefID;
        [HideInInspector]
        public bool Purchase;
    }

    //[HideInInspector]
    public List<PurchasingType> CoinsList = new List<PurchasingType>();
    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
        AddCoinsToList();
    }


    private void AddCoinsToList()
    {
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.Coins)
            {
                CoinsList.Add(IAPData[i]);
            }
        }
    }
   
    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach(PurchasingType IAPData in IAPData)
        {
            builder.AddProduct(IAPData.StoreId, IAPData.productType, new IDs() { { IAPData.StoreId, AppleAppStore.Name }, { IAPData.StoreId, GooglePlay.Name } });
        }
        UnityPurchasing.Initialize(this, builder);
    }
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void IAPUnLockAllLevels()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
       // isBulletPack = true;
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.UnlockAllLevels)
            {
                IAPData[i].Purchase = true;
                BuyProductID(IAPData[i].StoreId);
                break;
            }
        }
    }
    public void IAPUnLockAllVehicles()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.UnlockAllCars)
            {
                IAPData[i].Purchase = true;
                BuyProductID(IAPData[i].StoreId);
                break;
            }
        }
    }
    public void IAPUnLockEveryhting()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.UnLockEveryThing)
            {
                IAPData[i].Purchase = true;
                BuyProductID(IAPData[i].StoreId);
                break;
            }
        }
    }
    public void IAPRemoveADs()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.RemoveAds)
            {
                IAPData[i].Purchase = true;
                BuyProductID(IAPData[i].StoreId);
                break;
            }
        }
    }
    public void IAPCoins(int index, int value)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            return;
        //for (int i = 0; i < IAPData.Length; i++)
        //{
        //    if (IAPData[i].purchaseType == PurchaseType.Coins)
        //    {
        //        IAPData[i].Purchase = true;
        //        BuyProductID(IAPData[i].StoreId);
        //        break;
        //    }
        //}
        CoinsList[index].Purchase = true;
        
        BuyProductID(CoinsList[index].StoreId, value);

    }
    public void IAPShuffle()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.Shuffle)
            {
                IAPData[i].Purchase = true;
                BuyProductID(IAPData[i].StoreId);
                break;
            }
        }
    }
    public void IAPJoker()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.Joker)
            {
                IAPData[i].Purchase = true;
                BuyProductID(IAPData[i].StoreId);
                break;
            }
        }
    }

    public void IAPEarth()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.EarthSuit)
            {
                IAPData[i].Purchase = true;
                BuyProductID(IAPData[i].StoreId);
                break;
            }
        }
    }

    public void IAPRose()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.RoseSuit)
            {
                IAPData[i].Purchase = true;
                BuyProductID(IAPData[i].StoreId);
                break;
            }
        }
    }

    public void IAPSky()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.SkySuit)
            {
                IAPData[i].Purchase = true;
                BuyProductID(IAPData[i].StoreId);
                break;
            }
        }
    }

    public void IAPRoyal()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }
        for (int i = 0; i < IAPData.Length; i++)
        {
            if (IAPData[i].purchaseType == PurchaseType.RoyalSuit)
            {
                IAPData[i].Purchase = true;
                BuyProductID(IAPData[i].StoreId);
                break;
            }
        }
    }

    public static int IAPGetPurchaseNonConsumer(string PlayerPrefId)
    {
        return PlayerPrefs.GetInt(PlayerPrefId, 0);
    }
    public static void IAPSetPurchaseNonConsumer(string PlayerPrefId, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefId, value);
    }

    public static int IAPGetPurchaseConsumer(string PlayerPrefId)
    {
        return PlayerPrefs.GetInt(PlayerPrefId, 0);
    }
    public static void IAPSetPurchaseConsumer(string PlayerPrefId, int value)
    {
        PlayerPrefs.SetInt(PlayerPrefId, value);
    }
    public void BuyProductID(string productId,int CoinsAmount = 0)
    {
        try
        {
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productId);
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: Completed!");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

        if(args.purchasedProduct.definition.type == ProductType.Consumable)
        {
            foreach(PurchasingType IAPData in IAPData)
            {
                if (IAPData.productType == ProductType.Consumable)
                {
                    if (IAPData.Purchase)
                    {
                        IAPData.Purchase = false;
                        //if (IAPData.purchaseType == PurchaseType.Coins)
                        //{
                        //    int CurrentCoins = IAPGetPurchaseConsumer(IAPData.PlayerPrefID);
                        //    CurrentCoins += IAPData.ConsumeValue;
                        //    IAPSetPurchaseConsumer(IAPData.PlayerPrefID, CurrentCoins);
                        //    Debug.Log("CoinsAdded " + IAPData.ConsumeValue);
                        //}
                        StoreData.Instance.SetDataToApiAfterPurchase();
                       
                    }
                }
            }
        }
        if(args.purchasedProduct.definition.type == ProductType.NonConsumable)
        {
            foreach (PurchasingType IAPData in IAPData)
            {
                if (IAPData.productType == ProductType.NonConsumable)
                {
                    StoreData.Instance.SetDataToApiAfterPurchase();
                    if (IAPData.Purchase)
                    {
                        if (IAPData.purchaseType == PurchaseType.RemoveAds)
                        {
                            IAPData.Purchase = false;
                           
                            IAPSetPurchaseNonConsumer(IAPData.PlayerPrefID, 1);
                            Debug.Log(IAPData.productType + " Purchased");
                        }
                        else
                        {
                            IAPData.Purchase = false;
                            IAPSetPurchaseNonConsumer(IAPData.PlayerPrefID, 1);
                            Debug.Log(IAPData.productType + " Purchased");
                        }
                    }
                }
            }
        }
        if (args.purchasedProduct.definition.type == ProductType.Subscription)
        {

        }
        //if (String.Equals(args.purchasedProduct.definition.id, pMoney80, StringComparison.Ordinal))
        //{
        //    //Action for money
        //}
        //else if (String.Equals(args.purchasedProduct.definition.id, pNoAds, StringComparison.Ordinal))
        //{
        //    //Action for no ads
        //}

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
