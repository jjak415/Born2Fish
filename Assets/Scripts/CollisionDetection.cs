using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionDetection : MonoBehaviour
{
    public GameObject parent;
    private void OnCollisionStay(Collision collision)
    {
        // if (collision.gameObject.CompareTag("Barrier"))
        //{
        Debug.Log("I collided");

        parent.GetComponent<PlayerController>().move = Vector3.zero;

        //}
    }

}
