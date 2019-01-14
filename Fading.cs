using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

    //set the fading texture and fade speed
	public Texture2D fadingTexture;	
	public float fadingSpeed = 0.9f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1;

	void OnGUI()
	{
        // fade in and out the alpha using direction in seconds
		alpha += fadeDir * fadingSpeed * Time.deltaTime;
        // clamp the fade between 0 and 1
		alpha = Mathf.Clamp01(alpha);

        // set the colour of the GUI
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        // render the black texture on top
		GUI.depth = drawDepth;
        // draw the texture to fit the screen
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadingTexture);
	}
    
    // set the fadeDir to direction of parameter making scene fade in
	public float BeginFade (int direction)
	{
		fadeDir = direction;
		return (fadingSpeed);
	}
    

}
