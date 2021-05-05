using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FanBehavior : MonoBehaviour {

	// Public variables
    public float speed;
    public int life;
	public float damage;
    public Spawner spawn;

    // Internal variables
    protected Stage stage;
    bool hasTouchedStage;
    BoxCollider2D collide;
    SpriteRenderer sprite;
    bool isFrozen;
    float freezeTimer;

	// Use this for initialization
	protected virtual void Start () {

        isFrozen = false;
        freezeTimer = 0.0f;
        collide = GetComponent<BoxCollider2D>();
        stage = FindObjectOfType<Stage>();
        spawn = FindObjectOfType<Spawner>();
        sprite = GetComponent<SpriteRenderer>();
   
	}
	
	// Update is called once per frame
	protected virtual void Update () {

        if (collide.IsTouching(stage.GetComponent<BoxCollider2D>())) { // Checks to see if the fan is currently touching the stage
            hasTouchedStage = true; // If so set hasTouchedStage to true
            stage.life -= damage; // Remove life from stage for as long as fan is touching
            FindObjectOfType<GameManager>().isShaking = true; // Also shake screen
        }
        else 
        {
            if(hasTouchedStage)
            {
                // If The fan has touched the stage but stops, stop the screen shake.
                FindObjectOfType<GameManager>().isShaking = false;
            }
        }

        
        if (isFrozen == false){
            transform.position += (Vector3.left / 15) * speed;
        }
        
        if (life <= 0) {
            Die();
        }

        if (isFrozen) {
            freezeTimer -= 1.0f;
            if (freezeTimer <= 0.0f) {
                isFrozen = false;
            }
        }
	}

    public virtual void Die() {
        FindObjectOfType<GameManager>().isShaking = false;
        //Once the fan dies turn off the screen shake and increase the count in fans defeated
        spawn.increaseFansDefeated(); 
        GameObject.Destroy(this.gameObject);
        //finally destroy object
    }

	public virtual void Hurt(int damage) {
        // Everytime the fan takes damage, change sprite color to look more red.
        Color og = sprite.color;
        sprite.color = new Color(og.r + 0.20f, og.g - 0.20f, og.b - 0.20f);
        life -= damage; // Also remove life.
    }

    public virtual void Freeze() {
        isFrozen = true;
        freezeTimer = 50;
    }
}