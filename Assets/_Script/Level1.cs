using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {


    public AudioClip[] notes;
    public AudioSource com;
    private float t;
    private int indx;
    private int[] RandNoteHis = new int[3] { 0,0,0};
	private float[] lenth = new float[]{ 0.25f, 0.5f, 1.0f, 2f };
	private int lenthIndex = 1;
	private float comPlay = 0f;
	private float playerPlay = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (t > lenth[lenthIndex])
        {
            RandomNote();
            t = 0;
			lenthIndex = Random.Range (0, lenth.Length);
			comPlay = Time.realtimeSinceStartup;
        }
        else {
            t += Time.deltaTime;
        }
		if (Input.GetKeyDown ("f")) {
			PlayNote (indx);
		}

//        if (Input.GetKeyUp("a")) { PlayNote(0); }
//        else if (Input.GetKeyUp("s")) { PlayNote(1); }
//        else if (Input.GetKeyUp("d")) { PlayNote(2); }
//        else if (Input.GetKeyUp("f")) { PlayNote(3); }
//        else if (Input.GetKeyUp("j")) { PlayNote(4); }
//        else if (Input.GetKeyUp("k")) { PlayNote(5); }
//        else if (Input.GetKeyUp("l")) { PlayNote(6); }
//        else if (Input.GetKeyUp(";")) { PlayNote(7); }
    }

    void RandomNote()
    {
        indx = Random.Range(0, 7);
        RandNoteHis[0] = RandNoteHis[1];
        RandNoteHis[1] = RandNoteHis[2];
		RandNoteHis[2] = indx;
        com.clip = notes[indx];
        com.Play();
    }

    void PlayNote(int i)
    {
		playerPlay = Time.realtimeSinceStartup;
		if((playerPlay - comPlay) < 0.5)
        {
            AudioSource Source = GetComponent<AudioSource>();
            Source.clip = notes[i];
            Source.Play();
        }
    }
}
