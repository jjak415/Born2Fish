using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaTest : MonoBehaviour
{
    [SerializeField]
    private int height = 4;
    [SerializeField]
    float apex = 0.5f;

    [SerializeField]
    private float speed = 4;
    float gravity;
    float jumpVelocity;

    Vector3 velocity;

    public bool collided;

    private void Start()
    {
        gravity = -(2 * height) / Mathf.Pow(apex, 2);
        jumpVelocity = Mathf.Abs(gravity) * apex;
        print("Gravity: " + gravity + "Jump : " + jumpVelocity);

        
    }

    void Update()
    {
        Vector2 buttons = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(!collided)
        velocity.y += gravity * Time.deltaTime;

       // if (Input.GetKey(KeyCode.D))
            velocity.x = buttons.x * speed;

        //if (Input.GetKey(KeyCode.A))
           //velocity.x = -buttons.x * speed;
        
        if (collided)
            velocity.y = 0;
        if(Input.GetKeyDown(KeyCode.Space) && collided)
        {
            velocity.y = jumpVelocity;
        }

        Movement(velocity * Time.deltaTime);

    }
    public void Movement(Vector3 velocity)
    {
        transform.Translate(velocity);
    }
    private void OnCollisionEnter(Collision collision)
    {
        collided = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        collided = false;
    }
}
