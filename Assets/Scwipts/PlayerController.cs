using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector2 move;
    public Rigidbody rb;
    public float stopTime;
    public delegate void OnPlayerEnteredArea();
    public static event OnPlayerEnteredArea PlayerEnteredArea;
    public GameObject FManager;
    //Transform playerTransform; // assign the player's Transform component
    //Transform objectTransform; // assign the object's Transform component


    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    // i have to grab the script from fishinspot to check if I am okay to press E to fish (in the trigger)

    

    void FixedUpdate()
    {
        
        movePlayer();
      
    }

    public void movePlayer()
    {
        if (move.sqrMagnitude > 0.1f)
        {
           Vector3 movement = new Vector3(move.x, 0f, move.y);

            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            rb.velocity = new Vector3(movement.x, 0, movement.z) * speed;
           
        }
        else
        {
             ///Debug.Log("Im at 0");
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, stopTime);
        }
        if (FManager.GetComponent<FeeshManager>().LookAtFish)
        {
            //Look at the defined FishinSpot's X
            Vector3 direction = FManager.GetComponent<FeeshManager>().TurnToFish.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = rotation;
            FManager.GetComponent<FeeshManager>().LookAtFish = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Feesh"))
        {
            //int areaIndex = other.GetComponent<Feesh>().index;
            PlayerEnteredArea?.Invoke();
        }
    }
}
