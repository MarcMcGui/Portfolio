using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
	
    // Public vriables, configure in inspector
	public float baseTimer = 3.0f; //Set the number of seconds between enemy units spawning
    public GameObject[] enemyUnits;
    public int totalFans;
    public int maxFans;
    public int numberofEnemies;
    public Text txt;
    public int fansDefeated;
	public bool isTutorial = false;

    // Internal/Private variables
    private bool isInfinite;
    private static GameManager gm;
    public float infiTimer;
    private float infiX;
    private Vector3[] lanes;
    private float edge;
	private float timer;
	private int tutDelay = 1000;

    // Use this for initialization
    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        isInfinite = gm.isInfinite; //get mode from gm (game manager)
		edge = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0)).x;
        lanes = gm.lanes;
		timer = baseTimer;
        gm.enemyMax = maxFans;
        fansDefeated = 0;
        if (isInfinite) {
            txt.text = "";
        } 
        infiX = 0; //x value for infinite mode funciton
        infiTimer = 50; //value to stor result of function
		numberofEnemies = enemyUnits.Length;
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("HERE:" + timer);
		if(isTutorial)
		{
			if (tutDelay < 0 && timer <= 0)
			{
				txt.text = "Fans Remaining: " + (maxFans - fansDefeated);
				//writes fans remaining only if the current level is not in infinite mode.
				if (totalFans < maxFans) Spawn ();
			}
			else if (timer <= 0)
			{
				tutDelay--;
			}
			timer -= Time.deltaTime;
		}
		else if (isInfinite == false)
		{
			txt.text = "Fans Remaining: " + (maxFans - fansDefeated);
			//writes fans remaining only if the current level is not in infinite mode.

			timer -= Time.deltaTime;
			if ((timer <= 0) && totalFans < maxFans) Spawn();
            else if ((timer <= 0) && totalFans == maxFans && maxFans != fansDefeated)
            {
                Spawn();
            }
		}
        else
        {
            infiX += Time.deltaTime;
            timer -= infiTimer*Time.deltaTime;
            if ((timer <= 0))
            {
                baseTimer = 4.0f;
                Spawn();
                infiTimer = 1+ (11 / (1 + Mathf.Pow(2.7f, -0.0375f * (infiX - 120))));
            }
        }
    }

    void Spawn () {
        totalFans += 1;
        gm.IncreaseEnemyCount();
        Vector3 location = lanes[Random.Range(0, lanes.Length)]; // Chooses a lane to spawn into
        location.x = edge;
        int type = Random.Range(0, numberofEnemies); //Currently spawns 1 types, [0,1,2,3] stored in enemyUnits
        GameObject t = Instantiate<GameObject>(enemyUnits[type], location, Quaternion.identity);
        timer = baseTimer;
    }

    public void increaseFansDefeated()
    {
        fansDefeated += 1;
    }
    
}