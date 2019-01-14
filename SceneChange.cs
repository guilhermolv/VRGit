using System.Collections;
using UnityEngine;

public class SceneChange : MonoBehaviour {

    public void ChangeToScene (string sceneToChangeTo)
    {
        Application.LoadLevel(sceneToChangeTo);
    }
}
