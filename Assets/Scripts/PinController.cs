using UnityEngine;

public class PinController : MonoBehaviour
{

    public Material originalSurface;
    public Material glowingSurface;
    private Renderer render;

    public PinHoleHandler pinHole;

    private bool popped = false;

    public bool invis = false;

    void Awake() {
        render = GetComponent<Renderer>();
        render.material = originalSurface;
        if (invis) {
            render.enabled = false;
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePin() {
        if (!popped) {
            popped = true;
            transform.position += transform.forward * -0.38f; //0.0064462f;
            //TODO: enable glowing and next step!
            render.material = glowingSurface;
            pinHole.readyToClick = true;
        }
    }
}
