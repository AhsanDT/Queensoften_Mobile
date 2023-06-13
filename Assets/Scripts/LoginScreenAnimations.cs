using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreenAnimations : MonoBehaviour
{
    public float Speed = 1f;
    public float MaxOffsetValue = 0.1f;
    public float MinoffsetValue = -0.1f;
    public Material Planemat;

    private float Offset;
    private bool MoveForward = true;
    private bool TurnRight = true;
    private void OnEnable()
    {
        TurnRight = true;
        MoveForward = true;
    }
    // Update is called once per frame
    void Update()
    {


        if(TurnRight)
        {
            if((Mathf.Round(Offset*100)/100f) == (Mathf.Round(MaxOffsetValue*100)/100f))
            {
                TurnRight = false;
            }
            MoveForward = true;
          
        }
        else
        {
            MoveForward = false;

            if ((Mathf.Round(Offset * 100) / 100f) == (Mathf.Round(MinoffsetValue * 100) / 100f))
            {
                TurnRight = true;
            }


        }
        if(MoveForward)
        {
            Offset += (Time.deltaTime * Speed) / 10f;
            Planemat.SetTextureOffset("_MainTex", new Vector2(Offset, 0f));
        }
        else
        {
            Offset -= (Time.deltaTime * Speed) / 10f;
            Planemat.SetTextureOffset("_MainTex", new Vector2(Offset, 0f));
        }
      
    }
}
