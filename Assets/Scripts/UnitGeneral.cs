using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneral : MonoBehaviour {
    public bool onLeftPlayerSide = true;
    public int unitType;
    public int health = 100;
    public HealthBar healthBar;
    public bool alwaysShowHealth = false;
    public float showHealthTime = 2.0f;

    public float deathTime = 1.0f; // length of afterdeath animation
    public float hurtTime = 1.0f;


    private float lifeTime = -1.0f;

    private bool isHurt = false;
    private float sinceHurt = 0.0f;

    private bool resistHurt = false;
    private bool hurtIncepted = false;
    private float hurtDelay = 0.0f;
    private float sinceHurtInception = 0.0f;
    private int pendDamage = 0;

    private float regenerationBuff = 0.0f;

    private Shader defaultShader;
    private Shader hurtShader;

    private int[] LOOT = { 0, 11, 16, 21 }; 

    void Start() {
        defaultShader = Shader.Find("Sprites/Default");
        hurtShader = Shader.Find("GUI/Text Shader");
        healthBar.setMaxHealth(health);
        deactivateHealthBar();
    }

    void Update() {

        // begin Hurtflash after delay
        if(hurtIncepted && Time.time - sinceHurtInception >= hurtDelay) {
            hurtIncepted = false;
            hurtDelay = 0.0f;
            isHurt = true;
            sinceHurt = Time.time;
            this.GetComponent<SpriteRenderer>().material.shader = hurtShader;
            this.GetComponent<SpriteRenderer>().material.color = new Color(241.0f/255.0f, 241.0f/255f, 241.0f/255.0f, 255.0f/255.0f);
            activateHealthBar();

            this.health -= pendDamage;
            this.healthBar.setHealth(this.health);
            this.pendDamage = 0;
            if (health <= 0) {
                die();
            } else if(!resistHurt) {
                this.GetComponent<Animator>().SetTrigger("Hurt");
            }
        }
        // return to normal shader after damage flash
        if(isHurt && (Time.time - sinceHurt >= hurtTime)) { isHurt = false; this.GetComponent<SpriteRenderer>().material.shader = defaultShader; this.GetComponent<SpriteRenderer>().material.color = Color.white; }

        // deactivate HealthBar
        if(showHealthTime <= Time.time - sinceHurt) { this.deactivateHealthBar(); }


        // TODO - handle gameEndMessage/Event for Bases i.e. maybe call onDeath Function from other script or something
        if(lifeTime <= 0.0f && lifeTime != -1.0f) { Destroy(this.gameObject); }
        if(lifeTime != -1.0f) { lifeTime -= Time.deltaTime; }
        // this admittedly convoluted line fixes an error, where two units kill each other at around the same time and one of them does not execute its death animation
        //if(lifeTime != -1.0f && !died && !this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death")) { died = true;  this.GetComponent<Animator>().SetTrigger("Death"); }

        this.health = Mathf.Max(this.health + ((int) (this.regenerationBuff*this.healthBar.getMaxHealth())), this.health);
        this.healthBar.setHealth(this.health);
    }

    // if you want to kill your boy...
    public void die() {
        if(!onLeftPlayerSide) Money.money += LOOT[this.unitType];
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<Animator>().SetTrigger("Death");
        lifeTime = deathTime;
        deactivateHealthBar();
        if(this.GetComponent<UnitMelee>().getEnemy() != null && this.GetComponent<UnitMelee>().getFighting() == true) { this.GetComponent<UnitMelee>().getEnemy().GetComponent<UnitMelee>().winFight(); }
    }

    public void hurt(int damage, float delay, bool resist) {

        resistHurt = resist;
        hurtIncepted = true;
        sinceHurtInception = Time.time;
        hurtDelay = delay;
        pendDamage = damage;

        //this.GetComponent<Animator>().SetTrigger("Hurt");
    }

    public void activateHealthBar() {
        if(transform.GetChild(0).gameObject != null) { transform.GetChild(0).gameObject.SetActive(true); }
    }

    public void deactivateHealthBar() {
        if(alwaysShowHealth) { return; }
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public bool getHurtIncepted() { return this.hurtIncepted; }

    public void setRegenerationBuff(float regenerationBuff) { this.regenerationBuff = regenerationBuff; }
}
