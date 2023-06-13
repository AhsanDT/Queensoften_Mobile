using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreGamesButtonHandler : MonoBehaviour
{
    Button _button;
    public string MoreGamesLink;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RateUsButtonPressed);
    }
    public void RateUsButtonPressed()
    {
        Application.OpenURL(MoreGamesLink);
    }
}
