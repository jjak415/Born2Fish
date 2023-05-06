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
    public FeeshManager FManager;
    public MeshRenderer FishinPole;
    public GameObject FishAlert;
    private float biteWindow = 0.0f;
    private float triggerTime = 0.0f;
    private bool isFishin;
    private float catchWindow = 0.0f;
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    // i have to grab the script from fishinspot to check if I am okay to press E to fish (in the trigger)

    
    private void Start()
    {
       biteWindow = Random.Range(3, 6);
       
        //Debug.Log(biteWindow);
    }

    void FixedUpdate()
    {
        if(!isFishin)
        movePlayer();
       
        // I will need to check if the player presses E again to stop fishing, as well as implement a catch timer to allow for input to be pressed that catches the fish.
        // will also need to stop fishing if the catch window is missed

        if (FManager.LookAtFish)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FManager.LookAtFish = false;
                isFishin = false;
            }
            isFishin = true;
            //Debug.Log("Look is true");
            triggerTime += Time.deltaTime;
            int triggerTimeInt = (int)triggerTime;
            //Debug.Log(triggerTimeInt);
            move = Vector2.zero;

            if(triggerTimeInt == biteWindow)
            {
                //FManager.LookAtFish = false;
                FishAlert.SetActive(true); 
                biteWindow = Random.Range(3, 6);

            }
        }
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
        if (FManager.LookAtFish)
        {

            //Look at the defined FishinSpot's X
            Vector3 turnToFishSanitized = new Vector3(FManager.TurnToFish.position.x, FManager.TurnToFish.position.y, transform.position.z);
            Vector3 direction = turnToFishSanitized - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = rotation;
           // FManager.GetComponent<FeeshManager>().LookAtFish = false;
            FishinPole.enabled = true;
           
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
