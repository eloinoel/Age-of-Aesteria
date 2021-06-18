using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneral : MonoBehaviour {
    public bool onLeftPlayerSide = true;
    public int health = 100;

    private float lifeTime = -1.0f;

    void Start() {
        
    }

    void Update() {
        // TODO - handle gameEndMessage/Event for Bases i.e. maybe call onDeath Function from other script or something
        if(lifeTime <= 0.0f && lifeTime != -1.0f) { Destroy(this.gameObject); }
        if(lifeTime != -1.0f) { lifeTime -= Time.deltaTime; }
    }

    // if you want to kill your boy...
    public void die() {
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<Animator>().SetTrigger("Death");
        lifeTime = 3.0f;
    }
}
