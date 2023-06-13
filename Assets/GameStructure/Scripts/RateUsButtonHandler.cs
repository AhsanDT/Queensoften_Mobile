using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateUsButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RateUsButtonPressed);
    }
    public void RateUsButtonPressed()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }
}
