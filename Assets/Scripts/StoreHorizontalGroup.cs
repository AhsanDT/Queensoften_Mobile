using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreHorizontalGroup : MonoBehaviour
{
    [Header("Item1 Data")]
    public GameObject Item1;
    public Text Item1Title;
    public Text Item1Quantity;
    public Image Item1Image;
    public Button Item1Button;
    [Header("Item2 Data")]
    public GameObject Item2;
    public Text Item2Title;
    public Text Item2Quantity;
    public Image Item2Image;
    public Button Item2Button;

    [Header("Button Images")]
    public Sprite SimpleButton;
    public Sprite CoinButton;
}
