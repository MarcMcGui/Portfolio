using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowSelect : MonoBehaviour {

	// Public variables
    public Toggle[] lanemarks;
    public PositionSelection parent;
    public int level;
    public int index;
    public bool hasChecked = false;

    Toggle currentActive;
	// Use this for initialization
	void Start () {    
        parent = FindObjectOfType<PositionSelection>();
        level = parent.level;
	}
	
	// Update is called once per frame
	void Update () {
        //checks current level and sets lane selection buttons that are active accordingly
	   switch (level)
        {
            case 1:
                lanemarks[4].interactable = false;
                lanemarks[3].interactable = false;
                lanemarks[4].isOn = false;
                lanemarks[3].isOn = false;
                break;
            case 2:
                lanemarks[4].interactable = false;
                lanemarks[4].isOn = false;
                break;
            case 3:
                break;
			case 999:
				lanemarks[4].interactable = false;
				lanemarks[3].interactable = false;
				lanemarks[2].interactable = false;
				lanemarks[1].interactable = false;
				lanemarks[4].isOn = false;
				lanemarks[3].isOn = false;
				lanemarks[2].isOn = false;
				lanemarks[1].isOn = false;
				break;
            default:
                break;
        }

        for( int i = 0; i < lanemarks.Length; i++)
        {
            if (lanemarks[i].isOn && hasChecked == false)
            {
                currentActive = lanemarks[i];
                hasChecked = true;
                parent.vals[index] = "" + i;
            }
            else if ( lanemarks[i].isOn && lanemarks[i] != currentActive)
            {
                Deactivate(i);
                hasChecked = false;
            }
        }
	}

    public void Setlevel( int l)
    {
        level = l;
    }

    void Deactivate (int s)
    {
        for (int i = 0; i < lanemarks.Length; i++)
        {
            if (lanemarks[i] != lanemarks[s])
            {
                lanemarks[i].isOn = false;
            }
        }
        hasChecked = true;
    }
}
