using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert_Controller {
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
    public void OnSuccess(CanvasGroup CG, Text Alert, AudioSource Audi, AudioClip ac)
    {
        Alert.text = "Success!";
        Alert.color = Color.green;
        CG.alpha = 1f;
        Audi.PlayOneShot(ac);
    }
    public void OnFailure(CanvasGroup CG, Text Alert, AudioSource Audi, AudioClip ac)
    {
        Alert.text = "Incorrect!";
        Alert.color = Color.red;
        CG.alpha = 1f;
        Audi.PlayOneShot(ac);
    }
    public void HideAlert(CanvasGroup CG)
    {
        CG.alpha = 0;
    }
}
