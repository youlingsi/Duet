using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour {
    public int beats = 60;
    public int beatsperbar = 4;
    public int basenote = 4;
    public AudioClip[] piano;
    public AudioClip[] volin;
    public 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


    }

    void PatternGenerater(int beats, int beatsperbar, int basenote)
    {
        float beatstime = 60 / beats;
    }
}
