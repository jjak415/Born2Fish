using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMgmt : MonoBehaviour
{
    public static WaveMgmt instance;

    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("GTFO");
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight (float _x)
    {
        
        float floatW = amplitude * Mathf.Sin(_x / length + offset);
        //Debug.Log(floatW);
        return floatW;
    }
}
