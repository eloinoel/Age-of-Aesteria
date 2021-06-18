using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMelee : MonoBehaviour {
    public int attackDamage = 20;
    public int attackSpeed = 30;

    void Update() {

    }

    private void OnCollisionStay2D(Collision2D dataFromCollision) {
        this.GetComponent<SpriteRenderer>().color = Color.white; // TODO - maybe generalize to more than Sprites
        if (Time.frameCount % attackSpeed != 0) { return; } // TODO - adjust Conditions for attacking bases and tweak as well
        if(dataFromCollision.gameObject.GetComponent<UnitGeneral>() == null) { return; }
        if(dataFromCollision.gameObject.GetComponent<UnitGeneral>().onLeftPlayerSide == this.GetComponent<UnitGeneral>().onLeftPlayerSide) { return; }
        Color c = dataFromCollision.gameObject.GetComponent<SpriteRenderer>().color;
        if (c != Color.red) {
            dataFromCollision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        dataFromCollision.gameObject.GetComponent<UnitGeneral>().health -= attackDamage;
    }

    /*private void OnCollisionEnter2D(Collision2D dataFromCollision) {
        if(dataFromCollision.gameObject.GetComponent<UnitMelee>() == null) { return; }
        if (dataFromCollision.gameObject.GetComponent<UnitMovement>().onLeftPlayerSide == this.GetComponent<UnitMovement>().onLeftPlayerSide) { return; }
        Color c = this.GetComponent<SpriteRenderer>().color;
        if(c != Color.red) {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        } else {
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }*/

    /*private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Melee Detected");
        Color c = this.GetComponent<SpriteRenderer>().color;
        if (c != Color.green) {
            this.GetComponent<SpriteRenderer>().color = Color.green;
        } else {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }*/
}
