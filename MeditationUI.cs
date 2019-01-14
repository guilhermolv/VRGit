using UnityEngine;
using UnityEngine.UI;

public class MeditationUI : MonoBehaviour {

    Image fillImg;
    float timer;
    float interval;

    public AudioClip inhale;
    public AudioClip exhale;

    void Awake()
    {
        //finds the manager object which is storing the breath option script
        Manager manager  = GameObject.Find("_Manager").GetComponent<Manager>();

        //interval is changed to match user chosen breath length
        interval = manager.breathOption;
    }

    // Use this for initialization
    void Start () {
        fillImg = GetComponent<Image>();    
    }
	
	// Update is called once per frame
	void Update () {
        //sets the timer to bounce between 0 and 1 based on the user
        //chosen interval then change the breath bars fill amount accordingly
        timer = Mathf.PingPong(Time.timeSinceLevelLoad / interval, 1);
        fillImg.fillAmount = timer;

        //when the fill amount is at the bottom play the inhale sound clip
        //when it is at the top play in exhale sound clip
        if (timer <= .1f && !GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(inhale, 1f);
        } else if (timer >= .9f && !GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(exhale, 1f);
        }
    }
}
