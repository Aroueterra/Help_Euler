using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Store2_Script : MonoBehaviour {
    public InputField[] IF;
    public GameObject Player;
    public GameObject Coin;
    public GameObject Alert;
    public Button Submit;
    // Private Init
    Alert_Controller ac = new Alert_Controller();
    private Progress_Controller PC;
    private CG_Controller CGC;
    private AudioSource audi_source;
    private AudioClip fail;
    private AudioClip success;
    void Start () {
        //Object Setup
        Alert = GameObject.Find("Alert");
        Player = GameObject.Find("Player");
        PC = GameObject.Find("Progress_Obj").GetComponent<Progress_Controller>();
        //Sound Setup
        audi_source = Player.GetComponent<Player_Movement>().audi;
        fail = Player.GetComponent<Player_Movement>().Fail;
        success = Player.GetComponent<Player_Movement>().Success;
        //Event Setup
        Submit.onClick.AddListener(OnSubmitChecker);
    }
    public bool SumTotal()
    {
        int[] array;
        int counter = 0;
        array = new int[9];
        foreach(InputField var in IF)
        {
            array[counter] = Convert.ToInt32(var.text);
            counter++;
        }
        int totalColumns = 3;
        int totalRows = 3;
        int[] colTotals = new int[totalColumns];
        int[] rowTotals = new int[totalRows];
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalColumns; j++)
            {
                rowTotals[i] += array[i * totalColumns + j];
                colTotals[j] += array[i * totalColumns + j];
            }
        }
        Debug.Log("Row totals");

        foreach (int rowTotal in rowTotals)
        {
            if(rowTotal != 15)
            {
                return false;
            }
            else
            {
            }
        }

        Debug.Log("Col totals");

        foreach (int colTotal in colTotals)
        {
            if (colTotal != 15)
            {
                return false;
            }
            else
            {

            }
        }
        if ( ((array[0]+array[4]+array[8]) == 15) && ((array[2] + array[4] + array[6]) == 15) )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnSubmitChecker()
    {
        if (PC.Store_Progress == 1)
        {
            bool failed = false;
            //Check WhiteSpace
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
                if (SumTotal() == true)
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
    void HideCG()
    {
        GameObject MakeVisible = GameObject.Find("Puzzle_Store_2");
        MakeVisible.GetComponent<Visibility>().visible = false;
        GameObject cm = GameObject.Find("Canvas_Manager");
        CGC = cm.GetComponent<CG_Controller>();
        CGC.HideCG(CGC.Puzzle_Store_2);
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
