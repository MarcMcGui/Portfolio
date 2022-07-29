using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBar : MonoBehaviour {

	// Public variables
    public GameObject icon;
    public float timer;
    public float index;
    public bool cue = false;

	// Internal variables
	protected int level;
	protected NoteSequencer noteSequencer;
	protected float bpm;
	protected float timeSigTop;

    //(bpm/timeSigTop)/60f seconds should be the travel time from spawn to bar.

    // Use this for initialization
    void Start () {
        System.IO.StreamReader data = new System.IO.StreamReader(@"level.txt");
        string dataToLoad = data.ReadLine();
        level = int.Parse(dataToLoad);
        data.Close();
        timer = 3;
        noteSequencer = GetComponentInParent<NoteSequencer>();
        bpm = (float)noteSequencer.bpm;
        timeSigTop = (float)noteSequencer.timeSigTop;
	}
	
	// Update is called once per frame
	void Update () {
        if (index  + 1 > level + 2)
        {
            gameObject.SetActive(false);
        }
		if (level == 999 && index != 0) {
			gameObject.SetActive(false);
		}
        timer -= Time.deltaTime;
        int rnd = Random.Range(0, 4);
		if (timer < 0)
        {
            if (rnd == 2)
            {
                //Instantiate(icon, transform.position, Quaternion.identity);
            }
            timer = 3;
           
        }
        if (cue)
        {
            Instantiate(icon, transform.position, Quaternion.identity);
            cue = false;
        }
	}

    public void SpawnNote()
    {
        Instantiate(icon, transform.position, Quaternion.identity);
    }
}
