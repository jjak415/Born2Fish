using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Transform camT;
    public float smoothing = 1f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    public int speed; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


         if (target != null)
         {
         Vector3 targetPos = target.position + offset;

          transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothing);
         }

        if (Input.GetMouseButton(1))
        {
            camT.eulerAngles = camT.eulerAngles + new Vector3(0, Input.GetAxis("Mouse X") * speed, 0);
        }
    }


}

