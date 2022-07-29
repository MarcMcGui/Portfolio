using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BandMember {

    // Public variables
    public GameObject projectile;
    public float timer;

    // Internal variables
    bool canWalk;
    bool canFire;
    GameObject myProjectile;

    // Use this for initialization
    protected override void Start()
    {
        canFire = true;
        canWalk = true;
        timer = 0;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        switch ((int) Input.GetAxisRaw("Vertical"))
        {
            case -1:
                if (lane != 0 && timer <= 0)
                {
                    lane--;
                    timer = 0.1f;
                }
                break;
            case 1:
                if (lane != lanes.Length - 1 && timer <= 0)
                {
                    lane++;
                    timer = 0.1f;
                }
                break;
            default:
                break;
        }

        if ( timer > 0 )
        {
            timer -= Time.deltaTime;
        }

        if ( Input.GetButtonUp("Jump") && canFire)
        {
            if ( canFireMove)
            {
                Fire();
            }
        }

        if (canFire == false )
        {
            if (myProjectile == null)
            {
                canFire = true;
            }
        }

        base.Update();
    }

    public override void Fire(bool rhythmBuff = false)
    {
        myProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        canFire = false;
    }
}
