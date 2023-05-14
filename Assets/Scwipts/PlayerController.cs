using UnityEngine;
using UnityEngine.InputSystem;


namespace Born2Fish
{
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
        private float catchWindow = 1.5f;
        private float catchTrigger = 0.0f;
        private PlayerSM pStateM;

        private Camera _camera;
        private Vector2 _mouseDelta;
        public float CameraSpeed = 1.0f;
        public float CameraSmoothing = 1.0f;
        private Vector3 _cameraVelocity = Vector3.zero;

        private Transform _characterTransform => rb.transform;
        private Transform _playerTransform => transform;
        private GameObject _cameraController;

        private void Awake()
        {
            pStateM = GetComponent<PlayerSM>();
            pStateM.FManagerRef = FManager;
            pStateM.Init();
            biteWindow = Random.Range(3, 6);

            _camera = GetComponentInChildren<Camera>();
            _cameraController = GameObject.Find("CameraController"); //TODO: this should be smarter :)
        }

        public void DetectMouseClicks()
        {
            if (Mouse.current.leftButton.isPressed)
            {
                _mouseDelta = Mouse.current.delta.ReadValue();
                Debug.Log("_mouseDelta = " + _mouseDelta);
                // Do stuff for mouse rotation
            }
            else
            {
                // TODO: make this a slow stop
                _mouseDelta = Vector2.zero;
            }
        }

        // Hooked up to UI system!
        public void OnMove(InputAction.CallbackContext context)
        {
            move = context.ReadValue<Vector2>();
        }

        protected void FixedUpdate()
        {
            if (!isFishin)
                movePlayer();

            // I will need to check if the player presses E again to stop fishing, as well as implement a catch timer to allow for input to be pressed that catches the fish.
            // will also need to stop fishing if the catch window is missed

            //if (FManager.LookAtFish)
            //{
            //    if (Input.GetKeyDown(KeyCode.E))
            //    {
            //        FManager.LookAtFish = false;
            //        isFishin = false;
            //        FishinPole.enabled = false;
            //    }
            //    else
            //    {
            //        isFishin = true;
            //        //Debug.Log("Look is true");
            //        triggerTime += Time.deltaTime;
            //        int triggerTimeInt = (int)triggerTime;
            //        //Debug.Log(triggerTimeInt);
            //        move = Vector2.zero;

            //        if (triggerTimeInt == biteWindow)
            //        {
            //            //have to check for catch window here, and reset biteWindow on each state (caught or not caught)
            //            triggerTimeInt = (int)biteWindow;
            //            Debug.Log("catchTime = " + triggerTimeInt);
            //            //FManager.LookAtFish = false;
            //            FishAlert.SetActive(true);
            //            catchTrigger += Time.deltaTime;
            //            int catchTriggerInt = (int)catchTrigger;
            //            Debug.Log(catchTriggerInt);


            //            if (catchTriggerInt <= catchWindow && Input.GetKeyDown(KeyCode.E))
            //            {
            //                FishAlert.SetActive(false);
            //                Debug.Log("you caught the mfer");
            //            }
            //            biteWindow = Random.Range(3, 6);

            //        }



            //        }
            //    }

            DetectMouseClicks();
            rotateCamera();

            _cameraController.transform.position = Vector3.Lerp(_cameraController.transform.position, rb.position, CameraSmoothing);
        }

        private void rotateCamera()
        {
            if (_mouseDelta.x != 0.0f)
            {
                _cameraController.transform.RotateAround(_characterTransform.position, Vector3.up, _mouseDelta.x * CameraSpeed);
            }
        }

        protected void movePlayer()
        {
            if (move.sqrMagnitude > 0.1f)
            {
                Vector3 movementDirection =
                    (_camera.transform.forward.normalized * move.y + _camera.transform.right.normalized * move.x).normalized;
                Vector3 movement = movementDirection * speed;

                rb.velocity = movement;
            }
            else
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, stopTime);
            }

            if (FManager.LookAtFish)
            {
                //Look at the defined FishinSpot's X
                Vector3 turnToFishSanitized = new Vector3(FManager.TurnToFish.position.x, FManager.TurnToFish.position.y, _characterTransform.position.z);

                Vector3 direction = turnToFishSanitized - _characterTransform.position;
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
                _characterTransform.rotation = rotation;
                FManager.GetComponent<FeeshManager>().LookAtFish = false;
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

}
