using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// code for fishing spots

public class Feesh : MonoBehaviour
{
    public bool  inRange;
    public Text feesh;
    public int index;



    private void Awake()
    {

    }
    private void Start()
    {
        PlayerController.PlayerEnteredArea += OnPlayerEnteredArea;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            feesh.enabled = true;
            Debug.Log("Rdy 2 Fish :)");
        
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            feesh.enabled = false;
            Debug.Log("No Feesh :(");

        }
    }

    void OnPlayerEnteredArea(int areaIndex)
    {
        Debug.Log("He be feeshin at " + areaIndex);
    }

}
