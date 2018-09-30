using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stacks_Controller : MonoBehaviour
{
    public GameObject Player;
    public GameObject Coin;
    public GameObject Alert;
    public InputField Submitter;
    // Private Init
    Alert_Controller ac = new Alert_Controller();
    private Progress_Controller PC;
    private CG_Controller CGC;
    private AudioSource audi_source;
    private AudioClip fail;
    private AudioClip success;
    void Start()
    {
        //Object Setup
        Alert = GameObject.Find("Alert");
        Player = GameObject.Find("Player");
        PC = GameObject.Find("Progress_Obj").GetComponent<Progress_Controller>();
        Submitter = GameObject.FindWithTag("Writer").GetComponent<InputField>();
        //Sound Setup
        audi_source = Player.GetComponent<Player_Movement>().audi;
        fail = Player.GetComponent<Player_Movement>().Fail;
        success = Player.GetComponent<Player_Movement>().Success;
        //Event Setup
        //Submit.onEndEdit.AddListener(val =>
        //{
        //});onEndEdit

        Submitter.onEndEdit.AddListener(delegate { OnSubmitChecking(); });
    }
    private bool wasFocused;
    void Update()
    {
        if (wasFocused && (Submitter.text.Length > 0) && Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("?+");
            if (Submitter.text == "" || Submitter.text == " ")
            {
                return;
            }
            Debug.Log("?-");
            OnSubmitChecking();
        }
        else
            wasFocused = Submitter.isFocused;
    }
    public void OnSubmitChecking()
    {
        if (true) { 

            
            string input = Submitter.text;
            bool failed = false;
            //Check WhiteSpace
            int answer;
            bool ifSuccess = System.Int32.TryParse(input, out answer);
            if (ifSuccess == false)
            {
                
                if (input == "" || input == " ")

                {
                    Debug.Log("1>");
                    return;
                }

                ac.OnFailure(Alert.GetComponent<CanvasGroup>(), Alert.GetComponent<Text>(), audi_source, fail);
                failed = true;
                PC.Damage();
                HideCG("Puzzle_Stacks_1");
                Submitter.text = "";
                Debug.Log("9>");
            }
            else if (!failed)
            {
                Debug.Log("2>");
                if (answer == 9)
                {
                    Debug.Log("3>");
                    PC.Stacks_Progress++;
                    ac.OnSuccess(Alert.GetComponent<CanvasGroup>(), Alert.GetComponent<Text>(), audi_source, success);
                    CreateCoin(Player.transform);
                    CreateCoin(Player.transform);
                    HideCG("Puzzle_Stacks_1");
                    Submitter.text = "";
                }
                else
                {
                    Debug.Log("4>");
                    ac.OnFailure(Alert.GetComponent<CanvasGroup>(), Alert.GetComponent<Text>(), audi_source, fail);
                    PC.Damage();
                    HideCG("Puzzle_Stacks_1");
                    Submitter.text = "";
                }
            }

        }
    }
    void HideCG(string Parent)
    {
        GameObject MakeVisible = GameObject.Find(Parent);
        MakeVisible.GetComponent<Visibility>().visible = false;
        GameObject cm = GameObject.Find("Canvas_Manager");
        CGC = cm.GetComponent<CG_Controller>();
        CGC.HideCG(CGC.Puzzle_Stacks_1);
        Invoke("_HideAlert", 5);
    }
    void _HideAlert()
    {
        ac.HideAlert(Alert.GetComponent<CanvasGroup>());
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
