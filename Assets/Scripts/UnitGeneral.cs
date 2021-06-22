using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneral : MonoBehaviour {
    public bool onLeftPlayerSide = true;
    public int health = 100;
    public float deathTime = 1.0f; // length of afterdeath animation

    private float lifeTime = -1.0f;

    void Start() {
        
    }

    void Update() {
        // TODO - handle gameEndMessage/Event for Bases i.e. maybe call onDeath Function from other script or something
        if(lifeTime <= 0.0f && lifeTime != -1.0f) { Destroy(this.gameObject); }
        if(lifeTime != -1.0f) { lifeTime -= Time.deltaTime; }
        // this admittedly convoluted line fixes an error, where two units kill each other at around the same time and one of them does not execute its death animation
        if(lifeTime != -1.0f && !this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death")) { this.GetComponent<Animator>().SetTrigger("Death"); }
    }

    // if you want to kill your boy...
    public void die() {
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<Animator>().SetTrigger("Death");
        lifeTime = deathTime;
    }

    public void hurt(int damage)
    {
        if(health - damage <= 0)
        {
            die();
            return;
        }
        this.GetComponent<Animator>().SetTrigger("Hurt");
        this.health -= damage;
    }
}
