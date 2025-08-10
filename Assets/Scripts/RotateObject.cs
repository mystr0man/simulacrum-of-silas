using UnityEngine;
using UnityEngine.InputSystem;


public class RotateObject : MonoBehaviour
{
    // Sourced from: https://www.youtube.com/watch?v=CiZvMc8aI3U 


   public InputActionAsset _action;

   public InputActionAsset action {
    get => _action;
    set => _action = value;
   }

   protected InputAction rightClickPressedInputAction {get; set; }

   protected InputAction mouseLookInputAction {get; set; }

   private bool rotateAllowed;
   public bool resetRotation;
   public ZoomControl mainCam;
   public float speed;
   public int inverted; //note - can only be 1 or -1 to control inversion

   public ExtensionController extension;

   private bool cutsceneGoing;

   private Vector3 oldRotation;

   private int cutsceneCounter = 1;


    protected virtual void OnRightClickPressed(InputAction.CallbackContext context){
    //when you right click or hold it, you can rotate game object
    if (context.started || context.performed) { 
        rotateAllowed = true;
    //when you release right click, you can no longer rotate
    } else if (context.canceled) {
        rotateAllowed = false;
    }
   }

    //Establish controls on game start
    void Awake(){
    //camera = GetComponent<Camera>();
    rightClickPressedInputAction = action.FindAction("RotateLook");
    if (rightClickPressedInputAction != null) {
        rightClickPressedInputAction.started += OnRightClickPressed;
        rightClickPressedInputAction.performed += OnRightClickPressed;
        rightClickPressedInputAction.canceled += OnRightClickPressed;
    }

    mouseLookInputAction = action.FindAction("MouseTrack");

    action.Enable();

    resetRotation = false;

    cutsceneGoing = false;
   }

   //Get the 2D look coordinates of the mouse, to be used to control rotation
   protected virtual Vector2 GetMouseLookInput() {
    if(mouseLookInputAction != null) {
        return mouseLookInputAction.ReadValue<Vector2>();
    } else {
        return Vector2.zero;
    }
   }

   void Update() {
    if (resetRotation) {
        cutsceneGoing = true;
        /* bring back controlled rotation if there is time, otherwise just snap back
        float rotationRate = 0.1f;
        rotateAllowed = false;
        Vector3 angles = NormalizeAngles(transform.localEulerAngles);
        Vector3 target = new Vector3(-90f, 0f, 90f);
        Vector3 result = Vector3.zero;

        result.x = Mathf.MoveTowards(angles.x, target.x, rotationRate);
        result.y = Mathf.MoveTowards(angles.y, target.y, rotationRate);
        result.z = Mathf.MoveTowards(angles.z, target.z, rotationRate);

        transform.localEulerAngles = result;

        angles = NormalizeAngles(transform.localEulerAngles);

        //cleaned up implementation of latter logic, with help of ChatGPT
        float allowance = 10f;
        bool closeX = (Mathf.Abs(-90f - angles.x) <= allowance);
        bool closeY = (Mathf.Abs(0f - angles.y) <= allowance);
        bool closeZ = (Mathf.Abs(90f - angles.z) <= allowance);

        Debug.Log("CloseX: " + closeX);
        Debug.Log("CloseY: " + closeY);
        Debug.Log("CloseZ: " + closeZ);

        bool readyToProceed = ((closeX && closeY) || (closeX && closeZ) || (closeY && closeZ)); //<- now not working as inended for some reason
        */
        bool readyToProceed = true; //a band-aid change, will likely be either reverted or made more concrete


        oldRotation = transform.localEulerAngles;
        mainCam.oldFOV = mainCam.cam.fieldOfView;
        if (readyToProceed) {
            Vector3 angles = new Vector3(-105f, 0f, 90f);
            transform.localEulerAngles = angles;
            mainCam.cam.fieldOfView = 40;
            mainCam.cutsceneGoing = true;
            

            //TODO: REMOVE THIS, ADD PLAY ANIMATION FUNCTIONALITY
            resetRotation = false;
            //rotateAllowed = true;

            //REMAINING: 
            if (cutsceneCounter == 1) {
                extension.Extend();
                cutsceneCounter++;
            } else {
                
            }

            //cutsceneGoing needs to be made false again, as well as mainCam.cutsceneGoing

        } 
        
    }
    if(!rotateAllowed || cutsceneGoing) {
        return;
    }

    Vector2 mouseDelta = GetMouseLookInput();

    transform.Rotate(Vector3.up * -inverted, mouseDelta.x, Space.World);
    transform.Rotate(Vector3.right * inverted, mouseDelta.y, Space.World);
   }

   private Vector3 NormalizeAngles(Vector3 angles) { //because Euler angles exist 0-359, I need to normalize them to the (-180, 180) range Unity uses
     if (angles.x > 180f) {
        angles.x -= 360f;
     } if (angles.y > 180f) {
        angles.y -= 360f;
     } if (angles.z > 180f) {
        angles.z -= 360f;
     }
     return angles;

   }

   public void ReturnControl(){
    transform.localEulerAngles = oldRotation;
    mainCam.cam.fieldOfView = mainCam.oldFOV;
    cutsceneGoing = false;
    mainCam.cutsceneGoing = false;
   }

}