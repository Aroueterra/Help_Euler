using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public Dialog dialog;
    public void TriggerDialog(Collider2D coll)
    {
        FindObjectOfType<Dialog_Manager>().StartDialog(dialog, coll);
    }
}
