using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tree_Controller : MonoBehaviour
{
    public GameObject Player;
    public GameObject Coin;
    public GameObject Alert;
    public InputField Submit;
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
        //Sound Setup
        audi_source = Player.GetComponent<Player_Movement>().audi;
        fail = Player.GetComponent<Player_Movement>().Fail;
        success = Player.GetComponent<Player_Movement>().Success;
        //Event Setup
        //Submit.onEndEdit.AddListener(val =>
        //{
        //});onEndEdit
        Submit.onEndEdit.AddListener(delegate { OnSubmitChecka(); });
    }
    private bool wasFocused;
    void Update()
    {
        if (wasFocused && (Submit.text.Length > 0) && Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (Submit.text == "" || Submit.text == " ")
            {
                return;
            }
            OnSubmitChecka();
        }
        else
            wasFocused = Submit.isFocused;
    }
    public void OnSubmitChecka()
    {//2
        if (PC.Logs_Progress >= 2 && PC.Store_Progress >= 2)
        {
            string input = Submit.text;
            bool failed = false;
            //Check WhiteSpace
            int answer;
            bool ifSuccess = System.Int32.TryParse(input, out answer);
            if (ifSuccess == false)
            {
                if (input == "" || input == " ")
                {
                    return;
                }
                ac.OnFailure(Alert.GetComponent<CanvasGroup>(), Alert.GetComponent<Text>(), audi_source, fail);
                failed = true;
                PC.Damage();
                HideCG("Puzzle_Tree_1");
                Submit.text = "";
            }
            else if (!failed)
            {
                if (answer == 3)
                {
                    PC.Tree_Progress++;
                    ac.OnSuccess(Alert.GetComponent<CanvasGroup>(), Alert.GetComponent<Text>(), audi_source, success);
                    CreateCoin(Player.transform);
                    CreateCoin(Player.transform);
                    CreateCoin(Player.transform);
                    HideCG("Puzzle_Tree_1");
                    Submit.text = "";
                }
                else
                {
                    ac.OnFailure(Alert.GetComponent<CanvasGroup>(), Alert.GetComponent<Text>(), audi_source, fail);
                    PC.Damage();
                    HideCG("Puzzle_Tree_1");
                    Submit.text = "";
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
        CGC.HideCG(CGC.Puzzle_Tree_1);
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
