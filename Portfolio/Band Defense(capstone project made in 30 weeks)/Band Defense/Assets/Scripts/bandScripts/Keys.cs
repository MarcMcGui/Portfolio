using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : BandMember {

	// Public variables
	public GameObject projectile;
 
	// Internal variables
	private float timer;

	// Use this for initialization
	protected override void Start () {
        MaxTimer = 2.0f;
        timer = MaxTimer;
        initialTimer = MaxTimer;
		base.Start();
        lane = int.Parse(positions[4]);
        buffTimer = MaxTimer / 2;
		maxSpeed = 15.0f;
		brakeFactor = 18.0f;
        int.Parse(positions[4]);
    }

	// Update is called once per frame
	protected override void Update () {
		timer -= Time.deltaTime;

        //Check time
        if (timer <= 0)
        {
            Fire(false);
            if (buffed == false)
            {
                timer = MaxTimer;
            }
            else
            {
                timer = buffTimer;
            }
        }

        base.Update();

        if (buffed)
        {
            sprite.sprite = buffSprite;
            sprite.color = initialColor;
        }
        else
        {
            sprite.sprite = initialSprite;
        }
    }

    public override void Fire(bool rhythmBuff = false)
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }

    public override void RythmBreak()
    {
        MaxTimer = initialTimer;
    }
}
