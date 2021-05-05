using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BandMember : MonoBehaviour {

	// Public variables
    public int lane = 0;
    public float MaxTimer;
    public bool buffed;
    public bool drumFlag;
    public Sprite buffSprite;
    public BandMember above;
    public BandMember below;
    public GameObject cooldown;
    public string[] positions;
    public ParticleSystem buffPart;
    public float initialTimer;

    // Internal variables
    [SerializeField]
    [Range(0f, 25f)]
    protected float maxSpeed;
    [SerializeField]
    [Range(1f, 20f)]
    protected SpriteRenderer sprite;
    protected float brakeFactor;
    protected bool canFireMove;
    protected Color initialColor;
    protected Sprite initialSprite;
    protected float buffTimer;
    protected static GameManager gm;
    protected Vector3[] lanes;
    protected bool xSet;
    

    // Use this for initialization
    protected virtual void Start () {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        lanes = gm.lanes;
        drumFlag = false;
        xSet = false;
        sprite = GetComponent<SpriteRenderer>();
        initialColor = sprite.color;
        initialSprite = sprite.sprite;
        System.IO.StreamReader data = new System.IO.StreamReader(@"lanes.txt");
        string dataToLoad = data.ReadLine();
        positions = dataToLoad.Split(',');
        data.Close();

    }

    protected virtual void Awake () {
        
    }

    // Update is called once per frame
    protected virtual void Update() { 
        if (transform.position.y != lanes[lane].y) {
            for (int i = 0; i < lanes.Length; i++) {
                lanes[i].x = transform.position.x;
            }
            canFireMove = false;
            xSet = true;
            SeekLane(lane);
        }

        if (transform.position.y != lanes[lane].y)
        {
            canFireMove = false;
        }
        else canFireMove = true;

        if (lane + 1 < lanes.Length)
        {
            above = gm.getBandByLane(lane + 1);
        }
        else above = null;
        if (lane - 1 >= 0)
        {
            below = gm.getBandByLane(lane - 1);

        }
        else below = null;

        if ((below != null && below.drumFlag) || (above != null && above.drumFlag))
        {
            buffed = true;
        }
        else buffed = false;

        
    }

    abstract public void Fire(bool rhythmBuff = false);

    protected void SeekLane (int target) {

        float toTarget = Mathf.Abs(lanes[target].y - transform.position.y);
        float step = Mathf.Min(toTarget / brakeFactor, maxSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, lanes[target], step);
    }

    public virtual void RythmBreak() {

    }
}