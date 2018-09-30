using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_Movement : MonoBehaviour {
    Animator anim;
    public AudioSource audi;
    public int score;
    public Text ScoreBoard;
    public AudioClip Success;
    public AudioClip Coins;
    public AudioClip Fail;
    public AudioClip Click;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        audi = GetComponent<AudioSource>();
        score = 0;
        ScoreBoard.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        float input_x = Input.GetAxisRaw("Horizontal");
        float input_y = Input.GetAxisRaw("Vertical");

        bool isWalking = (Mathf.Abs(input_x) + Mathf.Abs(input_y)> 0);

        anim.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            anim.SetFloat("x", input_x);
            anim.SetFloat("y", input_y);
            transform.position += new Vector3(input_x, input_y, 0).normalized * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D pickup)
    {
        if (pickup.gameObject.CompareTag("Pickup"))
        {
            Debug.Log("?");
            score += 1;
            audi.clip = Coins;
            audi.Play();
            Destroy(pickup.gameObject);
            UpdateScore();
        }
    }
    void UpdateScore()
    {
        ScoreBoard.text = score.ToString();

    }
}
