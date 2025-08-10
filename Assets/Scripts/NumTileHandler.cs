using UnityEngine;
using UnityEngine.InputSystem;

public class NumTileHandler : MonoBehaviour
{

    public NumPadHandler numpad;

    public int num;

    public bool moveUp = false;
    public bool moveRight = false;
    public bool moveDown = false;
    public bool moveLeft = false;

    public InputActionAsset _action;

    public InputActionAsset action {
        get => _action;
        set => _action = value;
    }

    protected InputAction clickAction {get; set; }

    public int boardSlotHolder;

    public int boardSlotHolderBeforeMove;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() {
        clickAction = action.FindAction("ButtonPress");
        if (clickAction != null) {
            clickAction.started += OnLeftClickPressed;
            clickAction.performed += OnLeftClickPressed;
            clickAction.canceled += OnLeftClickPressed;
        }

        //sourced from https://docs.unity3d.com/6000.1/Documentation/ScriptReference/AudioSource-playOnAwake.html 
        /*
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        */
    }

    protected virtual void OnLeftClickPressed(InputAction.CallbackContext context) {
        Ray clickRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit clickHit;
        if (Physics.Raycast(clickRay, out clickHit)) {
            if (clickHit.transform == transform && (context.started) && numpad.timeToPuzzle) { 
                Debug.Log("click on tile registered");
                //x axis is x, y axis is z
                if (moveUp) {  
                    boardSlotHolderBeforeMove = boardSlotHolder;
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1);
                    numpad.UpdateBoardState(this);
                } else if (moveRight) {
                    boardSlotHolderBeforeMove = boardSlotHolder;
                    transform.localPosition = new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
                    numpad.UpdateBoardState(this);
                } else if (moveDown) {
                    boardSlotHolderBeforeMove = boardSlotHolder;
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 1);
                    numpad.UpdateBoardState(this);
                } else if (moveLeft) {
                    boardSlotHolderBeforeMove = boardSlotHolder;
                    transform.localPosition = new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
                    numpad.UpdateBoardState(this);
                }
            }
        }
    }

}
