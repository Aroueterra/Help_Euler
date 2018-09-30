using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialog_Manager : MonoBehaviour {
    private Queue<string> sentences;
    public Text titleText;
    public Text dialogText;
    public Animator anim;
    public CanvasGroup canvasGroup;
    AudioSource audi;
    //private Collider2D collide;
    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
        audi = GameObject.Find("Dialog_Panel").GetComponent<AudioSource>();
        Hide();
	}
	
    public void StartDialog(Dialog dialog, Collider2D coll)
    {
        //collide = coll;
        //Show();
        anim.SetBool("isOpen",true);
        titleText.text = dialog.name;
        sentences.Clear();
        foreach(string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            if(letter == ' ')
                audi.Play();
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        anim.SetBool("isOpen", false);
        
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

    private void OnTriggerExit2D(Collider2D collide)
    {
        EndDialog();
    }
}
