using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour {
    public GameModel game;
    public Button self;
    public static LoadGameButton singleton;
	// Use this for initialization
	void Start () {
        game = FindObjectOfType<GameModel>();
	}
	
	// Update is called once per frame
	void Update () {
      
             
	}
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }  
}
