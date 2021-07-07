using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMelee : MonoBehaviour {
    public bool doFight = true;
    public int attackDamage = 20;
    public float attackStun = 0.5f;
    public float dodgeChance = 0.2f; //In percent from 0-1
    public float comboChance = 0.5f; //In percent from 0-1

    public bool dodgeIsResist = false;
    public float resistFactor = 0.5f;
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

    public float attack1HurtDelay = 0.0f;
    public float attack2HurtDelay = 0.0f;
    public float attack3HurtDelay = 0.0f;
    public float retaliateHurtDelay = 0.0f;

    private bool buffed = false;
    public float buffTime = 5.0f;
    public float sinceBuff = 0.0f;
    private float attackBuff = 1.0f;

    private GameObject enemy = null;

    private Animator m_animator;
    private bool fighting = false;
    private int attackCounter = 0;
    private float timeSinceAttack = 0.0f;
    private float delay = 0.0f;

    private bool dodgeIncepted = false;
    private float dodgeDelay = 0.0f;
    private float sinceDodgeInception = 0.0f;


    void Start() {
        m_animator = this.GetComponent<Animator>();
    }

    void Update() {
        // Increase timer that controls attack combo
        timeSinceAttack += Time.deltaTime;

        if(dodgeIncepted && (Time.time - sinceDodgeInception >= dodgeDelay)) { 
            dodgeIncepted = false; 
            dodgeDelay = 0.0f;
            sinceDodgeInception = 0.0f;
            m_animator.SetTrigger("Block");
            delay = dodgeLag;
        }

        // end buffed state
        if(buffed && Time.time - sinceBuff >= buffTime) {
            buffed = false;
            attackBuff = 1.0f;
            this.GetComponent<UnitMovement>().setSpeedBuff(1.0f);
            this.GetComponent<UnitGeneral>().setRegenerationBuff(0.0f);

        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        // check if fightable and if enemy
        if(collision.gameObject.GetComponent<UnitGeneral>() == null) { return; }
        if(collision.gameObject.GetComponent<UnitGeneral>().onLeftPlayerSide != this.GetComponent<UnitGeneral>().onLeftPlayerSide) {
            if(fighting) {
                if(collision.gameObject.GetComponent<UnitMelee>() == null) { return; }
                if(timeSinceAttack >= delay && doFight) { Attack(collision.gameObject); }
            } else {
                enterFight(collision.gameObject);
            }
        }

        if((((this.GetComponent<UnitGeneral>().onLeftPlayerSide == true && collision.gameObject.GetComponent<UnitGeneral>().onLeftPlayerSide == true) && (this.gameObject.transform.position.x < collision.gameObject.transform.position.x))
         || ((this.GetComponent<UnitGeneral>().onLeftPlayerSide == false && collision.gameObject.GetComponent<UnitGeneral>().onLeftPlayerSide == false) && (this.gameObject.transform.position.x > collision.gameObject.transform.position.x)))
          && (collision.gameObject.tag != "blue_fort") && (collision.gameObject.tag != "red_fort") && !fighting) {
            this.GetComponent<UnitMovement>().stopMoving();
        }
    }

    //this removes collision between the fort and its units, so the units can be spawned inside the fort BoxCollider2D
    void OnCollisionEnter2D(Collision2D collision) {
        // ignore fort collisions
        if((collision.gameObject.tag == "blue_fort" && this.GetComponent<UnitGeneral>().onLeftPlayerSide) || (collision.gameObject.tag == "red_fort" && !this.GetComponent<UnitGeneral>().onLeftPlayerSide)) {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());
        }

        // This is a crazy experiment (no friendly unit Collisions):
        /*if(collision.gameObject.GetComponent<UnitGeneral>() == null) { return; }
        if(collision.gameObject.GetComponent<UnitGeneral>().onLeftPlayerSide == this.GetComponent<UnitGeneral>().onLeftPlayerSide) {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());
        }*/
    }

    void OnCollisionLeave2D(Collision2D collision) {
        
        if(collision.gameObject.GetComponent<UnitGeneral>().onLeftPlayerSide == this.GetComponent<UnitGeneral>().onLeftPlayerSide) {
            this.GetComponent<UnitMovement>().startMoving();
        }
    }

    public void enterFight(GameObject opponent) {
        // stop running animation, when encountering an enemy
        if(this.GetComponent<UnitMovement>() != null) { this.GetComponent<UnitMovement>().haltMoving(); }
        if(this.GetComponent<UnitGeneral>().health <= 0) { return; }
        this.GetComponent<UnitGeneral>().activateHealthBar();
        m_animator.SetInteger("AnimState", 0);
        fighting = true;
        enemy = opponent;
    }

    public void Attack(GameObject enemy) {
        if(comboChance > Random.value) {
            if(attackCounter == 0) {
                delay = combo1Lag;
                enemy.GetComponent<UnitMelee>().beAttacked(this.gameObject, (int) (attackDamage*attackBuff), attackStun, attack1HurtDelay);
            } else if(attackCounter == 1) {
                delay = combo2Lag;
                enemy.GetComponent<UnitMelee>().beAttacked(this.gameObject, (int) (attackDamage*attackBuff), attackStun, attack2HurtDelay);
            } else if(attackCounter == 2) {
                delay = combo3Lag;
                enemy.GetComponent<UnitMelee>().beAttacked(this.gameObject, (int) (attackDamage*attackBuff), attackStun, attack3HurtDelay);
            }
            m_animator.SetTrigger("Attack" + (attackCounter+1));
            attackCounter = ((attackCounter + 1) % 3);
        } else {
            attackCounter = 0;
            if(attackCounter == 0) {
                delay = attack1Lag;
                enemy.GetComponent<UnitMelee>().beAttacked(this.gameObject, (int) (attackDamage*attackBuff), attackStun, attack1HurtDelay);
            } else if(attackCounter == 1) {
                delay = attack2Lag;
                enemy.GetComponent<UnitMelee>().beAttacked(this.gameObject, (int) (attackDamage*attackBuff), attackStun, attack2HurtDelay);
            } else if(attackCounter == 2) {
                delay = attack3Lag;
                enemy.GetComponent<UnitMelee>().beAttacked(this.gameObject, (int) (attackDamage*attackBuff), attackStun, attack3HurtDelay);
            }
            m_animator.SetTrigger("Attack" + (attackCounter+1));
        }
        timeSinceAttack = 0.0f;
    }

    public void beAttacked(GameObject enemy, int damage, float stun, float hurtDelay) {
        // dodge attack (set dodgeChance to 0 if character doesnt have dodge-Animation) (and cannot dodge when not idle)
        if(dodgeChance < Random.value || timeSinceAttack < delay) { //!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")
            this.GetComponent<UnitGeneral>().hurt(damage, hurtDelay, false);
            this.resetCombo();
            if (this.GetComponent<UnitGeneral>().health <= 0) {
                // set up everything for death animation and destruction of gameObject
                fighting = false;
                enemy.GetComponent<UnitMelee>().winFight();
            } else {
                delay += hurtLag + stun;
                //m_animator.SetTrigger("Hurt");
            }
        } else {
            if(dodgeIsResist) {
                this.GetComponent<UnitGeneral>().hurt((int)(damage * resistFactor), hurtDelay, true);
                dodgeIncepted = true;
                dodgeDelay = hurtDelay;
                sinceDodgeInception = Time.time;
            } else {
                dodgeIncepted = true;
                dodgeDelay = hurtDelay;
                sinceDodgeInception = Time.time;
            }
            delay = hurtDelay;
            if(dodgeRetaliate) { Retaliate(enemy); }
        }
    }

    private void Retaliate(GameObject enemy) {
        enemy.GetComponent<UnitMelee>().beAttacked(this.gameObject, dodgeRetaliationDamage, dodgeRetaliationStun, retaliateHurtDelay);
    }
    
    public void winFight() {
        // start running animation after killing an enemy
        if(this.GetComponent<UnitMovement>() != null) { this.GetComponent<UnitMovement>().unhaltMoving(); }
        if(this.GetComponent<UnitGeneral>().health > 0) { m_animator.SetInteger("AnimState", 1); }
        //this.GetComponent<UnitGeneral>().deactivateHealthBar();
        fighting = false;
        this.resetCombo();
    }

    public bool getFighting() { return this.fighting; }
    public GameObject getEnemy() { return this.enemy; }

    public void buff() {
        buffed = true;
        sinceBuff = Time.time;
        attackBuff = 1.5f;
        this.GetComponent<UnitMovement>().setSpeedBuff(1.5f);
        this.GetComponent<UnitGeneral>().setRegenerationBuff(0.1f);
    }

    private void resetCombo() {
        this.attackCounter = 0;
    }
}
