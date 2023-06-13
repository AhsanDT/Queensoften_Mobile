using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUIScale : MonoBehaviour
{ 

    public bool _enabled;
    public bool debugMode;

    public GameObject objectToScale;
    public enum ScaleType { BottomLeft, TopRight };

    public ScaleType scaleType;


    public Transform referenceObjectTransform;



    private void Update()
    {
        if (debugMode)
        {
            Debug.LogError(this.transform.position); 
        }

        if (_enabled == false)
            return;


        if (scaleType == ScaleType.BottomLeft)
        {

            if (this.transform.position.x > referenceObjectTransform.position.x && this.transform.position.y > referenceObjectTransform.position.y)
            {
                objectToScale.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            }
            else if (this.transform.position.x > referenceObjectTransform.position.x && this.transform.position.y < referenceObjectTransform.position.y)
            {
                objectToScale.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            else if (this.transform.position.x < referenceObjectTransform.position.x && this.transform.position.y > referenceObjectTransform.position.y)
            {
                objectToScale.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            else if (this.transform.position.x < referenceObjectTransform.position.x && this.transform.position.y < referenceObjectTransform.position.y)
            {
                objectToScale.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
        }
        else if (scaleType == ScaleType.TopRight)
        {

            if (this.transform.position.x > referenceObjectTransform.position.x && this.transform.position.y > referenceObjectTransform.position.y)
            {
                objectToScale.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            else if (this.transform.position.x > referenceObjectTransform.position.x && this.transform.position.y < referenceObjectTransform.position.y)
            {
                objectToScale.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            else if (this.transform.position.x < referenceObjectTransform.position.x && this.transform.position.y > referenceObjectTransform.position.y)
            {
                objectToScale.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            else if (this.transform.position.x < referenceObjectTransform.position.x && this.transform.position.y < referenceObjectTransform.position.y)
            {
                objectToScale.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            }
        }

    }




}
