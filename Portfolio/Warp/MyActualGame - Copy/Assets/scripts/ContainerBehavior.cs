using UnityEngine;
using System.Collections;

public class ContainerBehavior : MonoBehaviour {
    public float maxHealth;
    public Vector3 position;
    public float vialCounter;
    public static ContainerBehavior singleton;
	// Use this for initialization
	void Start () {
        maxHealth = 50;
        position = new Vector3(-86.8f, -52.9f, 0);
        vialCounter = 0; 
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayerChanged(float h, float vC, Vector3 pos)
    {
        maxHealth = h;
        position = pos;
        vialCounter = vC;
    }
    void Awake()
    {
         if (singleton == null)
         {
           singleton = this;
           DontDestroyOnLoad(gameObject);
         }
        else {
   //  if this isn't the first copy, get rid of it
            Destroy(gameObject);
        }
    }
}
