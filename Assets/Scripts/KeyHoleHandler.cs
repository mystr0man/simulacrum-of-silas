using UnityEngine;
using UnityEngine.InputSystem;

public class KeyHoleHandler : MonoBehaviour
{

    public InputActionAsset _action;

    public InputActionAsset action {
        get => _action;
        set => _action = value;
    }

    protected InputAction clickAction {get; set; }

    public bool readyToClick = false;
    private bool clickedYet = false;

    // audio source and clip to be used for sound playing
    private AudioSource audioSource;
    public AudioClip audioClip;

    public KeyController oldKey;
    public DragRotate newKey;

    protected virtual void OnLeftClickPressed(InputAction.CallbackContext context) {
        if (readyToClick && !clickedYet) {
            Ray clickRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit clickHit;
            if (Physics.Raycast(clickRay, out clickHit)) {
                if (clickHit.transform == transform && (context.started || context.performed)) { 
                    clickedYet = true;
                    oldKey.GetComponent<Renderer>().enabled = false;
                    newKey.GetComponent<Renderer>().enabled = true;
                    newKey.turnToRotate = true;
                    audioSource.PlayOneShot(audioClip);
                }
            }
        }
    }

    void Awake() {
        clickAction = action.FindAction("ButtonPress");
        if (clickAction != null) {
            clickAction.started += OnLeftClickPressed;
            clickAction.performed += OnLeftClickPressed;
            clickAction.canceled += OnLeftClickPressed;
        }

        
        //sourced from https://docs.unity3d.com/6000.1/Documentation/ScriptReference/AudioSource-playOnAwake.html 
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
