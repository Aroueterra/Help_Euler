using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interact_Tip : MonoBehaviour {
    public GameObject Interactable;
    public Transform damageTransform;
    public GameObject Coin;
    public GameObject Collider = null;
    public GameObject Notifier;
    public GameObject Player;
    private GameObject Fade_Instance;
    private bool Entered = false;
    //private int cntr = 0;
    //private Transform target;
    private CanvasGroup canvasGroup;
    // Update is called once per frame
    private DialogTrigger trigger;
    Alert_Controller ac = new Alert_Controller();
    CG_Controller CGC;
    GameObject Alert;
    private AudioSource audi_source;
    //private AudioClip fail_sound;
    //private AudioClip success_sound;
    private AudioClip click_sound;
    private Progress_Controller PC;
    private GameObject damagePrefab;

    CanvasGroup currentGroup;
    void Start()
    {
        trigger = GetComponent<DialogTrigger>();
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        canvasGroup = GameObject.Find("Interface").GetComponent<CanvasGroup>();
        Alert = GameObject.Find("Alert");
        audi_source = Player.GetComponent<Player_Movement>().audi;
        //fail_sound = Player.GetComponent<Player_Movement>().Fail;
        //success_sound = Player.GetComponent<Player_Movement>().Success;
        click_sound = Player.GetComponent<Player_Movement>().Click;
        PC = GameObject.Find("Progress_Obj").GetComponent<Progress_Controller>();
    }
    
    void Update () {
        if (Entered == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D coll = Collider.GetComponent<Collider2D>();
                if (coll.OverlapPoint(wp) && !EventSystem.current.IsPointerOverGameObject())
                {
                    audi_source.PlayOneShot(click_sound);
                    GameObject cm = GameObject.Find("Canvas_Manager");
                    CGC = cm.GetComponent<CG_Controller>();
                    switch (this.gameObject.name)
                    {
                        case "Interactable_Guide":
                            EnableDialog(coll);
                            break;
                        case "Interactable_Stack":
                            ////StartCoroutine(ExecuteAfterTime(4, "Puzzle_Stacks_1", CGC.Puzzle_Stacks_1));
                            ////EnableDialog(coll);
                            break;
                        case "Interactable_Shop":
                            if (PC.Store_Progress < 1)
                            {
                                StartCoroutine(ExecuteAfterTime(4, "Visibility", CGC.Puzzle_Store_1));
                                EnableDialog(coll);
                            }
                            if (PC.Store_Progress == 1)
                            {
                                StartCoroutine(ExecuteAfterTime(4, "Puzzle_Store_2", CGC.Puzzle_Store_2));
                                EnableDialog(coll);
                            }
                            if (PC.Store_Progress == 2 && PC.GotLogs==0)
                            {
                                PC.CanGetLogs = true;
                                EnableDialog(coll); 
                            }
                            if (PC.Store_Progress == 2 && PC.GotLogs == 1)
                            {
                                PC.CanGetLogs = true;
                                EnableDialog(coll); 
                            }
                            if (PC.Store_Progress == 2 && PC.GotLogs == 2)
                            {
                                EnableDialog(coll);
                            }
                            break;
                        case "Interactable_Tree"://
                            if (PC.Store_Progress == 2 && PC.GotLogs == 2)
                            {
                                StartCoroutine(ExecuteAfterTime(4, "Puzzle_Tree_1", CGC.Puzzle_Tree_1));
                                EnableDialog(coll);
                            }
                            break;
                        case "Interactable_Logs":
                            if (PC.Logs_Progress < 1 && PC.Store_Progress == 2 && PC.CanGetLogs == true)
                            {
                                StartCoroutine(ExecuteAfterTime(2, "Puzzle_Logs_1", CGC.Puzzle_Logs_1));
                                EnableDialog(coll);
                            }
                            if(PC.Logs_Progress == 1 && PC.Store_Progress == 2 && PC.CanGetLogs == true)
                            {
                                StartCoroutine(ExecuteAfterTime(2, "Puzzle_Logs_2", CGC.Puzzle_Logs_2));
                                EnableDialog(coll);
                            }
                            break;
                        case "Interactable_Guard":
                            if (Player.GetComponent<Player_Movement>().score >= 12)
                            {
                                SceneManager.LoadScene("End", LoadSceneMode.Single);
                            }
                            EnableDialog(coll);
                            break;
                    }
                }
            }
        }
    }
    void EnableDialog(Collider2D coll)
    {
        //Invoke("_HideAlert", 5);
        Show();
        trigger.TriggerDialog(coll);
    }
    IEnumerator ExecuteAfterTime(float time, string Parent, CanvasGroup cg)
    {
        yield return new WaitForSeconds(time);
        GameObject MakeVisible = GameObject.Find(Parent);
        MakeVisible.GetComponent<Visibility>().visible = true;
        currentGroup = cg;
        CGC.ShowCG(cg);
    }
    void _HideAlert()
    {
        ac.HideAlert(Alert.GetComponent<CanvasGroup>());
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
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
    

    public void CreateFloatingText(string text, Vector2 textPosition, Color32 textColor)
    {
        if (GameObject.FindWithTag("Destroy") == null)
        {
            GameObject instance = Instantiate(Notifier);
            instance.transform.Find("Notify").GetComponent<Text>().text = text;
            instance.transform.Find("Notify").GetComponent<Text>().color = textColor;
            //instance.transform.SetParent(Noter.transform, false);
            instance.transform.SetParent(Player.transform, false);
            instance.transform.position = textPosition;;
            Fade_Instance = instance;
            if (Fade_Instance != null)
                StartCoroutine(FadeTextToFullAlpha(1f, instance.transform.Find("Notify").GetComponent<Text>()));
        }
        else
        {
            DestroyAllObjects();
        }
    }

    public void CreateCoin(Transform target)
    {
        float angle = Random.Range(2f, Mathf.PI * 2);

        // create a vector with length 1.0
        Vector3 V = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

        // scale it to the desired length
        V *= 1.2f;
        
        GameObject instance = Instantiate(Coin);
        instance.transform.position = Vector2.MoveTowards(V, target.position, 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entered = true;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Entered = false;
        DestroyAllObjects();
        Hide();
        if (CGC != null && currentGroup != null)
        {
            CGC.HideCG(currentGroup);
        }
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    public void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
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
}

