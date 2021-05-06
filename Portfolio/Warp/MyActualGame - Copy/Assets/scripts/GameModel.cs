﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// we need these namespaces for serialization
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


public class GameModel : MonoBehaviour
{
    // reference to this game model as a singleton--never use this, just use GameModel.Instance
    private static GameModel mSingleton;
    
    public float[] savedValues = new float[5];
    // the (static) property to grab the instance with, 
    // notice that it's read-only
    public static GameModel Instance { get { return mSingleton; } }

    // public variable that can be dragged in the editor over here
    public ContainerBehavior player;
    // name of the file to read from and write to
    public string saveFileName;

   // void Awake()
   // {
        // set up the Singleton
       // if (mSingleton == null)
      //  {
        //    mSingleton = this;
        //    DontDestroyOnLoad(gameObject);
       // }
       // else {
            // if this isn't the first copy, get rid of it
           // Destroy(gameObject);
       // }
  //  }

    // this can be passed to a button that calls this method when clicked
    public void OnCheckPointSave()
    {
        SaveGame saveGame = new SaveGame();
        Debug.Log(Application.persistentDataPath);

        // then write it out
        SaveGameModel(saveGame, saveFileName);
    }

    // this can be passed to a button that calls this method when clicked
    public void OnLoadClick()
    {
        // load game does all the work of calling loaddata
        if (File.Exists("SavedData.txt"))
        {
            SceneManager.LoadScene(1);
            System.IO.StreamReader data = new System.IO.StreamReader(@"SavedData.txt");
            string dataToLoad = data.ReadLine();
            Debug.Log(dataToLoad);
            string[] dataToLoadString = dataToLoad.Split(' ');

            for (int i = 0; i < savedValues.Length; i++)
            {
                savedValues[i] = float.Parse(dataToLoadString[i]);
                Debug.Log("part" + i + " " + savedValues[i]);
            }
            
            player.maxHealth = savedValues[0];
            player.vialCounter = savedValues[1];
            player.position = new Vector3(savedValues[2], savedValues[3], 0);
            
            data.Close();

        }
        else
        {
            SceneManager.LoadScene(1);
        
        }
        
       
    }


    public void SaveGameModel(SaveGame save, string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        // then create a file stream that can be opened or created, with write access to it
        FileStream fs = File.OpenWrite(Application.persistentDataPath + "/" + filename + ".dat");

        // note that we store the data from our game model (this object)
        // first in the SaveGame instance, think of SaveGame like a file
        save.StoreData(this);

        // then we can serialize it to the disk using Serialize and
        // we serialize the SaveGame object. 
        bf.Serialize(fs, save);

        // close the file stream
        fs.Close();
    }

    public void LoadGame(string filename)
    {
        Debug.Log("is loading");
        SceneManager.LoadScene(1);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.OpenRead(Application.persistentDataPath + "/" + filename + ".dat");

        // deserialize the save game--this will throw an exception if we can't
        // deserialize from the file stream
        SaveGame saveGame = (SaveGame)bf.Deserialize(fs);

        // we assume we have access to the game model
        saveGame.LoadData(this);

        // close the file stream
        fs.Close();
    }
}