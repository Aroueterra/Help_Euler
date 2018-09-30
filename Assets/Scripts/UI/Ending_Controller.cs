using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ending_Controller : MonoBehaviour {
    Camera cam;
    public Text txt;
    int counter = 0;
    // Use this for initialization
    void Start() {
        cam = this.gameObject.GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update() {
        //StartCoroutine(LerpFoV(100f));
        cam.orthographicSize = (Screen.height / 100f) / 7;
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) && counter == 0)
        {
            txt.text = "Thank you for playing!";
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) && counter == 1)
        {
            Application.Quit();
        }
    }
    public IEnumerator LerpFoV(float fov)
    {
        float dif = Mathf.Abs(Camera.main.fieldOfView - fov);

        while (dif > 0.05)
        {
            Mathf.Lerp(Camera.main.fieldOfView, fov, 0.1f);
            // update the difference
            dif = Mathf.Abs(Camera.main.fieldOfView - fov);
            yield return null;
        }
        counter++;
    }

    // start the coroutine
}
