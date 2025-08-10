using UnityEngine;

public class ExtensionController : MonoBehaviour
{

    private float startingYScale = 0.00001266708f; //finished yscale / 1000
    private float finishedYScale = 0.01266708f;
    private float currentYScale;
    public bool readied = false;
    public bool fullyExtended = false;

        

    //Traveling distances: 
    public float xStep = 0.286235831235f; 
    public float zStep = -0.564983484921f;

    private float finishedX = -0.009879998f;
    private float finishedZ = 0.01459f;

    public RotateObject box;

    public FramePartController frameStart;
        
        
        
    void Awake() {
        transform.localScale = new Vector3(transform.localScale.x, startingYScale, transform.localScale.z);
        //Calculated using trigonometry, rotation angle, and local position
        transform.localPosition = new Vector3(-0.00701763968765f, 0f, 0.00894016515079f);

        currentYScale = startingYScale;
    }

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (readied && !fullyExtended) {
            //if (transform.localPosition.x <= finishedX - xStep && transform.localPosition.z >= finishedZ - zStep) {
                transform.localPosition = new Vector3(finishedX, 0f, finishedZ);
                fullyExtended = true;
                frameStart.PopIn();
            //} else {
                /*
                float newX = transform.localPosition.x - xStep;
                float newZ = transform.localPosition.z - zStep;
                transform.localPosition = new Vector3(newX, 0f, newZ);
                */
                //bandaid fix – no time to fix what broke last second
                //transform.localPosition = new Vector3(finishedX - xStep, 0f, finishedZ - zStep);
            //}
        }
    }

    public void Extend(){
        transform.localScale = new Vector3(transform.localScale.x, finishedYScale, transform.localScale.z);
        transform.localPosition = new Vector3(-0.00704f, 0f, 0.00588f);
        readied = true;
        //transform.localPosition = new Vector3(-0.009879998f, 0f, 0.01459f);
    }
}


/*using UnityEngine;

public class ExtensionController : MonoBehaviour
{

    private float startingYScale = 0.00001266708f; //finished yscale / 1000
    private float finishedYScale = 0.01266708f;
    public bool extending = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Traveling distances: 
    public float xStep = -0.00286235831235f / 1000f; 
    public float zStep = +0.00564983484921f / 1000f;
    
    void Awake() {
        transform.localScale = new Vector3(transform.localScale.x, startingYScale, transform.localScale.z);
        //Calculated using trigonometry, rotation angle, and local position
        transform.localPosition = new Vector3(-0.00701763968765f, 0f, 0.00894016515079f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Lots of trigonometry to make these numbers work manually
        if (extending) {
            if (transform.localScale.y >= finishedYScale - startingYScale) {
                extending = false;
                transform.localScale = new Vector3(transform.localScale.x, finishedYScale, transform.localScale.z);
                transform.localPosition = new Vector3(-0.009879998f, 0f, 0.01459f);
            } else {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + startingYScale, transform.localScale.z);
                transform.localPosition = new Vector3(transform.localPosition.x + xStep, 0f, transform.localPosition.z + zStep);
                
            }
        }
    }
}
*/