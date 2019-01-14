using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VR;
using System.Collections;

public class Manager : MonoBehaviour
{
    //sets up all UI public variables to be dragged in
    [Header("UI Elements")]
    public Button submitButton;
    public Button howToButton;
    public Button backButton;
    public GameObject howToUI;
    public Dropdown sceneSelect;
    public Slider breathSlider;
    public Image loadBar;
    public GameObject UICanvas;

    int sceneSelection;
    private float breathLength;
    public float breathOption;
    public static Manager instance = null;    
    

    void Awake()
    {

        //Check if instance already exists
        if (instance == null)
        {

            //if not, set instance to this
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scenedestor
        DontDestroyOnLoad(gameObject);
    }


    public void Start()
    {
        //make sure VR settings are disabled for the menu screen upon start
        VRSettings.enabled = false;

        //add functions to the UI buttons
        submitButton.onClick.AddListener(() => TaskOnClick());
        howToButton.onClick.AddListener(() => HowToMenu());
        backButton.onClick.AddListener(() => HowToMenuBack());
    }

    //function to run when button has been pressed
    public void TaskOnClick()
        {
        //finding the user options on scene and breath length
        sceneSelection = sceneSelect.value;
        breathLength = breathSlider.value;

        //start coroutine to play selected scene
        StartCoroutine(LoadScene(sceneSelection + 1));
    }

    //function to set the How To screen active
    public void HowToMenu()
    {
        howToUI.SetActive(true);
    }

    //function to disable the How To screen
    public void HowToMenuBack ()
    {
        howToUI.SetActive(false);
    }

    //coroutine to load the next scene and use the loading bar
    IEnumerator LoadScene(int sceneIndex)
    {
        //load the chosen scene and work out its progress to be set to
        //the fill amount of the loadbar
        AsyncOperation nextScene = SceneManager.LoadSceneAsync(sceneIndex);        

        while (!nextScene.isDone)
        {
            float progress = Mathf.Clamp01(nextScene.progress / .9f);
            loadBar.fillAmount = nextScene.progress;
            yield return null;
        }
        //start the loadDevice coroutine to set the VR device to google cardboard
        StartCoroutine(LoadDevice("cardboard"));
        yield return null;
    }

    void Update()
    {
        //set the user selected breath option to the public breathLength variable
        breathOption = breathLength;

        //check if the scene is not the menu scene
        //if so disable the menu GUI
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            loadBar.fillAmount = 0;
            UICanvas.SetActive(false);
        } else
        {
            UICanvas.SetActive(true);
        }
            if (Input.GetKeyDown(KeyCode.Escape))
        {
            //uses the VR close icon to close the current scene and open the menu scene
            Fader(1);
            StartCoroutine(SceneOptions(2f));
        }
    }

    //public function to reset the scene when meditation has finished
    public void Reset()
    {
        StartCoroutine(FinishScene(5));
    }

    //coroutine to set the VR device and enable the VR settings
    IEnumerator LoadDevice(string newDevice)
    {
        VRSettings.LoadDeviceByName(newDevice);
        yield return null;
        VRSettings.enabled = true;
    }

    //coroutine to load the finish scene, wait a few seconds then load the 
    //menu scene
    IEnumerator FinishScene(float delay)
    {
        SceneManager.LoadScene(4);
        yield return new WaitForSeconds(delay);
        StartCoroutine(SceneOptions(5));
    }

    //function to use to fade script
    public void Fader(int fade)
    {
        GetComponent<Fading>().BeginFade(fade);
    }

    // called when level is loaded to fade the screen
    void OnLevelWasLoaded()
    {
        Fader(-1);
    }

    //coroutine to set the VR device to none and disable to VR setting
    //wait a few seconds before the menu scene is loaded
    IEnumerator SceneOptions(float delay) {
        VRSettings.LoadDeviceByName("None");
        VRSettings.enabled = false;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
        yield break;
    }

    //reload menu scene when user comes off the application
    /*void OnApplicationPause(bool pause)
    {
        StartCoroutine(SceneOptions(2));
    }*/
    }
