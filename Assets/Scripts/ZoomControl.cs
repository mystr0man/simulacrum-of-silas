using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomControl : MonoBehaviour
{

   public InputActionAsset _action;

   public InputActionAsset action {
    get => _action;
    set => _action = value;
   }

   protected InputAction scrollAction {get; set; }

   public Camera cam;

   public float min;
   public float max;

   private bool scrollAllowed;

   public bool cutsceneGoing;
   public float oldFOV;

   //Establish controls on game start
    private void Awake(){
        scrollAction = action.FindAction("Zoom");
        if (scrollAction != null) {
            scrollAction.started += OnScroll;
            scrollAction.performed += OnScroll;
            scrollAction.canceled += OnScroll;
        }
        
        cutsceneGoing = false;
    }
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    protected virtual void OnScroll(InputAction.CallbackContext context){
    //when you start scrolling, you can zoom
    if (context.started || context.performed) { 
        scrollAllowed = true;
    //when you stop, you can't
    } else if (context.canceled) {
        scrollAllowed = false;
    }
   }

   protected virtual float GetScroll() {
    if (scrollAllowed) {
        return (scrollAction.ReadValue<Vector2>().y);
    } else {
        return Vector2.zero.y;
    }
   }

    // Update is called once per frame
    private void Update()
    {
        if (!cutsceneGoing) {
            float scrollAmount = GetScroll();

            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - scrollAmount * 0.1f, min, max);
        }
    }
}
