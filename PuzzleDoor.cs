using UnityEngine;
using System.Collections;

public class PuzzleDoor : MonoBehaviour {

    public GameObject ButtonN; //the actual buttons that play the animation
    public GameObject ButtonE;
    public GameObject ButtonS;
    public GameObject ButtonW;
    public GameObject PuzzleDoorLeft; 
    public GameObject PuzzleDoorRight;

    public GameObject N; //the glyphs that display whether the button pressed was the correct choice
    public GameObject E;
    public GameObject S;
    public GameObject W;

    bool isNActive = false; //initialises all of them to be false
    bool isEActive = false;
    bool isSActive = false;
    bool isWActive = false;

    bool isDoorOpen = false; //bool to see if the door is already open

    // Use this for initialization
    void Start ()
    {

	}
	
    void ResetPuzzle()
    {
        isNActive = false; //resets the puzzle, changing all of the glyphs to be inactive and changing their font colour back to white (the default colour)
        N.GetComponent<TextMesh>().color = Color.white;

        isEActive = false;
        E.GetComponent<TextMesh>().color = Color.white;

        isSActive = false;
        S.GetComponent<TextMesh>().color = Color.white;

        isWActive = false;
        W.GetComponent<TextMesh>().color = Color.white;
    }

    void CheckForCorrectChoice()
    {
        if(isWActive == true) //if W is pressed first, make the W glyph green (correct choice)
        {
            W.GetComponent<TextMesh>().color = Color.green;
        }
        if (isWActive == true && isEActive == true) //if E is then pressed after W, change the E glyph to be also green
        {
            E.GetComponent<TextMesh>().color = Color.green;
        }
        if (isWActive == true && isEActive == true && isNActive == true) //if N is then pressed after E, change the N glyph to be also green
        {
            N.GetComponent<TextMesh>().color = Color.green;
        }
        if (isWActive == true && isEActive == true && isNActive == true && isSActive == true && isDoorOpen == false) //if S is then pressed after N, change the S glyph to be also green and play the animations for both of the doors to open them,
        {                                                                                                            //and change the isOpen bool to true, to signify that the door is oen
            S.GetComponent<TextMesh>().color = Color.green;
            PuzzleDoorLeft.GetComponent<Animation>().Play("OpenPuzzleDoorLeft");
            PuzzleDoorRight.GetComponent<Animation>().Play("OpenPuzzleDoorRight");
            isDoorOpen = true;
        }
    }

    void CheckIfButtonPressed() //checks whether the button is pressed, as the animation will play upon being interacted with
    {
        if(ButtonN.GetComponent<Animation>().isPlaying == true)
        {
            isNActive = true;
        }
        if (ButtonE.GetComponent<Animation>().isPlaying == true)
        {
            isEActive = true;
        }
        if (ButtonS.GetComponent<Animation>().isPlaying == true)
        {
            isSActive = true;
        }
        if (ButtonW.GetComponent<Animation>().isPlaying == true)
        {
            isWActive = true;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        CheckIfButtonPressed();
        CheckForCorrectChoice();

        if (isWActive == true && isEActive == true && isNActive != true && isSActive == true) //checks to see if the sequences is incorrect, three stages into the puzzle, if incorrect it will reset the puzzle
        {
            ResetPuzzle();
        }

        if (isWActive == true && isEActive != true && (isNActive == true  || isSActive == true)) //checks to see if the sequence is incorrect, two stages into the puzzle, if incorrect it will reset the puzzle
        {
            ResetPuzzle();
        }


        if (isWActive != true && (isNActive == true || isEActive == true || isSActive == true)) //checks to see if the sequences is incorrect, one stage into the puzzle, if incorrect it will reset the puzzle
        {
            ResetPuzzle();
        }

    }

}
