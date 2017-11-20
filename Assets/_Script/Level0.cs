using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : MonoBehaviour {


    public AudioClip[] Piano;
    public AudioClip[] String;
    public AudioSource com;
    private float t;
    private int indx;
    private int[] RandNoteHis = new int[3] { 0,0,0};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (t > 1)
        {
            RandomNote();
            t = 0;
        }
        else {
            t += Time.deltaTime;
        }

        if (Input.GetKeyUp("a")) { PlayNote(0); }
        else if (Input.GetKeyUp("s")) { PlayNote(1); }
        else if (Input.GetKeyUp("d")) { PlayNote(2); }
        else if (Input.GetKeyUp("f")) { PlayNote(3); }
        else if (Input.GetKeyUp("j")) { PlayNote(4); }
        else if (Input.GetKeyUp("k")) { PlayNote(5); }
        else if (Input.GetKeyUp("l")) { PlayNote(6); }
        else if (Input.GetKeyUp(";")) { PlayNote(7); }
    }

    void RandomNote()
    {
        indx = Random.Range(1, 7);
        RandNoteHis[0] = RandNoteHis[1];
        RandNoteHis[1] = RandNoteHis[2];
		RandNoteHis[2] = indx;
        com.clip = Piano[indx];
        com.Play();
    }

    void PlayNote(int i)
    {
        if(i == RandNoteHis[2])
        {
            AudioSource Source = GetComponent<AudioSource>();
            Source.clip = String[i];
            Source.Play();
        }
    }
}
