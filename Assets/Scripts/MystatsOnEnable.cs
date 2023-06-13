using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MystatsOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
      ApiCode.Instance.mystats.FreePlayButtonClicked();
    }
}
