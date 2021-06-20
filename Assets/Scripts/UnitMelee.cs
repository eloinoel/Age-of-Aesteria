using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMelee : MonoBehaviour {
    public int attackDamage = 20;
    public float attackStun = 0.5f;
    public float dodgeChance = 0.2f; //In percent from 0-1
    public float comboChance = 0.5f; //In percent from 0-1

    public bool dodgeRetaliate = false;
    public int dodgeRetaliationDamage = 10;
    public float dodgeRetaliationStun = 1.0f;

    public float attack1Lag = 3.0f;
    public float attack2Lag = 3.0f;
    public float attack3Lag = 3.0f;
    public float hurtLag = 1.0f;
    public float dodgeLag = 1.0f;
    public float combo1Lag = 1.0f;
    public float combo2Lag = 1.0f;
    public float combo3Lag = 1.0f;

    // TODO - implement attack-specific lagtimes instead of attackspeed

    private Animator m_animator;
    private bool fighting = false;
    private int attackCounter = 0;
    private float timeSinceAttack = 0.0f;
    private float delay = 0.0f;

    void Start() {
        m_animator = this.GetComponent<Animator>();
    }

    void Update() {
        // Increase timer that controls attack combo
        timeSinceAttack += Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D dataFromCollision) {
        // check if fightable and if enemy
        if(dataFromCollision.gameObject.GetComponent<UnitGeneral>() == null) { return; }
        if(dataFromCollision.gameObject.GetComponent<UnitGeneral>().onLeftPlayerSide == this.GetComponent<UnitGeneral>().onLeftPlayerSide) { return; }
        if(fighting) {
            if(dataFromCollision.gameObject.GetComponent<UnitMelee>() == null) { return; }
            if(timeSinceAttack >= delay) { Attack(dataFromCollision.gameObject); }
        } else {       
            enterFight();

        }
    }

    //this removes collision between the fort and its units, so the units can be spawned inside the fort BoxCollider2D
    void OnCollisionEnter2D(Collision2D collision) {
        if((collision.gameObject.tag == "blue_fort" && this.GetComponent<UnitGeneral>().onLeftPlayerSide) || (collision.gameObject.tag == "red_fort" && !this.GetComponent<UnitGeneral>().onLeftPlayerSide)) {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());
        }
        // This is a crazy experiment (no friendly unit Collisions):
        /*if(collision.gameObject.GetComponent<UnitGeneral>() == null) { return; }
        if(collision.gameObject.GetComponent<UnitGeneral>().onLeftPlayerSide == this.GetComponent<UnitGeneral>().onLeftPlayerSide) {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());
        }*/
    }

        public void enterFight() {
        // stop running animation, when encountering an enemy
        if(this.GetComponent<UnitGeneral>().health <= 0) { return; }
        m_animator.SetInteger("AnimState", 0);
        fighting = true;
    }

    public void Attack(GameObject enemy) {
        enemy.GetComponent<UnitMelee>().beAttacked(this.gameObject, attackDamage, attackStun);
        if(comboChance > Random.value) {
            attackCounter = ((attackCounter + 1) % 3);
            if(attackCounter == 0) {
                delay = combo1Lag;
            } else if(attackCounter == 1) {
                delay = combo2Lag;
            } else if(attackCounter == 2) {
                delay = combo3Lag;
            }
        } else {
            attackCounter = 0;
            if(attackCounter == 0) {
                delay = attack1Lag;
            } else if(attackCounter == 1) {
                delay = attack2Lag;
            } else if(attackCounter == 2) {
                delay = attack3Lag;
            }
        }
        m_animator.SetTrigger("Attack" + (attackCounter+1));
        timeSinceAttack = 0.0f;
    }

    public void beAttacked(GameObject enemy, int damage, float stun) {
        // dodge attack (set dodgeChance to 0 if character doesnt have dodge-Animation)
        if(dodgeChance < Random.value) {
            this.GetComponent<UnitGeneral>().health -= damage;
            if(this.GetComponent<UnitGeneral>().health <= 0) {
                // set up everything for death animation and destruction of gameObject
                this.GetComponent<UnitGeneral>().die();
                fighting = false;
                enemy.GetComponent<UnitMelee>().winFight();
            } else {
                delay += hurtLag + stun;
                m_animator.SetTrigger("Hurt");
            }
        } else {
            m_animator.SetTrigger("Block");
            if(dodgeRetaliate) { Retaliate(enemy); }
            delay = dodgeLag;
        }
    }

    private void Retaliate(GameObject enemy) {
        enemy.GetComponent<UnitMelee>().beAttacked(this.gameObject, dodgeRetaliationDamage, dodgeRetaliationStun);
    }
    
    public void winFight() {
        // start running animation after killing an enemy
        m_animator.SetInteger("AnimState", 1);
        fighting = false;
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
