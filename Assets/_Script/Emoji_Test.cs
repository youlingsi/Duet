using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class Emoji_Test : MonoBehaviour
{

    private const string url = "http://grantwu.me:5000/galaga_emoji";
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

            setNextType(www.text);

            yield return new WaitForSeconds(5.0f);
        }
    }
    void setNextType(string text)
    {
        var N = JSON.Parse(text);
        int pogChamp = N["PogChamp"];
        int triHard = N["TriHard"];
        int kappa = N["Kappa"];

        if (kappa > triHard && kappa > pogChamp)
        {
            type = 1;
            print("Current wave: Kappa");
        }
        if (triHard > kappa && triHard > pogChamp)
        {
            type = 2;
            print("Current wave: TriHard");
        }
        if (pogChamp > kappa && pogChamp > triHard)
        {
            type = 3;
            print("Current wave: PogChamp");
        }
    }

	// Update is called once per frame
	void Update () {

	}
}
