using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Underwater : MonoBehaviour {

    public Slider breathSlider;
    public Transform Player;
    public float breathChange = 0.25f;

	// Use this for initialization
	void Start ()
    {
        breathSlider.gameObject.SetActive(false); //initially disables the breath slider
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Player.position.y < 4) //when the player is below 4 on the y axis activate the blur effect and displays the breath slider, minusing the breathChange value each update from the current breath
        {
            GameObject.Find("FirstPersonCharacter").GetComponent<Blur>().enabled = true; 
            breathSlider.gameObject.SetActive(true);
            breathSlider.value = breathSlider.value - breathChange;
            
            if(breathSlider.value <= breathSlider.minValue) //if the player's breath reaches 0 or below, save the breath for use in the death script and load the EndGame scene
            {
                PlayerPrefs.SetFloat("Breath", breathSlider.value);
                SceneManager.LoadScene("EndGame");
            }
        }

        if(Player.position.y > 4 && breathSlider.value != breathSlider.maxValue) //when the player is above water the breath slider will be disabled. If their breath is not at the maximum value, it will regenerate by the breathChange value 
        {
            breathSlider.value = breathSlider.value + breathChange;
            GameObject.Find("FirstPersonCharacter").GetComponent<Blur>().enabled = false;
        }

        if(Player.position.y > 4 && breathSlider.value == breathSlider.maxValue) //when the player is above water and the breath is at its maximum value, the breath slider will deactivate
        {
            breathSlider.gameObject.SetActive(false);
        }
        PlayerPrefs.SetFloat("Breath", breathSlider.value);
    }
}
