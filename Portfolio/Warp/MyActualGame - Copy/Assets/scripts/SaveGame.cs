using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;

[Serializable]
public class SaveGame : ISerializable
{
    public float Health { get; set; }
    public float VialCount;
    public Vector3 playerPosition;

    // we have to copy information over from the game model into this class
    // because it is the one that is written to disk and read back from
    public void StoreData(GameModel model)
    {
        Health = model.player.maxHealth;
        VialCount = model.player.vialCounter;
        //playerScale = model.player.gameObject.transform.localScale;
        playerPosition = model.player.position;
        //playerOrientation = model.player.gameObject.transform.rotation;
    }

    // we pass in the game model here so that we can copy information back to
    // the model from this save game object
    public void LoadData(GameModel model)
    {
        model.player.maxHealth = Health;
        model.player.vialCounter = VialCount;
        //model.player.gameObject.transform.localScale = playerScale;
        model.player.gameObject.transform.position = playerPosition;
        //model.player.gameObject.transform.rotation = playerOrientation;
    }

    // this method is called when your object is serialized--this helps deal with some of the
    // issues around unity objects that aren't flagged as serializable
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("health", Health);
        info.AddValue("vials", VialCount);
        // Vector3, just like other Unity objects, is not serializable, so
        // we have to break it apart into three values, definitely a major pain
        info.AddValue("posx", playerPosition.x);
        info.AddValue("posy", playerPosition.y);
        info.AddValue("posz", playerPosition.z);
    }

    // we use the empty constructor when creating a save game before writing it to the disk
    public SaveGame()
    {
    }

    // this is a special constructor needed by ISerializable so that we can
    // construct the object from a stream--here we must fill out all the values
    // we saved to the file
    public SaveGame(SerializationInfo info, StreamingContext context)
    {
        Health = info.GetInt32("health");
        VialCount = info.GetInt32("vial");
        playerPosition = new Vector3(
            info.GetSingle("posx"),
            info.GetSingle("posy"),
            info.GetSingle("posz"));

    }

}