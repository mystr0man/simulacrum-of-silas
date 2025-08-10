using UnityEngine;
using UnityEngine.InputSystem;
using System; //for System.Math

public class DragRotate : MonoBehaviour
{

    public InputActionAsset _action;

    public InputActionAsset action {
        get => _action;
        set => _action = value;
    }

    protected InputAction leftClickPressedInputAction {get; set; }

    protected InputAction mouseLookInputAction {get; set; }

    private bool rotateAllowed;

    public bool turnToRotate = false; 

    private Camera newCamera;

    private bool nextCued = false;

    public PinController pin;
    public PanelController panel;
    
    private float totalRotated = 0;

    public string type; 

    public float speed = 1000f;

     // audio source and clip to be used for sound playing
    private AudioSource audioSource;
    public AudioClip audioClip;
    
    public RotateObject box;

    private void OnLeftClickPressed(InputAction.CallbackContext context) {
        if (nextCued) {
            return;
        }
        
        if (context.started || context.performed) {
            // Raycast to check if clicked object
            Ray clickRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit clickHit;
            if (Physics.Raycast(clickRay, out clickHit)) { 
                if (clickHit.transform == transform) {
                    rotateAllowed = true;
                }
            }
        } else if (context.canceled) {
            rotateAllowed = false;
        }
    }

    //Establish controls on game start
    private void Awake()
    {
        turnToRotate = false;
        leftClickPressedInputAction = action.FindAction("ButtonPress");
        mouseLookInputAction = action.FindAction("MouseTrack");

        if (leftClickPressedInputAction != null) {
            leftClickPressedInputAction.started += OnLeftClickPressed;
            leftClickPressedInputAction.performed += OnLeftClickPressed;
            leftClickPressedInputAction.canceled += OnLeftClickPressed;
        }
        
        action.Enable();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

    }

    //Get the 2D look coordinates of the mouse, to be used to control rotation
    protected virtual Vector2 GetMouseLookInput() {
        if(mouseLookInputAction != null) {
            return mouseLookInputAction.ReadValue<Vector2>();
        } else {
            return Vector2.zero;
        }
    }

    private void Update() {
        if(!rotateAllowed || !turnToRotate) {
            return;
        }
        Vector2 mouseDelta = GetMouseLookInput();

        if (rotateAllowed && mouseDelta.x > 0f) {
            if (type == "face") Debug.Log("Face trying to rotate");
            //if (type == "foot") {Debug.Log("Foot trying to rotate");}
            float rotationAmount = mouseDelta.x * speed * Time.deltaTime;
            if (type == "face") {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - rotationAmount, transform.localEulerAngles.y, transform.localEulerAngles.z);
            } else {
                transform.Rotate(Vector3.forward, -rotationAmount, Space.Self);
            }
            totalRotated += rotationAmount;
        }

        if (type == "gear" && transform.localEulerAngles.z <= 1f && transform.localEulerAngles.z > 0f || totalRotated >= 360f && !nextCued) {
            if (pin != null){
                //TODO: DO PIN THING
                pin.TogglePin();
            }

            rotateAllowed = false;
            nextCued = true;
            //TODO: PLAY SOUND
            audioSource.PlayOneShot(audioClip);
            return;
        }

        if (turnToRotate && type == "foot" && Math.Abs(totalRotated) >= 190f && !nextCued) {
            //TODO: OPEN TOP HATCH
            if (panel != null) {
                panel.TogglePanel();
            }
            rotateAllowed = false;
            nextCued = true;
            audioSource.PlayOneShot(audioClip);
            return;
        }

        if (turnToRotate && type == "key" && Math.Abs(totalRotated) >= 90f && !nextCued) {
            //TODO: OPEN TOP HATCH
            if (panel != null) {
                panel.TogglePanel();
            }
            rotateAllowed = false;
            nextCued = true;
            audioSource.PlayOneShot(audioClip);
            box.resetRotation = true;
            return;
        }

        if (turnToRotate && type == "face" && Math.Abs(totalRotated) >= 28f && !nextCued) {
            rotateAllowed = false;
            nextCued = true;
            audioSource.PlayOneShot(audioClip);
            return;
        }
    }

    public void FootTurn() {
        if (type == "foot") {
            transform.Rotate(0f, 0f, -10f, Space.Self);
            rotateAllowed = true;
            turnToRotate = true;
        }
    }
}