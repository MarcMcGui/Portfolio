using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Public variables
    public int life;
    public bool isShaking;
    public Vector3[] lanes;
    public BandMember[] bandMembers;
	public GameObject[] laneIcons;
    public int enemyMax;
    public bool isInfinite; //determines if the level is in infinite mode or not.

    // Internal variables
    protected Vector3 cameraStart;
	protected Spawner spawn;
	protected int totalEnemies;
    [SerializeField]
	protected PlayerMovement player;
    [SerializeField]
	protected GameObject stage;

    private void Awake()
    {
        float screenHeight = (Camera.main.pixelHeight - 100);
		float laneHeight = (screenHeight-100) / lanes.Length;
        float offset = laneHeight / 2;
        //float otherOffset = offset + 40;
        //offset += 40;
        for (int i = 0; i < lanes.Length; i++)
        {
          
            lanes[i] = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, (laneHeight * i + offset) + 150, 0));
            lanes[i].z = 0;
            lanes[i].x = 0;
           
        }
		for (int i = 0; i < lanes.Length; i++)
		{
			laneIcons [i].GetComponent<SpriteRenderer> ().enabled = true;
			Vector3 NewPos = laneIcons [i].transform.position;
			NewPos.y = lanes [i].y;
			laneIcons [i].transform.position = NewPos;
		}
		for (int i = lanes.Length; i < laneIcons.Length; i++)
		{
			laneIcons [i].GetComponent<SpriteRenderer> ().enabled = false;
		}
    }

    // Use this for initialization
    void Start()
    {
        spawn = FindObjectOfType<Spawner>();
        //read in data from infiite file to determine whether or not level should be infinite.
        System.IO.StreamReader data = new System.IO.StreamReader(@"infinite.txt");
        string dataToLoad = data.ReadLine();
        if (dataToLoad == "True")
        {
            isInfinite = true;
        }
        else isInfinite = false;
        data.Close();
        isShaking = false;
        cameraStart = Camera.main.transform.position;
        totalEnemies = 0;
        bandMembers = FindObjectsOfType<BandMember>();
        player = FindObjectOfType<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!(spawn.totalFans < spawn.maxFans) && isInfinite == false)
        {
            checkEnemies();
        }
        
        if ( isShaking)
        {
            ScreenShake();
        } 
        else
        {
            Camera.main.transform.position = cameraStart;
        }
        
        if ( Input.GetAxis( "next") > 0  )
        {
            System.IO.StreamReader data = new System.IO.StreamReader(@"level.txt");
            string dataToLoad = data.ReadLine();
            int thenextlevel = int.Parse(dataToLoad) + 1;
            data.Close();
            System.IO.File.WriteAllText("level.txt", thenextlevel + "");
            SceneManager.LoadScene("BandPlacement");
        }

    }

    public BandMember getBandByLane(int ln)
    {
        for (int i = 0; i < bandMembers.Length; i++)
        {
            if (bandMembers[i].lane == ln && bandMembers[i] != player)
            {
                return bandMembers[i];
            }
        }
        return null;
    }

    public void IncreaseEnemyCount()
    {
        totalEnemies += 1;
    }

    void checkEnemies()
    {
        if (!(spawn.totalFans < spawn.maxFans))
        {
            
            System.IO.StreamReader data = new System.IO.StreamReader(@"level.txt");
            string dataToLoad = data.ReadLine();
            int thenextlevel = int.Parse(dataToLoad) + 1;
            data.Close();
            if (int.Parse(dataToLoad) >= 3  )
            {
                SceneManager.LoadScene("Ending");
            }
            else
            {
               
                System.IO.File.WriteAllText("level.txt", thenextlevel + "");
                SceneManager.LoadScene("BandPlacement");
            }
            
        }
    }

    void ScreenShake()
    {
        float y = Random.Range(-0.4f, 0.4f);
        float x = Random.Range(-0.4f, 0.4f);
        Vector3 campos = Camera.main.transform.position;
        Vector3 trans = new Vector3(x, y, -10);
        if ( 
            (((campos.x + trans.x) < (cameraStart.x + 0.5f)) && (campos.x + trans.x) > (cameraStart.x - 0.5f)) &&
            (((campos.y + trans.y) < (cameraStart.y + 0.5f)) && (campos.y + trans.y) > (cameraStart.y - 0.5f)))
        {
            Camera.main.transform.position = Vector3.MoveTowards(campos, trans, 0.2f);
        }
      

    }

    
}
