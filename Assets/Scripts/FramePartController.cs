using UnityEngine;
using System.Collections;

public class FramePartController : MonoBehaviour
{
    public FramePartController nextPart;

    public LegExtendController leg1;

    public LegExtendController leg2;

    public NumPadHandler numpad;

    public RotateObject box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // audio source and clip to be used for sound playing
    private AudioSource audioSource;
    public AudioClip audioClip;

    void Awake() {
        GetComponent<Renderer>().enabled = false;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopIn() {
        GetComponent<Renderer>().enabled = true;

        //TODO: ADD POPPING SOUND
        audioSource.PlayOneShot(audioClip);


        if (nextPart != null) {
            StartCoroutine(ShortWait());
        } else if (numpad != null) {
            numpad.StartPuzzle();
            box.ReturnControl();
        } else if (leg1 != null && leg2 != null) {
            leg1.GetComponent<Renderer>().enabled = true;
            leg2.GetComponent<Renderer>().enabled = true;
            leg1.move = true;
            leg2.move = true;
        } 
    }

    // Using a coroutine for wait was ChatGPT's suggestion; logic + implementation is my own
    private IEnumerator ShortWait() {
        yield return new WaitForSeconds(0.47f); // wait for a beat
        nextPart.PopIn();
    }
}
