
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    public InputActionAsset InputActions;
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction attackAction;

    [SerializeField] GameObject[] lasers;

    [SerializeField] float speedFactor = 12f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 10f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;

    [SerializeField] float smoothInputSpeed = .1f;

    Vector2 currentInputVector;
    Vector2 smoothInputVelocity;



    float xThrow, yThrow;

    void Start()
    {

    }

    void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
    }
    void Update()
    {
        SmoothController();
        MovePlayer();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessRotation()

    {

        float pitch = (transform.localPosition.y * positionPitchFactor) + (yThrow * controlPitchFactor);

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void MovePlayer()
    {

        float xPos = (xThrow * Time.deltaTime * speedFactor) + transform.localPosition.x;
        float clampedXPos = Mathf.Clamp(xPos, -xRange, xRange);

        float yPos = (yThrow * Time.deltaTime * speedFactor) + transform.localPosition.y;
        float clampedYPos = Mathf.Clamp(yPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, 0);

    }


    void SmoothController()
    {
        Vector2 throw_ = moveAction.ReadValue<Vector2>();
        currentInputVector = Vector2.SmoothDamp(currentInputVector, throw_, ref smoothInputVelocity, smoothInputSpeed);
        xThrow = currentInputVector.x;
        yThrow = currentInputVector.y;
    }

    void ProcessFiring()
    {
        if (attackAction.IsPressed())
        {
            SetActivateLasers(true);
        }
        else
        {
            SetActivateLasers(false);
        }


    }
    void SetActivateLasers(bool isActive )
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
   

}
