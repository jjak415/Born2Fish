using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Born2Fish
{
    public class FeeshManager : MonoBehaviour
    {
        public bool LookAtFish;
        [SerializeField]
        public List<Feesh> FSpots = new List<Feesh>();
        public Transform TurnToFish;
        //*TO DO*
        // make sure to implement the finding of children objects
        //separate Feesh and FeeshSpots. Feesh will be what active the Spots so that the player can walk up and interact.
        // Use Spots as static areas and have the player rotate to face the direction of the Feesh that create the FeeshSpot
        // Use an animation to turn off the fishing pole after you catch a fish or fail to catch a fish


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
            
        }
    }
}

