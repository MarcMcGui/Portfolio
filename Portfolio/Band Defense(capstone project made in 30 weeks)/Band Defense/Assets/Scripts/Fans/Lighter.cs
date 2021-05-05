using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : FanBehavior {

	// Public variables
    public GameObject thrownLight;
    public float range; // The range from the stage needed to start throwing lighters

	// Internal variables
	private GameObject myLighter;
    private bool canMove = true;

	// Use this for initialization
	protected override void Start () {

        base.Start();

	}

    // Update is called once per frame
    protected override void Update() {
        if (transform.position.x >= stage.transform.position.x + range) // checks position relative to stage
        {
            base.Update();
        }
        else // If close enough, throws lighter at stage and stops moving
        {
            GetComponent<Rigidbody2D>().isKinematic = false; // Set physics off as to not impede other fans
            if (myLighter == null)
            {
                ThrowLighter(); //throw lighter as soon as previous lighter is destroyed.
            }
        }
    }

    void ThrowLighter() {
        myLighter = Instantiate(thrownLight, transform.position, Quaternion.identity);
        myLighter.GetComponent<Rigidbody2D>().AddForce(new Vector3(-450, 200, 0));
    }
}