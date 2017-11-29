using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour {


    private float[] NOTES = new float[5] { 0.125f, 0.25f, 0.375f, 0.5f, 1f};
    private string PATTERNPATH = "Assets/pattern.txt";
    private string PITCHPATH = "Assets/pitch.txt";
    private TextAsset SongTime;
    private TextAsset SongPitch;

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
    private int[][] songPitch;
    private int progress = 0;
//    private bool keyhit = false;
    private int iPlayer = -1;
    private int jPlayer = -1;
    private float noteStamp = 0f;


    // Use this for initialization
    void Start () {
        System.IO.File.WriteAllText(PATTERNPATH, string.Empty);      
        for (int i = 0; i <barNum; i++)
        {
            songTime.Add(PatternGenerator(nNotes, unitNote));
        }
        songPitch = PitchGenerator(songTime);
        unitTime = 60 / bpm * nNotes;
        StartCoroutine(Play());
    }
	
	// Update is called once per frame
	void Update () {
        if(state == 1 & Input.GetKeyDown("f"))
        {
            float hitStamp = Time.realtimeSinceStartup;
            if(hitStamp - noteStamp < tolerence)
            {
                PlayerPlay(iPlayer, jPlayer);
            }
            else
            {
                print("wrong" + (hitStamp-noteStamp).ToString());
            }
        }

	}

    IEnumerator Play()
    {
        while(progress < barNum)
        {
            for(int j = 0; j < songTime[progress].Count; j++)
            {
                state = 0;
                GetComponent<AudioSource>().Stop();
                iPlayer = -1;
                jPlayer = -1;
                print("i" + progress.ToString() + " " + "j" + j.ToString());
                if(j == songTime[progress].Count - 1)
                {
                    ComPlay(songPitch[progress][j], 1f);
                }
                else
                {
                    ComPlay(songPitch[progress][j], 0.3f);
                }

                yield return new WaitForSecondsRealtime(unitTime * NOTES[songTime[progress][j]]);
            }
            //yield return new WaitForSecondsRealtime(60/bpm);

            for (int j = 0; j < songTime[progress].Count; j++)
            {
                state = 1;
                noteStamp = Time.realtimeSinceStartup;
                print("wait");
                iPlayer = progress;
                jPlayer = j;
                yield return new WaitForSecondsRealtime(unitTime * NOTES[songTime[progress][j]]);


            }
            progress++;
        }
    }

    void PlayerPlay(int i, int j)
    {
        AudioSource player = GetComponent<AudioSource>();
        player.Stop();
        player.clip = voilin[songPitch[i][j]];
        player.Play();
    }

    //Generate a bar of the note times
    List<int> PatternGenerator(int nNotes, float unitNote) {
        float barSum = unitNote * nNotes;
        float sum = 0;
        List<string> pattern = new List<string>();
        List<int> intPattern = new List<int>();
        while (sum < barSum)
        {
            int noteIndx = NoteGenerator(barSum, sum);
            intPattern.Add(noteIndx);
            pattern.Add(noteIndx.ToString());
            sum += NOTES[noteIndx];
        }
        System.IO.StreamWriter writer = new System.IO.StreamWriter(PATTERNPATH, true);
        writer.WriteLine(string.Join(",", pattern.ToArray()));
        writer.Close();
        return intPattern;
    }
    
    //Generate a value of time based on defined tempo.
    int NoteGenerator(float barSum, float sum)
    {
        int indx = Random.Range(0, NOTES.Length);
        float note = NOTES[indx];
        if (sum + note > barSum)
        {
            return NoteGenerator(barSum, sum);
        }
        else
        {
            return indx;
        }

    }

    // Generate the pitch based on the pattern file
    int[][] PitchGenerator(List<List<int>> timesheet)
    {
        int[][] pitch = new int[barNum][];
        for(int i = 0; i < barNum; i++)
        {
            pitch[i] = new int[timesheet[i].Count];
            for (int j = 0; j < timesheet[i].Count; j++)
            {
                pitch[i][j] = Random.Range(0, 8);
            }
        }
        return pitch;

    }

    // computer play the notes
    void ComPlay(int pitch, float volumn)
    {
        com.Stop();
        com.clip = piano[pitch];
        com.volume = volumn;
        com.Play();
    }

    List<List<int>> Readfile(TextAsset file)
    {
        string text = file.text;
        
    }
}
