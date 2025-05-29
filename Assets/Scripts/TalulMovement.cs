using UnityEngine;

public class TalulMovement : MonoBehaviour
{
    //rigidbody of main character
    private Rigidbody rigidB;

    //movement along x and z axes
    private float movementX;
    private float movementZ;

    //speed of movement
    [SerializeField] private float speed;
    //speed of rotation - cause i want the player to also rotate when moving towards a direction
    [SerializeField] private float rotationSpeed;

    //to play animations
    private Animator anim;

    private bool gamePaused;
    [SerializeField] GameObject pauseScreen;
    //stop the bg music and cat sounds
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource[] catSounds;

    void Start()
    {
        rigidB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        gamePaused = false;

        //setting time back to 1 after reloading the scene or loading next scene etc 
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(gamePaused && Time.timeScale == 0f)
            {
                Time.timeScale = 1f; //unpause game
                pauseScreen.SetActive(false);
                pauseScreen.transform.parent.gameObject.SetActive(false);
                bgMusic.UnPause();
                foreach (AudioSource cat in catSounds)
                {
                    cat.UnPause();
                }
                gamePaused = !gamePaused;
            }
            else if(!gamePaused && Time.timeScale == 1f)
            {
                Time.timeScale = 0f; //pause game
                pauseScreen.SetActive(true);
                pauseScreen.transform.parent.gameObject.SetActive(true);
                bgMusic.Pause();
                foreach (AudioSource cat in catSounds)
                {
                    cat.Pause();
                }
                gamePaused = !gamePaused;
            }
        }
    }

    //for stuff that need physics -> changing the velocity of the rigidbody directly (it will conflict with any other type of physics/force so remember that)
    void FixedUpdate()
    {
        //when input is detected(arrow keys) - gets player input(input system) - values between -1 and 1
        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");

        /* getting the follow camera's normalized directional vectors (local space), these are the directions in which i want 
        the character to move and not in the world space (which is always the same) cause it messes the movement keys when rotation happens
        btw these values are normalized & in world space */
        Vector3 forward = Camera.main.transform.forward; //forward->z
        Vector3 right = Camera.main.transform.right; //right->x

        //also cancel out any rotation on y axis cause it messes things up (player tilts) - ignore upward and downward camera angles
        forward.y = 0;
        right.y = 0;

        //removing y -> not normalized anymore (normalized vectors = unit vectors = have a length/magnitude of 1)

        //so normalize again these values cause player seems to be slowed down at times - messes with the speed
        forward = forward.normalized;
        right = right.normalized; 

        //direction-relative input vectors - changing the basic coordinates to be the camera ones by multiplying the player input with the cam rotation
        Vector3 rightRelativeVerticalInput = movementX * right;
        Vector3 forwardRelativeVerticalInput = movementZ * forward;

        //create final 3d movement vector for camera-relative movement - addition of these values rotates the original players input movement vector (value in world space)
        Vector3 movement = rightRelativeVerticalInput + forwardRelativeVerticalInput;

        //move&rotate if theres movement - and signal to play the animation
        if (movement != Vector3.zero)
        {
            anim.SetBool("isWalking", true);
            //move the player by changing the rgbody's velocity directly - using the movements from the input system (moves relative to world space)
            rigidB.linearVelocity = speed * movement;
            //for smooth rotation (towards the y axis = up)
            Quaternion rotate = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, rotationSpeed);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }   
}
