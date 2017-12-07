using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime : MonoBehaviour {

    private Animator anime;

	// Use this for initialization
	void Start () {
        anime = this.GetComponent<Animator>();
        anime.SetBool("Sing", false);
        anime.SetBool("Fail", false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        anime.SetBool("Sing", false);
        anime.SetBool("Fail", false);
    }

    public void Sing()
    {
        anime.SetBool("Sing", true);
        anime.SetBool("Fail", false);        
    }


    public void Fail()
    {
        anime.SetBool("Fail", true);
    }

}
