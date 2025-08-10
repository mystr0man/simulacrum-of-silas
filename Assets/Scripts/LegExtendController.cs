using UnityEngine;

public class LegExtendController : MonoBehaviour
{
    private float startingZ = -0.0039f;
    private float endingZ = -0.01926f;

    private float stepZ;

    public bool move = false;
    public bool finished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake() {
        stepZ = (endingZ - startingZ)/1000f;
        GetComponent<Renderer>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (move && !finished) {
            if (transform.localPosition.z + stepZ <= endingZ) {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, endingZ);
                move = false;
                finished = true;

                //TOOD: prompt next thing
            } else {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + stepZ);
            }
        }
    }
}
