using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour {
    public BossBehavior boss;
    public Scrollbar scroll;
    // Use this for initialization
    void Start () {
        boss = FindObjectOfType<BossBehavior>();
        scroll = GetComponent<Scrollbar>();
    }
	
	// Update is called once per frame
	void Update () {
        if( boss.isDead)
        {
            Destroy(gameObject);
        }
        scroll.size = ((float)boss.health) / 100;
	}
}
