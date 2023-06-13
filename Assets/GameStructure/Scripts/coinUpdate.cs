using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class coinUpdate : MonoBehaviour
{
    public Text coinText;
    private void Reset()
    {
        coinText = gameObject.GetComponentInChildren<Text>();
    }

    
       

    // Start is called before the first frame update
    void Update()
    {
       
      
        if (coinText)
        {

            coinText.text = PlayerPrefsHandler.Coins + "";
        }
        else { Debug.LogError("Text Component Is Null"); }

    }

  
       





}
