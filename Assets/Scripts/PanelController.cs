using UnityEngine;

public class PanelController : MonoBehaviour
{

    private bool opened = false;

    public DragRotate gear;

    public KeyHoleHandler keyhole;

    public FaceButtonHandler faceButton;

    public int panelNum = 1; //TODO: GENERALIZE so i can control panel-to-panel
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePanel()
    {
        if (!opened) {
            opened = true;
            if (panelNum == 1) {
                transform.localPosition = new Vector3(-0.01488f, 0f, -0.01003f);
                transform.Rotate(0f, -126.677f, 0f, Space.Self);
                //TODO: add enabling of gear
                gear.turnToRotate = true;
            } else if (panelNum == 2) {
                //TODO: CHANGE, just needs specifying
                transform.localPosition = new Vector3(-0.00512f, 0f, 0.0131f);
                transform.Rotate(0f, -126.677f, 0f, Space.Self);
                if (keyhole != null) {
                    keyhole.readyToClick = true;
                }
            } else if (panelNum == 3) {
                transform.localEulerAngles = new Vector3(0f, 90f, 0f);
                transform.localPosition = new Vector3(0.00791f, 0f, 0.01343f);
                faceButton.readyToClick = true;
            }
        }

    }
}
