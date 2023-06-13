using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SupportMessageScript : MonoBehaviour
{

    public InputField Subject;
    public InputField Message;
    public Text WarningTextSubject;
    public Text WarningTextMessage;
    public GameObject MessageSend;
    public Text MessageObjectText;


    private static SupportMessageScript instance;


    

    public static SupportMessageScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SupportMessageScript>();
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
    private void OnEnable()
    {
        resetTexts();
       
    }
   
    public void resetTexts()
    {
        WarningTextSubject.text = "";
        WarningTextMessage.text = "";
        Subject.text = "";
        Message.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrEmpty(Subject.text))
        {
            WarningTextSubject.text = "";
        }
        if (!string.IsNullOrEmpty(Message.text))
        {
            WarningTextMessage.text = "";
        }

    }


    public void SendMessageToServer()
    {

        if(!string.IsNullOrEmpty(Subject.text)  && !string.IsNullOrEmpty(Message.text))
        {
            Debug.Log("Sending Support Message To Server");
            ApiCode.Instance.SetDataToAPISupportMessage(Subject.text, Message.text);
        }
        else
        {
            if (string.IsNullOrEmpty(Subject.text))
            {
                WarningTextSubject.text = "Subject Cannot Be Empty";
            }

            if (string.IsNullOrEmpty(Message.text))
            {
                WarningTextMessage.text = "Message Cannot Be Empty";
            }
        }
    }
}
