using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGPlay : MonoBehaviour {

    public Level3 Level;

	// Use this for initialization
	void Start () {

        StartCoroutine(BeatsPlay());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator BeatsPlay()
    {
        while (true)
        {
            GetComponent<AudioSource>().Play();
            yield return new WaitForSecondsRealtime(60 / Level.bpm);
        }

    }
}
