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
        anime.SetBool("Fall", false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        anime.SetBool("Land", true);
        anime.SetBool("Fall", false);
        // reset the transform of the cloud and the character
    }

    public void Jump()
    {
        anime.SetBool("Land", false);
        anime.SetBool("Fall", false);
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 50), ForceMode2D.Impulse);
        
    }

    public void LandJudge()
    {
        Collider2D co = this.GetComponent<Collider2D>();
        Collider2D cloudco = cloud.GetComponent<Collider2D>();
        if (co.IsTouching(cloudco)){
            anime.SetBool("Land", true);
        }
        else
        {
            anime.SetBool("Land", false);
        }
        
    }

    public void Fall()
    {
        anime.SetBool("Fall", true);
    }

}
