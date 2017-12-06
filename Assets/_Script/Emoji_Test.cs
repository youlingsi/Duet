using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class Emoji_Test : MonoBehaviour
{

    private const string url = "http://grantwu.me:5000/duet_emoji";
    private bool seen = false;
    public AudioSource com;
    public AudioClip[] clips;
    private string emoji = "";
    private int count = 0;
	// Use this for initialization
	void Start () {
		StartCoroutine(GetText());
	}
	
	IEnumerator GetText()
    {
        while(true)
        {
            WWW www = new WWW(url);
            yield return www;

            seen = false;
            setNextType(www.text);
            // print(www.text);

            yield return new WaitForSeconds(5.0f);
        }
    }
    void setNextType(string text)
    {
        var N = JSON.Parse(text);
        emoji = N["emoji"];
        print(emoji);
        count = N["count"];
        print(count);
    }

	// Update is called once per frame
	void Update ()
    {
        if (!seen)
        {
            seen = true;
            com.Stop();
            com.volume = 1f; // Do something with count here
            // if (emoji == "PogChamp")
            // For testing purposes I swapped in TriHard
            if (emoji == "TriHard")
            {
                com.clip = clips[0]; // Fill this in
                com.Play();
            }
            else if (emoji == "Kappa")
            {
                com.clip = clips[1];
                com.Play();
            }
            else if (emoji == "BibleThump")
            {
                com.clip = clips[2];
                com.Play();
            }
        }
	}
}
