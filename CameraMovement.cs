using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    //set the cameras default speed
    public static float defaultSpeed = 10f;

    //float to set the speed of the camera
    public float speed;

    //targets transform used to check proximity
    private Transform target;
    //set the index of the waypoint and public float to change the distance
    //in which the next waypoint is triggered
    private int waypointIndex = 0;
    public float wpDistance = 10f;

	// Use this for initialization
	void Start () {
        //set all targets transform to an array
        target = Waypoints.points[0];
        //set the speed to equals the default speed
        speed = defaultSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        //check if the targets position compared to the cameras
        Vector3 dir = target.position - transform.position;
        //tranform the camera position to go towards the next waypoint using the speed
        //variable and Time.deltaTime
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        
        //check distance to next waypoint
        if (Vector3.Distance(transform.position, target.position) <= wpDistance)
        {
            //if distance to next way point is bellow the chosen distance 
            //the GetNextWaypoint function starts
            GetNextWaypoint();            
        }
	}

    
    void GetNextWaypoint()
    {
        //checks if the next waypoint is second to last on the array
        if (waypointIndex >= Waypoints.points.Length - 2)
        {
            //if waypoint is second to last then fade out the scene and start the finishScene coroutine
            Manager.instance.Fader(1);
            StartCoroutine(FinishScene(2));
        }

        //adds a value to the waypoint index and changes the camera to follow the next waypoint
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    IEnumerator FinishScene(int delay)
    {
        //if the scene is finished fade out the breathing sounds and wait a few seconds
        //before starting the Reset function on the managers script
        GameObject.Find("BreathBar").GetComponent<AudioSource>().volume -= Time.deltaTime /3;
        yield return new WaitForSeconds(delay);
        Manager.instance.Reset();
    }


}
