using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void Loader()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        //SceneManager.LoadScene(1);
    }
}
