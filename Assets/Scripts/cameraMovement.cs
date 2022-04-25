using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class cameraMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform handsTransform;
    public Transform orientation;
    public GameObject camholder;
    public CharacterController characterController;

    public FPSShooter fpsShooter;
    public Reticle reticle;

    public float cameraSensitivity;
    public float cameraSpeed;
    public float moveSpeed;
    public float moveInputDeadZone;
    public float moveInputSprintZone;
    private float timeToFire = 0f;
    private float fireRate = 4f;

    public Slider camspeedSlider;
    public Slider volumeSlider;


    // public Rigidbody rb;
    [Header("Gravity and Jumping")]
    public float gravity = 10;
    public float stickToGroundForce = 10;
    public float jumpForce = 10f;
    private float verticalVelocity;

    [Header("Ground check")]
    public Transform groundCheck;
    public LayerMask groundLayers;
    public float groundcheckradius;
    public bool jumping;
    public bool grounded;
    public bool isAlive = true;


    public int leftFingerID, rightFingerID, middleFingerID;
    float halfScreenWidth, oneThirdScreenWidth;

    public Vector2 lookInput;
    float cameraPitch;

    Vector2 moveTouchStartposition;
    public Vector2 moveInput;

    public Animator anim;

    public CameraBOB cameraBob;
    public PlayerInput playerInput;
    public Vector3 lastplayerPos = new Vector3(0,0,0);
    public Vector3 lastSwanPos = new Vector3(0, 0, 0);
    public AudioClip playerMove;
    public AudioClip playerJump;
    AudioSource audio;
    public AudioListener camaudioListner;

    bool run = false;

    // Start is called before the first frame update
    void Start()
    {
        string currentscene = SceneManager.GetActiveScene().ToString();
        audio = GetComponent<AudioSource>();
        leftFingerID = -1;
        rightFingerID = -1;
        middleFingerID = -1;
        halfScreenWidth = Screen.width / 2;
        oneThirdScreenWidth = Screen.width / 3;

        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
        //moveInputSprintZone = Screen.height * 0.4f;
        moveInputSprintZone = Mathf.Pow(Screen.height / 3, 2);
        cameraSpeed = camspeedSlider.value;
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        Move2();

        lookInput = playerInput.actions["Look"].ReadValue<Vector2>();
        LookAround2();

        if (isAlive) GetTouchInput();
        else return;

        if (rightFingerID != -1)
        {
            LookAround();
        }
        if (leftFingerID != -1)
        {
            Move2();
        }
        GravityEffect();

        
    }
    private void FixedUpdate()
    {
       // Debug.Log("Player Pos Last: "+ lastplayerPos);
        grounded = Physics.CheckSphere(groundCheck.position, groundcheckradius, groundLayers);
        if (grounded)
        {
           // Debug.Log(" Grounded ");
            lastplayerPos = gameObject.transform.position;
            StopCoroutine("JumpTimer");
        }
        else
        {
            StartCoroutine("JumpTimer");
          //  Debug.Log("Not Grounded ");
        }
    }

    void GetTouchInput()
    {

        for (int i = 0; i < Input.touchCount; i++)
        {
            //Touch t = Input.GetTouch(i);
            Touch t = Input.touches[i];
            

            switch (t.phase)
            {
                
                case UnityEngine.TouchPhase.Began:
                    if (t.position.x < oneThirdScreenWidth && leftFingerID == -1 && middleFingerID == -1)
                    {
                        
                        leftFingerID = t.fingerId;

                        moveTouchStartposition = t.position;
                    }
                    else if (t.position.x > 2 * oneThirdScreenWidth && rightFingerID == -1 && middleFingerID ==-1)
                    {
                        rightFingerID = t.fingerId;
                    }
                    else if (t.position.x > oneThirdScreenWidth && rightFingerID == -1 && leftFingerID ==-1)
                    {
                        middleFingerID = t.fingerId;
                    }
                    break;

                case UnityEngine.TouchPhase.Ended:
                case UnityEngine.TouchPhase.Canceled:
                    if (t.fingerId == leftFingerID)
                    {
                        leftFingerID = -1;
                    }
                    else if (t.fingerId == rightFingerID)
                    {
                        rightFingerID = -1;
                    }
                    else if (t.fingerId == middleFingerID)
                    {
                        middleFingerID = -1;
                    }
                    break;

                case UnityEngine.TouchPhase.Moved:
                    if (t.fingerId == rightFingerID)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerID)
                    {
                        moveInput = t.position - moveTouchStartposition;
                    }
                    break;

                case UnityEngine.TouchPhase.Stationary:
                    if (t.fingerId == rightFingerID)
                    {
                        lookInput = Vector2.zero;
                    }
                    else if (t.fingerId == middleFingerID && Time.time >= timeToFire)
                    {
                        timeToFire = Time.time + 1 / fireRate;
                        fpsShooter.Shoot();
                        reticle.SetMaxSize();
                    }
                    break;
            }
        }
    }

    public void ShootfunctionCAM()
    {
        timeToFire = Time.time + 1 / fireRate;
        fpsShooter.Shoot();
        reticle.SetMaxSize();
    }
    void LookAround2()
    {
        lookInput = -lookInput.normalized * cameraSpeed * Time.deltaTime;
       //vertical rotation
       cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -45f, 45f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        handsTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        //horizontal rotation
        transform.Rotate(transform.up, lookInput.x);
    }
    void LookAround()
    {
        //vertical rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -45f, 45f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        handsTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        //horizontal rotation
        transform.Rotate(transform.up, lookInput.x);
    }
    void Move2()
    {
            if (moveInput.sqrMagnitude <= 0.05f)
            {
            Idle();
                return;
                audio.clip = null;
            }
            if (moveInput.sqrMagnitude > 0.95f)
            {
            
                run = true;
                Walk();
                cameraBob.isRunning = true;
                cameraBob.isWalking = true;
            }
            else
            {
                run = false;
                Walk();
            
                cameraBob.isWalking = true;
                cameraBob.isRunning = false;
            }
    }
    void Move()
    {
       
        if (moveInput.sqrMagnitude <= moveInputDeadZone)
        {
            return;
            audio.clip = null;
        }

        if (moveInput.sqrMagnitude > moveInputSprintZone)
        {
           // Run();
           // cameraBob.isRunning = true;
           // cameraBob.isWalking = true;
        }
        else
        {
            Walk();
            cameraBob.isWalking = true;
            cameraBob.isRunning = false;
        }
    }

    private void GravityEffect()
    {
         if (grounded && verticalVelocity <= 0) verticalVelocity = -stickToGroundForce * Time.deltaTime;
         else verticalVelocity -= gravity * Time.deltaTime;

         Vector3 verticalMovement = transform.up * verticalVelocity;
         characterController.Move(verticalMovement * Time.deltaTime);
      
    }

    private void Walk()
    {
        audio.clip = playerMove;
        if(audio.isPlaying != true) audio.Play();
        if (run)
        {
            Vector2 movementDirection = moveInput.normalized * moveSpeed * 1.5f* Time.deltaTime;
            characterController.Move(transform.right * movementDirection.x + transform.forward * 1.5f * movementDirection.y);
        }
        else
        {
            Vector2 movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime;
            characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
        }
       

       // rb.AddForce(transform.right * movementDirection.x + transform.forward * movementDirection.y);
        //rb.MovePosition(transform.right * movementDirection.x + transform.forward * movementDirection.y);
        //rb.velocity = transform.right * movementDirection.x + transform.forward * movementDirection.y;
        //walking animation
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    public void Run()
    {
        cameraBob.isRunning = true;
        cameraBob.isWalking = true;
        Vector2 movementDirection = moveInput.normalized * moveSpeed *1.5f * Time.deltaTime;
        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);

        //Jump();
    }
    private void Idle()
    {
        //rb.velocity = new Vector3(0,0,0);
        cameraBob.isRunning = false;
        cameraBob.isWalking = false;
        //Idle animation
        audio.clip = null;
        anim.SetFloat("Speed", 0,0.1f, Time.deltaTime);
    }
    private void Attack()
    {
        // Call Attack from button pressing

        //Idle animation
        anim.SetTrigger("Attack");
    }
    public void Jump()
    {
        anim.SetFloat("Speed", 0.99f, 0.1f, Time.deltaTime);
        if (grounded)
        {
            audio.clip = null;
            audio.PlayOneShot(playerJump);
            verticalVelocity = jumpForce;
        }
        
    }
    IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(5);
        if (!grounded)
        {
            gameObject.transform.position = lastplayerPos;
            
        }
    }
    public void CameraSpeedSlider()
    {
        cameraSpeed = camspeedSlider.value; 
    }
    public void VolumeSlider()
    {
        //camholder.GetComponent<AudioSource>().volume = volumeSlider.value;
        //camaudioListner.volume = volumeSlider.value;
        camaudioListner.GetComponent<Vol>().ChangeVol(volumeSlider.value);
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("HomeMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public static bool IsPointerOverGameObject()
    {

        // Check touches
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            // Check if finger is over a UI element
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("Touched the UI");
            }
        }


        return false;
    }

}
