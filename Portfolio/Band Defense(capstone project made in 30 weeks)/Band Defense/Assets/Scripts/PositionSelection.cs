using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PositionSelection : MonoBehaviour {

	// Public variables
    public GameObject[] values;
    public string[] vals;
    public int level;
    public Button submitButton;
    public int totalValue;

	// Use this for initialization
	void Start () {
        
        System.IO.StreamReader data = new System.IO.StreamReader(@"level.txt");
        string dataToLoad = data.ReadLine();
        level = int.Parse(dataToLoad);
        data.Close();
        for (int i = 0; i < values.Length; i++)
        {
            values[i].SendMessage("Setlevel", level);
        }
		if (level == 999) {
			vals = new string[1];
			vals [0] = "-1";
		} else {
			vals = new string[level + 2];
			for (int i =0; i < vals.Length; i++)
			{
				vals[i] = "-1";
			}
		}
    }
	
	// Update is called once per frame
	void Update () {

        submitButton.interactable = true;

        switch (level)
        {
            case 1:
                totalValue = int.Parse(vals[0]) + int.Parse(vals[1]) + int.Parse(vals[2]);
                values[4].SetActive(false);
                values[3].SetActive(false);

                if (ArrayUtility.Contains<string>( vals,"-1"))
                {
                    submitButton.interactable = false;
                }
                
                break;
            case 2:
                totalValue = int.Parse(vals[0]) + int.Parse(vals[1]) + int.Parse(vals[2]) 
                    + int.Parse(vals[3]);

                values[4].SetActive(false);
                if (ArrayUtility.Contains<string>(vals, "-1"))
                {
                    submitButton.interactable = false;
                }
                break;
			case 999:
				values [4].SetActive (false);
				values [3].SetActive (false);
				values [2].SetActive (false);
				values [0].SetActive (false);
				if (ArrayUtility.Contains<string>(vals, "-1"))
				{
					submitButton.interactable = false;
				}
				break;
            default:
                totalValue = int.Parse(vals[0]) + int.Parse(vals[1]) + int.Parse(vals[2])
                   + int.Parse(vals[3]) + int.Parse(vals[4]);
                if (ArrayUtility.Contains<string>(vals, "-1"))
                {
                    submitButton.interactable = false;
                }
                break;
        }



        for (int i = 0; i < vals.Length; i++)
        {
            for (int j = 0; j < vals.Length; j++)
                if (i != j && vals[i] == vals[j])
                {
                    submitButton.interactable = false;
                }
        }
     
       
       
       
    }

    public void Submit()
    {
        System.IO.File.WriteAllText("lanes.txt", "");
        for (int i = 0; i < vals.Length; i++)
        {
            System.IO.File.AppendAllText("lanes.txt", vals[i] + ",");
        }
		if (level == 999) {
			SceneManager.LoadScene ("Tutorial");
		} else {
			SceneManager.LoadScene(level);
		}
        
    }

}
