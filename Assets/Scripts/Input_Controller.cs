using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_Controller : MonoBehaviour {
    public GameObject Inputter;
    public GameObject Player;
    public GameObject Noter;
    private GameObject Fade_Instance;
    private bool allowEnter;
    private bool typing;
    private string input;
    private InputField InputField;
    // Use this for initialization
    void Start () {
        InputField = Inputter.GetComponent<InputField>();
        //InputField.GetComponent<CanvasRenderer>().enabled = false;
        //Inputter.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Inputter.SetActive(true);
        //    InputField.Select();
        //    InputField.text = "";
        //}
        //if (allowEnter && (InputField.text.Length > 0) && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        //{
        //    OnSubmit();
        //    allowEnter = false;
        //    InputField.text = "";
        //}
        //else
        //{
        //    allowEnter = InputField.isFocused;
        //}
    }
    void OnSubmit()
    {
        input = InputField.text;
        //CreateFloatingText(input, Player.transform.position, Color.white);
    }


    public void CreateFloatingText(string text, Vector2 textPosition, Color32 textColor)
    {
        if (GameObject.FindWithTag("Destroy") == null)
        {
            GameObject instance = Instantiate(Noter);
            instance.transform.Find("Speech").GetComponent<Text>().text = text;
            instance.transform.Find("Speech").GetComponent<Text>().color = textColor;
            instance.transform.SetParent(Player.transform, false);
            instance.transform.position = textPosition;
            Fade_Instance = instance;
            if (Fade_Instance != null)
                StartCoroutine(FadeTextToFullAlpha(1f, instance.transform.Find("Speech").GetComponent<Text>()));

        }
        else
        {
            DestroyAllObjects();
        }
    }
    private GameObject[] Destroyer;

    void DestroyAllObjects()
    {
        Destroyer = GameObject.FindGameObjectsWithTag("Destroy");
        if (Fade_Instance != null)
        {
            //StartCoroutine(FadeTextToZeroAlpha(.5f, Fade_Instance.transform.Find("Notify").GetComponent<Text>()));
            StopAllCoroutines();
            for (var i = 0; i < Destroyer.Length; i++)
            {
                Destroy(Destroyer[i]);
            }
        }
    }
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            
        }
        yield return new WaitForSeconds(1);
        if (i != null)
            FadeTextToZeroAlpha(t, i);
        Invoke("DestroyAllObjects", 4); 
        
    }
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
