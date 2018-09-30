using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour {
    public bool visible;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Transform child in this.transform)
        {
            if (visible == true)
            {
                child.gameObject.SetActive(true);
            }
            else 
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
