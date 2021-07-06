using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {
    public float speed = 1.0f;
    public float waitTime = 0.5f;

    private Animator m_animator;
    private Rigidbody2D rigid_body;
    private Vector2 screenBounds;

    private bool move = true;
    private float sinceStop = 0.0f;
    private bool halted = false;

    private float speedBuff = 1.0f;

    void Start() {
        m_animator = this.GetComponent<Animator>();
        rigid_body = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // set animation to running by default
        m_animator.SetInteger("AnimState", 1);
        m_animator.SetBool("Grounded", true);
    }

    void Update() {
        // handle offscreen unit TODO - other cases
        /*if(transform.position.x < screenBounds.x) {
            Destroy(this.gameObject);
        }*/
        if(this.GetComponent<UnitMelee>().getFighting()) {
            if(this.GetComponent<UnitGeneral>().onLeftPlayerSide) {
                rigid_body.velocity = new Vector2(speed, 0);
            } else {
                rigid_body.velocity = new Vector2(-speed, 0);
            }
        }

        if(move) {
            if (this.GetComponent<UnitGeneral>().onLeftPlayerSide) {
                rigid_body.velocity = new Vector2(speed*speedBuff, 0);
            } else {
                rigid_body.velocity = new Vector2(-speed*speedBuff, 0);
            }
        }
        if(!move && !halted && Time.time - sinceStop >= waitTime) { startMoving(); }
    }

    public void startMoving() {
        this.move = true;
        m_animator.SetInteger("AnimState", 1);
    }

    public void stopMoving() {
        this.move = false;
        m_animator.SetInteger("AnimState", 0);
        sinceStop = Time.time;
    }

    public void haltMoving() {
        this.halted = true;
        this.stopMoving();
    }

    public void unhaltMoving() {
        this.halted = false;
        this.startMoving();
    }

    public void setSpeedBuff(float speedBuff) {
        this.speedBuff = speedBuff;
    }
}
