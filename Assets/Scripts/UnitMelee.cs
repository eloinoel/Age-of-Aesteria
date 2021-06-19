using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMelee : MonoBehaviour {
    public int attackDamage = 20;
    public float attackStun = 0.25f;
    public float attackSpeed = 3.0f;
    public float dodgeChance = 0.2f; //In percent from 0-1
    public float comboChance = 0.5f; //In percent from 0-1

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
            delay = 0.25f;
        } else {
            attackCounter = 0;
            delay = 0.5f;
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
                delay += stun;
                m_animator.SetTrigger("Hurt");
            }
        } else {
            m_animator.SetTrigger("Block");
            delay = 0.1f;
        }
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
