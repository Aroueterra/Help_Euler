using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Submit_Numbers : MonoBehaviour {
    public InputField[] IF;
    Alert_Controller ac = new Alert_Controller();
    public GameObject Alert;
    public GameObject Player;
    public GameObject Coin;
    private Button Submit;
    CG_Controller CGC;
    // Use this for initialization
    private AudioSource audi_source;
    private AudioClip fail;
    private AudioClip success;
    private Progress_Controller PC;
    
    void Start () {
        Alert = GameObject.Find("Alert");
        Player = GameObject.Find("Player");
        PC = GameObject.Find("Progress_Obj").GetComponent<Progress_Controller>();
        audi_source = Player.GetComponent<Player_Movement>().audi;
        fail = Player.GetComponent<Player_Movement>().Fail;
        success = Player.GetComponent<Player_Movement>().Success;
        IF[3].text = (4).ToString();
        Submit = this.gameObject.GetComponent<Button>();
        Submit.onClick.AddListener(OnSubmitChecko);
    }
	
	// Update is called once per frame
    void HideCG()
    {
        GameObject MakeVisible = GameObject.Find("Visibility");
        MakeVisible.GetComponent<Visibility>().visible = false;
        GameObject cm = GameObject.Find("Canvas_Manager");
        CGC = cm.GetComponent<CG_Controller>();
        CGC.HideCG(CGC.Puzzle_Store_1);
        Invoke("_HideAlert", 5);
    }
    void _HideAlert()
    {
        ac.HideAlert(Alert.GetComponent<CanvasGroup>());
    }
    public void OnSubmitChecko()
    {
        if (PC.Store_Progress < 1)
        {
            bool failed = false;
            foreach (InputField var in IF)
            {
                if (var.text == "" || var.text == " ")
                {
                    ac.OnFailure(Alert.GetComponent<CanvasGroup>(), Alert.GetComponent<Text>(), audi_source, fail);
                    failed = true;
                    PC.Damage();
                    HideCG();

                    return;
                }
            }
            if (!failed)
            {
                int counter = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (Convert.ToInt32(IF[i].text) == i + 1)
                    {
                        counter++;
                    }
                }
                if (counter == 6)
                {
                    PC.Store_Progress++;
                    ac.OnSuccess(Alert.GetComponent<CanvasGroup>(), Alert.GetComponent<Text>(), audi_source, success);
                    Invoke("_HideAlert", 5);
                    CreateCoin(Player.transform);
                    HideCG();
                }
                else
                {
                    ac.OnFailure(Alert.GetComponent<CanvasGroup>(), Alert.GetComponent<Text>(), audi_source, fail);
                    failed = true;
                    HideCG();
                    PC.Damage();
                    return;
                }
            }
        }
    }
    public void CreateCoin(Transform target)
    {
        float angle = UnityEngine.Random.Range(2f, Mathf.PI * 2);

        // create a vector with length 1.0
        Vector3 V = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

        // scale it to the desired length
        V *= 1.2f;

        GameObject instance = Instantiate(Coin);
        instance.transform.position = Vector2.MoveTowards(V, target.position, 3 * Time.deltaTime);
    }
}
