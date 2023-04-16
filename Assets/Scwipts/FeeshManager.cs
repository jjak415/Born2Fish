using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeeshManager : MonoBehaviour
{
    [SerializeField]
    private List<Feesh> FSpots = new List<Feesh>();

    //*TO DO*
    // make sure to implement the finding of children objects
    //separate Feesh and FeeshSpots. Feesh will be what active the Spots so that the player can walk up and interact.
    // Use Spots as static areas and have the player rotate to face the direction of the Feesh that create the FeeshSpot

    private void Awake()
    {
        FSpots.Clear();


        Feesh[] fishinSpot = GameObject.FindObjectsOfType<Feesh>();
        for (int i = 0; i < fishinSpot.Length; i++)
        {
            FSpots.Add(fishinSpot[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Feesh spot in FSpots)
        {
            if (spot.InRange)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Caught ya a big'un!");
                }
            }
        }
    }
}
