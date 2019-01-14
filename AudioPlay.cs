using UnityEngine;

public class AudioPlay : MonoBehaviour
{

    public AudioSounds[] sounds;
    private Vector3[] positionArray;
    private float triggerDistance;

    AudioSource playAudio;
    float proximity = 0f;

    public static AudioPlay instance = null;

    // Use this for initialization
    void Awake()
    {
        //adds Audio Sounds script attributes to the inspector
        foreach (AudioSounds s in sounds)
        {
            s.source = s.soundSource.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spacialBlend;
            s.source.maxDistance = s.maxDistance;

            //chosen distance will be distace the audio starts working in and the max distance of the audio source
            triggerDistance = s.triggerDistance;
        }
    }

    void Start()
    {

        //Array to hold audio sources Vector 3 positions in relation to the audio gameobjects
        positionArray = new Vector3[sounds.Length];

        for (int i = 0; i < sounds.Length; i++)
        {
            Vector3 position = sounds[i].source.transform.position;
            positionArray[i] = position;
        }

    }

    
    // Update is called once per frame
    void Update()
    {
            //checks if player is under the distance so that the audio can start playing
            for (int i = 0; i < sounds.Length; i++)
            {
                playAudio = sounds[i].soundSource.GetComponent<AudioSource>();
                proximity = Vector3.Distance(transform.position, positionArray[i]);
                triggerDistance = sounds[i].triggerDistance;

                if (proximity < triggerDistance)
                {
                    //checks that the audio is not already playing
                    if (!playAudio.isPlaying)
                    {
                        playAudio.PlayOneShot(playAudio.clip);
                    }
                }
                else
                {
                    //stops audio when ditance is above the chosen distance
                    playAudio.Stop();
                }
            }
    }
}
