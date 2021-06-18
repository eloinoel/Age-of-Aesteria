using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneral : MonoBehaviour {
    public bool onLeftPlayerSide = true;
    public int health = 100;

    void Start() {
        
    }

    void Update() {
        if(health <= 0) { Destroy(this.gameObject); }
        // TODO - handle gameEndMessage/Event for Bases i.e. maybe call onDeath Function from other script or something
    }

    // TODO - Handle damage animation here rather the elsewhere i.e. provide callable function
}
