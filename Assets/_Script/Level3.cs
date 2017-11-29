using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour {
<<<<<<< HEAD


    private float[] NOTES = new float[3] { 0.125f, 0.25f, 0.375, 0.5f, 1f};
    private string PATTERNPATH = "Assets/pattern.txt";
    private string PITCHPATH = "Assets/pitch.txt";
    public int nNotes = 4;

    public float unitNote = 0.25f;
    public int bpm = 60;
    public AudioClip[] piano;
    public AudioClip[] voilin;
    public int barNum = 10;
    public AudioSource com;
    public float tolerence = 0.5f;

    private int state = 0; //0= computer play. 1= playerplay
//    private float initialTime;
    private float unitTime = 0;
    private List<List<int>> songTime = new List<List<int>>();
	private List<List<int>> sPitch = new List<List<int>> ();
    private int[][] songPitch;
    private int progress = 0;
//    private bool keyhit = false;
    private int iPlayer = -1;
    private int jPlayer = -1;
    private float noteStamp = 0f;




=======
    public int beats = 60;
    public int beatsperbar = 4;
    public int basenote = 4;
    public AudioClip[] piano;
    public AudioClip[] volin;
    public 
>>>>>>> faca8666a0a7fa69cd989be895d13a11b552381a

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
