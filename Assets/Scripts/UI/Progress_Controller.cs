using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress_Controller : MonoBehaviour {
    public int Store_Progress = 0;
    public int Tree_Progress = 0;
    public int Fish_Progress = 0;
    public int Logs_Progress = 0;
    public int Stacks_Progress = 0;

    public Text Health;
    public int health;
    private int Store_Progress_Level = 0;
    private int Logs_Progress_Level = 0;
    //private int Tree_Progress_Level = 0;
    public int GotLogs = 0;
    public bool CanGetLogs = false;
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        Health.text = "x" + health;
        if(health < 1)
        {
            Application.Quit();
        }
        if (Store_Progress == 1 && Store_Progress_Level < 1)
            Store_LevelUp(1); //1st coin
        if (Store_Progress == 2 && GotLogs == 0 && Store_Progress_Level == 1)
            Store_LevelUp(2); //Logs instruction
        if (Store_Progress == 2 && GotLogs == 1 && Store_Progress_Level == 2)
            Store_LevelUp(3); //Logs return
        if (Store_Progress == 2 && GotLogs == 2 && Store_Progress_Level == 3)
            Store_LevelUp(4); //Logs return
        if (Logs_Progress == 1 && Logs_Progress_Level < 1)
            Logs_LevelUp(1); //Logs complete


    }
    public void Damage()
    {
        health--;
    }
    void Store_LevelUp(int option)
    {
        GameObject store = GameObject.Find("Interactable_Shop");
        switch (option)
        {
            case 1:
                store.GetComponent<DialogTrigger>().dialog.name = "A furious shopkeeper speaks..";
                store.GetComponent<DialogTrigger>().dialog.sentences[0] = "You weren't supposed to get a correct answer!";
                store.GetComponent<DialogTrigger>().dialog.sentences[1] = "Give me my coin back!";
                store.GetComponent<DialogTrigger>().dialog.sentences[2] = "Silent treatment eh? Fine, do this for me, and maybe I'll let you keep it.";
                store.GetComponent<DialogTrigger>().dialog.sentences[3] = "I've got a set of boxes here. Fill them up so that each row, column and diagonal will equal fifteen.";
                store.GetComponent<DialogTrigger>().dialog.sentences[4] = "Simple enough for you, right?";
                Store_Progress_Level++;
                break;
            case 2:
                store.GetComponent<DialogTrigger>().dialog.name = "A shopkeeper speaks..";
                store.GetComponent<DialogTrigger>().dialog.sentences[0] = "Excellent! For a cat, you have great organizational skills.";
                store.GetComponent<DialogTrigger>().dialog.sentences[1] = "I still want my coin back, however.";
                store.GetComponent<DialogTrigger>().dialog.sentences[2] = "Hmmm, maybe you can earn it instead.";
                store.GetComponent<DialogTrigger>().dialog.sentences[3] = "I've decided, fetch me some wood from that crate over there.";
                store.GetComponent<DialogTrigger>().dialog.sentences[4] = "I only want the best there is and nothing else. Got it?";
                Store_Progress_Level++;

                break;
            case 3:
                store.GetComponent<DialogTrigger>().dialog.name = "A shopkeeper speaks..";
                store.GetComponent<DialogTrigger>().dialog.sentences[0] = "That's not the right size of log!";
                store.GetComponent<DialogTrigger>().dialog.sentences[1] = "You've got it all wrong, it must be a perfect log.";
                store.GetComponent<DialogTrigger>().dialog.sentences[2] = "Get back in there and get me a proper log.";
                store.GetComponent<DialogTrigger>().dialog.sentences[3] = "I know it's difficult sorting through them...";
                store.GetComponent<DialogTrigger>().dialog.sentences[4] = "Tell you what, i'll give you TWO gold coins for the effort, what do you say?";
                Store_Progress_Level++;
                break;
            case 4:
                store.GetComponent<DialogTrigger>().dialog.name = "A shopkeeper speaks..";
                store.GetComponent<DialogTrigger>().dialog.sentences[0] = "MARVELLOUS!";
                store.GetComponent<DialogTrigger>().dialog.sentences[1] = "You found the perfect log as I imagined.";
                store.GetComponent<DialogTrigger>().dialog.sentences[2] = "I hope you put those two gold coins to good use!";
                store.GetComponent<DialogTrigger>().dialog.sentences[3] = "By the way, if you want more I'll tell you a secret on how to get more...";
                store.GetComponent<DialogTrigger>().dialog.sentences[4] = "See that big tree over there? I hear someone's stash is hidden there. Thank me later!";
                Store_Progress_Level++;
                break;
        }
    }
    void Logs_LevelUp(int option)
    {
        GameObject logs = GameObject.Find("Interactable_Logs");
        switch (option)
        {
            case 1:
                logs.GetComponent<DialogTrigger>().dialog.name = "Boxful of Logs..";
                logs.GetComponent<DialogTrigger>().dialog.sentences[0] = "Despite your efforts, it would seem you got the wrong logs. Let's try that again.";
                logs.GetComponent<DialogTrigger>().dialog.sentences[1] = "Arranged before you are varying sets of logs with different properties.";
                logs.GetComponent<DialogTrigger>().dialog.sentences[2] = "The merchant instructed you to get a particular size of log. There are so many....";
                logs.GetComponent<DialogTrigger>().dialog.sentences[3] = "Sorting them; You again notice 4 attributes. Number, Color and Shape.";
                logs.GetComponent<DialogTrigger>().dialog.sentences[4] = "Remember, three logs make a set if, for each attribute, the properties of the logs are either all the same or all different.";
                logs.GetComponent<DialogTrigger>().dialog.sentences[5] = "How many sets are there now?";
                Logs_Progress_Level++;
                break;
            //case 2:
            //    logs.GetComponent<DialogTrigger>().dialog.name = "Boxful of Logs..";
            //    logs.GetComponent<DialogTrigger>().dialog.sentences[0] = "hmmm";
            //    logs.GetComponent<DialogTrigger>().dialog.sentences[1] = "Arranged before you are varying sets of logs with different properties.";
            //    logs.GetComponent<DialogTrigger>().dialog.sentences[2] = "The merchant instructed you to get a particular size of log. But because there are so many.";
            //    logs.GetComponent<DialogTrigger>().dialog.sentences[3] = "Sorting them; You again notice 4 attributes. Number, Color and Shape.";
            //    logs.GetComponent<DialogTrigger>().dialog.sentences[4] = "Remember, three logs make a set if, for each attribute, the properties of the logs are either all the same or all different.";
            //    logs.GetComponent<DialogTrigger>().dialog.sentences[5] = "How many sets are there now?";
            //    Logs_Progress_Level++;
            //    Debug.Log(Store_Progress_Level);
            //    break;
            //case 3:
            //    break;
        }
    }
}
