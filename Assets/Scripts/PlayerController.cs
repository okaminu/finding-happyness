using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float MovementSpeed = 1;
    public float Gravity = 9.8f;
    public GameObject cinematicObject1;
    public GameObject cinematicObject2;
    public GameObject cinematicObject3;
    private float velocity = 0;
    private Camera cam;
    private bool finishedCinematic1 = false;
    private bool finishedCinematic2 = false;
    private bool finishedCinematic3 = false;

    // horizontal rotation speed
    public float horizontalSpeed = 1f;
    // vertical rotation speed
    public float verticalSpeed = 1f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        if (Vector3.Distance(cinematicObject1.transform.position, transform.position) <= 4 && !finishedCinematic1)
        {
            playCinematics1();
        }
        else if (Vector3.Distance(cinematicObject2.transform.position, transform.position) <= 3 && !finishedCinematic2)
        {
            playCinematics2();
        }
        else if (Vector3.Distance(cinematicObject3.transform.position, transform.position) <= 4 && !finishedCinematic3)
        {
            playCinematics3();
        }
        else
        {
            enableMovement();
            enableFreelook();
        }
        enableGravity();


    }

    void playCinematics1()
    {
        Quaternion finalRotation = Quaternion.LookRotation(cinematicObject1.transform.position - transform.position);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, finalRotation, 1 * Time.deltaTime);
        if (absolute(finalRotation) == absolute(cam.transform.rotation))
        {
            finishedCinematic1 = true;
        }
    }
    void playCinematics2()
    {
        Quaternion finalRotation = Quaternion.LookRotation(cinematicObject2.transform.position - transform.position);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, finalRotation, 1 * Time.deltaTime);
        if (absolute(finalRotation) == absolute(cam.transform.rotation))
        {
            finishedCinematic2 = true;
        }
    }

    Quaternion absolute(Quaternion a)
    {
        return new Quaternion(Mathf.Abs(a.x), Mathf.Abs(a.y), Mathf.Abs(a.z), Mathf.Abs(a.w));
    }

    void playCinematics3()
    {
        Quaternion finalRotation = Quaternion.LookRotation(cinematicObject3.transform.position - transform.position);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, finalRotation, 1 * Time.deltaTime);
        if (absolute(finalRotation) == absolute(cam.transform.rotation))
        {
            finishedCinematic3 = true;
        }
    }

    void enableFreelook()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

        yRotation += mouseX;
        xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90, 90);

        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }

    void enableMovement()
    {
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        characterController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);
    }

    void enableGravity()
    {
        // Gravity
        if (characterController.isGrounded)
        {
            velocity = 0;
        }
        else
        {
            velocity -= Gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, velocity, 0));
        }
    }
}