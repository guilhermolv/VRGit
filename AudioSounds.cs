using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class AudioSounds {

    //creates public variables for the audio play script to use    
    public GameObject soundSource;    

    //public variable for the sound clips
    public AudioClip clip;

    //public variables to set sound volume, pitch and spacial blend for 3D sound
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float spacialBlend;

    //public bool to set the clip to loop
    public bool loop;

    //public float to set the max distance the sound can be heard from
    public float maxDistance;

    //public float to set the distance the sound will start playing
    public float triggerDistance;

    //hidden public audio source to find the objects audio source or create one
    [HideInInspector]
    public AudioSource source;
    

}
