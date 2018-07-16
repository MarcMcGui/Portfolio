using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBouncers : MonoBehaviour
{

    public GameObject bouncer;
    int clearance = 0;
    int heightGenerated = 0;

    // Use this for initialization
    void Start()
    {
        heightGenerated = (int)transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        clearance = (int)transform.position.y + 300;
        if ((clearance - heightGenerated) > 10)
        {
            float xPos = Random.Range(-8, 8);
            float yPos = Random.Range(heightGenerated, heightGenerated + 10);
            heightGenerated += 10;
            GameObject.Instantiate(bouncer, new Vector3(xPos, yPos, 0), Quaternion.identity);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if((collisionInfo.gameObject.transform.position.y + 0.5) < gameObject.transform.position.y)
        {
            if(collisionInfo.gameObject.name.Contains("Bounce"))
            GameObject.Destroy(collisionInfo.gameObject);
        }
    }
}
