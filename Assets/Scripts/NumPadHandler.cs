using UnityEngine;

public class NumPadHandler : MonoBehaviour
{

    public NumTileHandler slot1;
    public NumTileHandler slot2;
    public NumTileHandler slot3;
    public NumTileHandler slot4;
    public NumTileHandler slot5;
    public NumTileHandler slot6;
    public NumTileHandler slot7;
    public NumTileHandler slot8;
    public NumTileHandler slot9;

    public int emptySlot;

    public bool timeToPuzzle = false;

    public PanelController panel;

    private AudioSource audioSource;
    public AudioClip audioClip;

    //TODO: Make variable for open box 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slot1.boardSlotHolder = 1;
        slot2.boardSlotHolder = 2;
        slot3.boardSlotHolder = 3;
        slot4.boardSlotHolder = 4;
        slot5.boardSlotHolder = 5;
        slot6.boardSlotHolder = 6;
        slot7.boardSlotHolder = 7;
        //8 skipped because it starts null
        slot9.boardSlotHolder = 9;
        GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (timeToPuzzle && slot9 == null) {
            if (slot1.num == 1 && slot2.num == 2 && slot3.num == 3 && slot4.num == 4 && slot5.num == 5 && slot6.num == 6 && slot7.num == 7 && slot8.num == 8) {
                //TODO: OPEN BOX
                timeToPuzzle = false;
                panel.TogglePanel();
                audioSource.PlayOneShot(audioClip);
            }
         }

         audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void StartPuzzle() {
        GetComponent<Renderer>().enabled = true;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.00065f, transform.localPosition.z);
        slot9.moveLeft = true;
        slot5.moveDown = true;
        slot7.moveRight = true;
        emptySlot = 8;
        timeToPuzzle = true;
    }

    public void UpdateBoardState(NumTileHandler moved) {
        ScrubMoves();
        //update the empty slot to match what moved there

        int oldEmptySlot = emptySlot;

        switch(oldEmptySlot) {
            case 1: 
                slot1 = moved;
                slot1.boardSlotHolder = 1;
                break;
            case 2: 
                slot2 = moved;
                slot2.boardSlotHolder = 2;
                break;
            case 3: 
                slot3 = moved;
                slot3.boardSlotHolder = 3;
                break;
            case 4: 
                slot4 = moved;
                slot4.boardSlotHolder = 4;
                break;
            case 5: 
                slot5 = moved;
                slot5.boardSlotHolder = 5;
                break;
            case 6: 
                slot6 = moved;
                slot6.boardSlotHolder = 6;
                break;
            case 7: 
                slot7 = moved;
                slot7.boardSlotHolder = 7;
                break;
            case 8: 
                slot8 = moved;
                slot8.boardSlotHolder = 8;
                break;
            case 9: 
                slot9 = moved;
                slot9.boardSlotHolder = 9;
                break;
        }
        emptySlot = moved.boardSlotHolderBeforeMove;
        //once emptySlot is changed to reflect where the old one came from
        switch(emptySlot) {
            case 1: 
                slot1 = null;
                if (slot2 != null) slot2.moveLeft = true;
                if (slot4 != null) slot4.moveUp = true;
                break;
            case 2: 
                slot2 = null;
                if (slot1 != null) slot1.moveRight = true;
                if (slot3 != null) slot3.moveLeft = true;
                if (slot5 != null) slot5.moveUp = true;
                break;
            case 3: 
                slot3 = null;
                if (slot2 != null) slot2.moveRight = true;
                if (slot6 != null) slot6.moveUp = true;
                break;
            case 4: 
                slot4 = null;
                if (slot1 != null) slot1.moveDown = true;
                if (slot5 != null) slot5.moveLeft = true;
                if (slot7 != null) slot7.moveUp = true;
                break;
            case 5: 
                slot5 = null;
                if (slot2 != null) slot2.moveDown = true;
                if (slot4 != null) slot4.moveRight = true;
                if (slot6 != null) slot6.moveLeft = true;
                if (slot8 != null) slot8.moveUp = true;
                break;
            case 6: 
                slot6 = null;
                if (slot3 != null) slot3.moveDown = true;
                if (slot5 != null) slot5.moveRight = true;
                if (slot9 != null) slot9.moveUp = true;
                break;
            case 7: 
                slot7 = null;
                if (slot4 != null) slot4.moveDown = true;
                if (slot8 != null) slot8.moveLeft = true;
                break;
            case 8: 
                slot8 = null;
                if (slot5 != null) slot5.moveDown = true;
                if (slot7 != null) slot7.moveRight = true;
                if (slot9 != null) slot9.moveLeft = true;
                break;
            case 9:
                slot9 = null;
                if (slot6 != null) slot6.moveDown = true;
                if (slot8 != null) slot8.moveRight = true;
                break;
            default: 
                break;
        }
    }

    public void ScrubMoves() {
        if (slot1 != null) {
            slot1.moveUp = slot1.moveRight = slot1.moveDown = slot1.moveLeft = false;
        }
        if (slot2 != null) {
            slot2.moveUp = slot2.moveRight = slot2.moveDown = slot2.moveLeft = false;
        }
        if (slot3 != null) {
            slot3.moveUp = slot3.moveRight = slot3.moveDown = slot3.moveLeft = false;
        }
        if (slot4 != null) {
            slot4.moveUp = slot4.moveRight = slot4.moveDown = slot4.moveLeft = false;
        }
        if (slot5 != null) {
            slot5.moveUp = slot5.moveRight = slot5.moveDown = slot5.moveLeft = false;
        }
        if (slot6 != null) {
            slot6.moveUp = slot6.moveRight = slot6.moveDown = slot6.moveLeft = false;
        }
        if (slot7 != null) {
            slot7.moveUp = slot7.moveRight = slot7.moveDown = slot7.moveLeft = false;
        }
        if (slot8 != null) {
            slot8.moveUp = slot8.moveRight = slot8.moveDown = slot8.moveLeft = false;
        }
        if (slot9 != null) {
            slot9.moveUp = slot9.moveRight = slot9.moveDown = slot9.moveLeft = false;
        }
    }
}
