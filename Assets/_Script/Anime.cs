using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime : MonoBehaviour {

    public GameObject cloud;
    private Animator anime;

	// Use this for initialization
	void Start () {
        anime = this.GetComponent<Animator>();
        Collider2D coHuman = this.GetComponent<Collider2D>();
        anime.SetBool("Land", true);
        anime.SetBool("Up", false);
        anime.SetBool("Fall", false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Reset()
    {
        anime.SetBool("Land", true);
        anime.SetBool("Up", false);
        anime.SetBool("Fall", false);
        // reset the transform of the cloud and the character
    }

    void Jump()
    {
        anime.SetBool("Land", false);
        anime.SetBool("Up", false);
        anime.SetBool("Fall", false);
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        
    }

    void LandJudge()
    {
        Collider2D co = this.GetComponent<Collider2D>();
        Collider2D cloudco = cloud.GetComponent<Collider2D>();
        if (co.IsTouching(cloudco)){
            anime.SetBool("Land", false);
        }
        
    }

}
