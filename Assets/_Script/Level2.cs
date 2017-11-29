using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour {


    public AudioClip[] piano;
    public AudioClip[] String;
    public AudioClip ding;
    public AudioSource com;
    private float tNote;
    private float tCom;
    private float tPly;
    private int indx;
    private int[] RandNoteHis = new int[3] { 0, 0, 0 };
    private float[] lenth = new float[] { 0.25f, 0.5f, 1.0f, 2f };
    private int lenthIndex = 1;
    private float playStart = 0f;
    private float playerPlay = 0f;
    private int state = 0; // 0-complay; 1-player play
    private float span = 4f;
    private List<int> MelodyPitch = new List<int> { };
    private List<float> MelodyTime = new List<float> { };


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (tNote > lenth[lenthIndex] & state ==0)
        {
            indx = Random.Range(0, 7);
            lenthIndex = Random.Range(0, lenth.Length);
            RandomNote(indx);
            tNote = 0;
            tCom += lenth[lenthIndex];
            MelodyPitch.Add(indx);
            MelodyTime.Add(tCom);

            if (tCom > span) {
                state = 1;
                tCom = 0;
                com.clip = ding;
                com.Play();
                playStart = Time.realtimeSinceStartup;
            }
        }
        else {
            tNote += Time.deltaTime;
        }
        if (state == 1)
        {
            if (Input.GetKeyDown("f"))
            {
                PlayNote(MelodyPitch,MelodyTime);
            }

        }

    }

    void RandomNote(int indx)
    {
        com.clip = piano[indx];
        com.Play();
    }

    void PlayNote(List<int> pitch, List<float>time)
    {
		playerPlay = Time.realtimeSinceStartup;
        float diff = playerPlay - playStart;
        for (int i = 0; i< pitch.Count; i++)
        {
            if(diff < time[i])
            {
                if((time[i] - diff) < 0.2)
                {
                    AudioSource Source = GetComponent<AudioSource>();
                    Source.clip = String[pitch[i]];
                    Source.Play();
                }
            }
        }
        if (diff > time[time.Count-1]) { state = 0; }

    }
}
