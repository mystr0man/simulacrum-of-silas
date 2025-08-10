using UnityEngine;

public class FacePortionHandler : MonoBehaviour
{
    public bool renderedOnStart = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (renderedOnStart) {
            GetComponent<Renderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
