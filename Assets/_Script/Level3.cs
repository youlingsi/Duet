using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour {


    private float[] NOTES = new float[5] { 0.125f, 0.25f, 0.375f, 0.5f, 1f};
    //private string PATTERNPATH = "Assets/pattern.txt";
   // private string PITCHPATH = "Assets/pitch.txt";

    public TextAsset MelTime;
    public TextAsset MelPitch;
    public int nNotes = 4;
    public float unitNote = 0.25f;
    public int bpm = 60;
    public AudioClip[] piano;
    public AudioClip[] voilin;
    public AudioClip[] soundeffect;
    public AudioSource com;
    public AudioSource effects;
    public float tolerence = 0.2f;
    public Anime animecontrol;

    private int state = 0; //0= computer play. 1= playerplay 2 = played;
    private float unitTime = 0;
    private List<List<string>> songTime = new List<List<string>>();
    private List<List<string>> songPitch = new List<List<string>>();
    private int progress = 0;
    private int iPlayer = -1;
    private int jPlayer = -1;
    private float noteStamp = 0f;
    private int correct;
    private int wrong;


    // Use this for initialization
    void Start () {
        songTime = Readfile(MelTime);
        songPitch = Readfile(MelPitch);
        unitTime = 60 / bpm;
        StartCoroutine(Play());
    }
	
	// Update is called once per frame
	void Update () {
        if (state == 1 & Input.GetKeyDown("f"))
        {
            float hitStamp = Time.realtimeSinceStartup;
            if (hitStamp - noteStamp < tolerence)
            {
                PlayerPlay(iPlayer, jPlayer);
                state = 2;
                animecontrol.Jump();
                correct++;
            }
            else
            {
                print("wrong" + (hitStamp - noteStamp).ToString());
                EffectPlay(0);
                animecontrol.Fall();
                state = 2;
                wrong++;
            }
        }

    }

    IEnumerator Play()
    {
        while(progress < songTime.Count)
        {
            for(int j = 0; j < songTime[progress].Count; j++)
            {
                state = 0;
                GetComponent<AudioSource>().Stop();
                iPlayer = -1;
                jPlayer = -1;
                // indicate the starting point
                //if(j == songTime[progress].Count - 1)
                //{
                //    print(progress.ToString() + ", " + j.ToString());
                //    ComPlay(ConvertPitch(songPitch[progress][j]), 1f);
                //}
                //else
                //{
                //    print(progress.ToString() + ", " + j.ToString());
                //    ComPlay(ConvertPitch(songPitch[progress][j]), 0.1f);
                //}
                ComPlay(ConvertPitch(songPitch[progress][j]), 1f);
                yield return new WaitForSecondsRealtime(unitTime * NOTES[int.Parse(songTime[progress][j])]);
                com.Stop();

            }
            yield return new WaitForSecondsRealtime(60/bpm);
            EffectPlay(1);
            correct = 0;
            wrong = 0;
            yield return new WaitForSecondsRealtime(0.2f);

            for (int j = 0; j < songTime[progress].Count; j++)
            {
                state = 1;
                noteStamp = Time.realtimeSinceStartup;
                iPlayer = progress;
                jPlayer = j;
                yield return new WaitForSecondsRealtime(unitTime * NOTES[int.Parse(songTime[progress][j])]);
                if(state ==1)
                {
                    EffectPlay(0);
                    animecontrol.Fall();
                    wrong++;
                }
            }
            if(correct > wrong)
            {
                EffectPlay(2);
            }
            else
            {
                EffectPlay(3);
            }
            yield return new WaitForSecondsRealtime(60 / bpm);
            progress++;
        }
    }

    void PlayerPlay(int i, int j)
    {
        AudioSource player = GetComponent<AudioSource>();
        player.Stop();
        player.clip = voilin[ConvertPitch(songPitch[i][j])];
        player.Play();
    }

    //Generate a bar of the note times
    //List<int> PatternGenerator(int nNotes, float unitNote) {
    //    float barSum = unitNote * nNotes;
    //    float sum = 0;
    //    List<string> pattern = new List<string>();
    //    List<int> intPattern = new List<int>();
    //    while (sum < barSum)
    //    {
    //        int noteIndx = NoteGenerator(barSum, sum);
    //        intPattern.Add(noteIndx);
    //        pattern.Add(noteIndx.ToString());
    //        sum += NOTES[noteIndx];
    //    }
    //    System.IO.StreamWriter writer = new System.IO.StreamWriter(PATTERNPATH, true);
    //    writer.WriteLine(string.Join(",", pattern.ToArray()));
    //    writer.Close();
    //    return intPattern;
    //}
    
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

    // computer play the notes
    void ComPlay(int pitch, float volumn)
    {
        com.Stop();
        print(pitch);
        com.clip = piano[pitch];
        com.volume = volumn;
        com.Play();
    }

    List<List<string>> Readfile(TextAsset file)
    {
        string text = file.text;
        string[] line = text.Split('\n');
        List<List<string>> ReturnFile = new List<List<string>>();

        foreach(string oneline in line)
        {
            string[] txtarr = oneline.Split(',');
            if (!txtarr[0].Contains("//"))
            {
                List<string> txt = new List<string>();
                foreach (string n in txtarr)
                {
                    txt.Add((n));
                }
                ReturnFile.Add(txt);
            }
        }
        return ReturnFile;
    }

    int ConvertPitch(string note)
    {
        int index = 0;
        switch (note[0])
        {
            case 'c':
                index = 1 + 12 * (int.Parse(note[1].ToString()) - 2);
                break;
            case 'd':
                index = 3 + 12 * (int.Parse(note[1].ToString()) - 2);
                break;
            case 'e':
                index = 5 + 12 * (int.Parse(note[1].ToString()) - 2);
                break;
            case 'f':
                index = 6 + 12 * (int.Parse(note[1].ToString()) - 2);
                break;
            case 'g':
                index = 8 + 12 * (int.Parse(note[1].ToString()) - 2);
                break;
            case 'a':
                index = 10 + 12 * (int.Parse(note[1].ToString()) - 2);
                break;
            case 'b':
                index = 12 * (int.Parse(note[1].ToString()) - 1);
                break;
            default:
                index = 0;
                break;
        }        
        if (note[2] == 's')
        {
            return index + 1;
        }
        else if (note[2] == 'b')
        {
            return index - 1;
        }
        else
        {
            return index;
        }
    }

    void EffectPlay(int indx)
    {
        effects.clip = soundeffect[indx];
        effects.Play();
    }
}
