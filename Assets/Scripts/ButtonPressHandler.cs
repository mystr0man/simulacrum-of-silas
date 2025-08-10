using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonPressHandler : MonoBehaviour
{
    
    public InputActionAsset _action;

    public InputActionAsset action {
        get => _action;
        set => _action = value;
    }

    protected InputAction clickAction {get; set; }

    private bool clickedYet = false;

    // audio source and clip to be used for sound playing
    private AudioSource audioSource;
    public AudioClip audioClip;

    //TODO: add variable that holds what is to be broadcasted as the effect of this interactable's event
    public PanelController panel;
    

    protected virtual void OnLeftClickPressed(InputAction.CallbackContext context) {
        if (!clickedYet) {
            Ray clickRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit clickHit;
            if (Physics.Raycast(clickRay, out clickHit)) {
                if (clickHit.transform == transform && (context.started || context.performed)) { 
                    clickedYet = true;
                    FootButtonClickResult();

                    //TODO: add broadcasting of effect of being clicked
                    if (panel != null) {
                        panel.TogglePanel();
                    }
                }
            }
        }
    }

    private void FootButtonClickResult() {
        transform.position += transform.up * 0.03f;
        audioSource.PlayOneShot(audioClip);
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
