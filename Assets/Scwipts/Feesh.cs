using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// code for fishing spots

public class Feesh : MonoBehaviour
{
   // public bool  inRange;
    public Text Good2Fish;
    public bool InRange;


    private void Start()
    {
        PlayerController.PlayerEnteredArea += OnPlayerEnteredArea;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InRange = true;
            Good2Fish.enabled = true;
            //Debug.Log("Rdy 2 Fish :)");
        
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InRange = false;
            Good2Fish.enabled = false;
            //Debug.Log("No Feesh :(");

        }
    }

    void OnPlayerEnteredArea()
    {
        //Debug.Log("He be feeshin at " + areaIndex);
    }

}
