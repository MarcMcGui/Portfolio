using UnityEngine;
using System.Collections;
using System.IO;


public class CheckPointBehavior : MonoBehaviour  {
    public PlayerBehavior player;
    public GameModel game;
    public SaveGame save;
	// Use this for initialization
	void Start () {
        game = FindObjectOfType<GameModel>();
        player = FindObjectOfType<PlayerBehavior>();
      //  game = new GameModel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D col )
    {
        if( col.name == "PlayerCharacter")
        {
            player.cont.PlayerChanged(player.maxHealth, player.vialCounter, player.transform.position);
          //  game.OnCheckPointSave();
            string fileString = "";
            fileString = fileString + player.maxHealth + " " + player.vialCounter + " " + player.transform.position.x + " " +
                player.transform.position.y + " " + player.transform.position.y;
            //System.IO.StreamWriter saveFile = new System.IO.StreamWriter(@"SavedData.txt");
            //saveFile.WriteLine(fileString);
            System.IO.File.WriteAllText("SavedData.txt", fileString);
            Destroy(gameObject);
           // saveFile.Close();
            

        }
    }
}
