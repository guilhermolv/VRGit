using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    Image fillImg;
    public float timeAmt = 10;
    float time;

	// Use this for initialization
	void Start () {
        fillImg = this.GetComponent<Image>();
        time = timeAmt;
	}
	
	// Update is called once per frame
	void Update () {
        if(time > 0)
        {
            time -= Time.deltaTime;
            fillImg.fillAmount = time / timeAmt;

            if (time < 0)
            {
                fillImg.fillAmount = 1;
                time = timeAmt;
            }
        }

        Debug.Log(time);	
	}
}
