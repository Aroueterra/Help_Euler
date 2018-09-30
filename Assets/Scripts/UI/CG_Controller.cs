using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CG_Controller : MonoBehaviour{
    public CanvasGroup Interface;
    public CanvasGroup Puzzle_Store_1;
    public CanvasGroup Puzzle_Store_2;
    public CanvasGroup Puzzle_Logs_1;
    public CanvasGroup Puzzle_Logs_2;
    public CanvasGroup Puzzle_Tree_1;
    public CanvasGroup Puzzle_Stacks_1;
    public CanvasGroup Auxilliary;
    // Use this for initialization
    void Start () {
        Puzzle_Store_1.gameObject.SetActive(!Puzzle_Store_1.gameObject.activeSelf);
        Puzzle_Store_1.alpha = 0f;
        Puzzle_Store_2.gameObject.SetActive(!Puzzle_Store_2.gameObject.activeSelf);
        Puzzle_Store_2.alpha = 0f;
        Puzzle_Logs_1.gameObject.SetActive(!Puzzle_Logs_1.gameObject.activeSelf);
        Puzzle_Logs_1.alpha = 0f;
        Puzzle_Logs_2.gameObject.SetActive(!Puzzle_Logs_2.gameObject.activeSelf);
        Puzzle_Logs_2.alpha = 0f;
        Puzzle_Tree_1.gameObject.SetActive(!Puzzle_Tree_1.gameObject.activeSelf);
        Puzzle_Tree_1.alpha = 0f;
        Puzzle_Stacks_1.gameObject.SetActive(!Puzzle_Stacks_1.gameObject.activeSelf);
        Puzzle_Stacks_1.alpha = 0f;

    }

    public void ShowCG(CanvasGroup CG)
    {
        //CG.gameObject.SetActive(CG.gameObject.activeSelf);
        CG.alpha = 1f;
    }
    public void HideCG(CanvasGroup CG)
    {
        CG.alpha = 0f;
    }
}
