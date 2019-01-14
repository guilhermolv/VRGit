using UnityEngine;
using UnityEngine.UI;

public class SliderVal : MonoBehaviour {

    Text txt;
	
	// Update is called once per frame
	void Update () {

        //checks slider value and displays it on the text box next to it
        txt = GetComponent<Text>();
        float breathSliderVal = GameObject.FindGameObjectWithTag("BreathOption").GetComponent<Slider>().value;
        txt.text = breathSliderVal + " Secs";
    }
}
