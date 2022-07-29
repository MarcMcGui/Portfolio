using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void start()
    {
        System.IO.File.WriteAllText("infinite.txt", "False");
        System.IO.File.WriteAllText("level.txt", "1");
        SceneManager.LoadScene("BandPlacement");
    }

    public void modeSelect()
    {
        SceneManager.LoadScene("ModeSelect");
    }

	public void tutorial(){
		System.IO.File.WriteAllText("infinite.txt", "False");
		System.IO.File.WriteAllText("level.txt", "" + 999);
		SceneManager.LoadScene("BandPlacement");
	}

    public void startInfinite( int val)
    {
        System.IO.File.WriteAllText("infinite.txt", "True");
        System.IO.File.WriteAllText("level.txt", "" + val);
        SceneManager.LoadScene("BandPlacement");
    }

    public void LaneNumber()
    {
        SceneManager.LoadScene("LaneNumber");

    }

    public void menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

	public void instruction()
	{
		SceneManager.LoadScene("HowToPlay");
	}

	public void exit()
	{
		Application.Quit ();
	}

    public void bandMembers()
    {
        SceneManager.LoadScene("BandMembers");
    }
}
