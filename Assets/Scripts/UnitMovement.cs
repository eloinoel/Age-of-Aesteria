using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {
    public float speed = 1.0f;
    private Rigidbody2D rigid_body;
    private Vector2 screenBounds;

    void Start() {
        rigid_body = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update() {
        // handle offscreen unit TODO - other cases
        /*if(transform.position.x < screenBounds.x) {
            Destroy(this.gameObject);
        }*/
        // set according movement to team of the unit
        if(this.GetComponent<UnitGeneral>().onLeftPlayerSide) {
            rigid_body.velocity = new Vector2(speed, 0);
        } else {
            rigid_body.velocity = new Vector2(-speed, 0);
        }
       
    }
}
