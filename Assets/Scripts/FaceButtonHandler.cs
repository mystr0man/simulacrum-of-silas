using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class FaceButtonHandler : MonoBehaviour
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

    public FacePortionHandler oldFace;
    public FacePortionHandler newFace;

    public RotateObject box;

    protected virtual void OnLeftClickPressed(InputAction.CallbackContext context) {
        if (readyToClick && !clickedYet) {
            Ray clickRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit clickHit;
            if (Physics.Raycast(clickRay, out clickHit)) {
                if (clickHit.transform == transform && (context.started || context.performed)) { 
                    clickedYet = true;
                    oldFace.GetComponent<Renderer>().enabled = false;
                    newFace.GetComponent<Renderer>().enabled = true;
                    box.resetRotation = true;
                    audioSource.PlayOneShot(audioClip);
                    StartCoroutine(WaitForAudioThenQuit());
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

    private IEnumerator WaitForAudioThenQuit() { //credit to ChatGPT for implementaton
        // Play the clip if it's not already playing
        if (!audioSource.isPlaying)
            audioSource.Play();

        // Wait until the clip finishes
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Quit the application
        Application.Quit();

    #if UNITY_EDITOR
        // Stop play mode in the editor
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
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
