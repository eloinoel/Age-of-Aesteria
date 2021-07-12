using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySpawner : MonoBehaviour
{
    private bool meteorUsedOnce = false;
    private bool fireUsedOnce = false;
    private bool buffUsedOnce = false;

    //METEOR VARIABLES
    public int meteorCost = 20;
    public GameObject meteor;
    public Vector2 startvelocity = new Vector2(30, -3);
    public float meteorCooldown = 15f;

    public float posleft = -25;
    public float posright = 25;
    public float height = 10;

    public float duration = 5;
    public int meteorsPerSpawn = 3;
    public float fireRate = 0.5f;
    private bool meteors_active = false;
    private float startTime = 0.0f;
    private float nextFire = 0.0f;
    private float timeSinceMeteor = 0f;

    //ATTACK BUFF VARIABLES
    public int buffCost = 15;
    public GameObject ressurection_vfx;
    public GameObject buff_vfx;
    public Image skillshot_marker;
    public float atk_y = 0;
    public float buffRadius = 5f;
    public float buff_duration = 10.0f;
    public float buffCooldown = 10f;

    private bool skillshot = false;
    private bool skillactive = false;
    private float buffvfxDuration = 3.5f;
    private bool buffvfx = false;
    private float atk_start = 0f;
    private Vector3 buffPosition;
    private float timeSinceBuff;

    //HELL FIRE VARIABLES
    public int fireCost = 30;
    public GameObject orb_vfx;
    public GameObject explosion_vfx;
    public GameObject fire_vfx;
    public Image fire_skillshot_img;
    public Vector2 fireHitBox = new Vector2(5.2f, 3);
    public float fireDuration = 3;
    public int fireDmgPerTick = 35;
    public float fireTickRate = 0.5f;
    public float hellfireCooldown = 20f;

    private bool fire_skillshot = false;
    private bool fire_active = false;
    private Vector3 firePosition;
    private float fire_start = 0f;
    private bool explosion_instantiated = false;
    private bool fire_instantiated = false;
    private GameObject fire;
    private float nextFireTick = 0f;
    private float timeSinceHellfire;

    public GameObject AbilityBar;
    public GameObject Money;

    private void SpawnMeteors()
    {
        for (int i = 0; i < meteorsPerSpawn; i++)
        {
            GameObject new_meteor = Instantiate(meteor) as GameObject;
            new_meteor.transform.position = new Vector2(Random.Range(posleft, posright), height);
            new_meteor.GetComponent<Rigidbody2D>().velocity = startvelocity;
        }
    }

    /**
     * Use this method to trigger one meteor shower
     * the parameters can be tweaked in the unity gameObject
     */
    private void MeteorShower()
    {
        //stop when duration exceeded limit
        if (meteors_active)
        {
            if (Time.time - startTime >= duration)
            {
                meteors_active = false;
            }
            if (Time.time >= nextFire)
            {
                nextFire = Time.time + fireRate;
                SpawnMeteors();
            }
        }

    }

    public void activateMeteorShower()
    {
        Money money = Money.GetComponent<Money>();
        if (timeSinceMeteor >= meteorCooldown && money.getMoney() >= meteorCost)
        {
            money.subMoney(meteorCost);
            meteorUsedOnce = true;
            startTime = Time.time;
            meteors_active = true;
            // set Cooldown
            AbilityBar.GetComponent<AbilityBar>().setCooldown(1, meteorCooldown);
        } else
        {
            AbilityBar.GetComponent<AbilityBar>().setWarning(1);
        }
    }

   
    private void atk_Buff()
    {
        //skillshot images follows mouse
        if(skillshot)
        {
            //Mouse Input
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            skillshot_marker.transform.position = new Vector3(mousePosition.x, atk_y + (skillshot_marker.GetComponent<RectTransform>().rect.height * skillshot_marker.transform.localScale.y)/2, 0f);
        }

        //spawn beam and despawn skillshot image
        Money money = Money.GetComponent<Money>();
        if (skillshot && Input.GetMouseButton(0) && money.getMoney() >= buffCost)
        {
            buffUsedOnce = true;
            atk_start = Time.time;
            skillshot_marker.GetComponent<Image>().enabled = false;
            buffPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject beam = Instantiate(ressurection_vfx) as GameObject;
            beam.transform.position = new Vector3(buffPosition.x, atk_y + 0.2f, 0f);
            skillshot = false;
            skillactive = true;
            buffvfx = false;
            // set Cooldown
            money.subMoney(buffCost);
            AbilityBar.GetComponent<AbilityBar>().setCooldown(2, buffCooldown);
        } else if (skillshot && Input.GetMouseButton(0))
        {
            //disable skillshot
            skillshot_marker.GetComponent<Image>().enabled = false;
            skillshot = false;
            skillactive = false;

            //ability blink
            AbilityBar.GetComponent<AbilityBar>().setWarning(2);

        }

        //while ability is active
        if (skillactive)
        {
            //spawn field with delay
            if (!buffvfx && Time.time - atk_start >= 0.7)
            {
                GameObject field = Instantiate(buff_vfx) as GameObject;
                field.transform.position = new Vector3(buffPosition.x, atk_y + 0.2f, 0f);
                buffvfx = true;

                //get nearby ally units
                Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(buffPosition.x, atk_y + 0.2f), buffRadius);
                //OnDrawGizmos(); //DEBUG
                //Debug.Log(colliders.Length);
                foreach (Collider2D nearbyObject in colliders)
                {
                    UnitGeneral unitGeneral = nearbyObject.gameObject.GetComponent<UnitGeneral>();
                    if(unitGeneral != null && unitGeneral.onLeftPlayerSide && nearbyObject.name != "blue_fort")
                    {
                        nearbyObject.gameObject.GetComponent<UnitMelee>().buff(buff_duration);
                    }
                }
            }
            //animation over
            if (Time.time - atk_start >= buffvfxDuration)
            {
                //Destroy(field);
                skillactive = false;
                buffvfx = false; 
            }
        }  
    }


    public void activateBuff()
    {
        if (skillshot)
        {
            resetSkillshotAbilities();
            return;
        }
        Money money = Money.GetComponent<Money>();
        if (timeSinceBuff >= buffCooldown)
        {
            resetSkillshotAbilities();
            skillshot_marker.GetComponent<Image>().enabled = true;
            skillshot = true;
        } else
        {
            AbilityBar.GetComponent<AbilityBar>().setWarning(2);
        }
        
    }

    //DEBUG TO SHOW CIRCLES
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        //Gizmos.DrawWireSphere(new Vector3(buffPosition.x, atk_y + 0.2f, 0f), buffRadius);
        Gizmos.DrawWireCube(new Vector3(firePosition.x, atk_y + fireHitBox.y/2, 0f), new Vector3(fireHitBox.x, fireHitBox.y, 0)); 
    }

    public void hellFire()
    {
        //skillshot images follows mouse
        if (fire_skillshot)
        {
            //Mouse Input
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            fire_skillshot_img.transform.position = new Vector3(mousePosition.x, atk_y + (fire_skillshot_img.GetComponent<RectTransform>().rect.height * fire_skillshot_img.transform.localScale.y) / 2, 0f);
        }

        //spawn beam and despawn skillshot image
        Money money = Money.GetComponent<Money>();
        if (fire_skillshot && Input.GetMouseButton(0) && money.getMoney() >= fireCost)
        {
            fireUsedOnce = true;
            //disable skillshot img
            fire_start = Time.time;
            fire_skillshot_img.GetComponent<Image>().enabled = false;

            //instantiate first particle system
            firePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject orb = Instantiate(orb_vfx) as GameObject;
            orb.transform.position = new Vector3(firePosition.x, atk_y + fireHitBox.y / 4, 0f);
            fire_skillshot = false;
            fire_active = true;
            explosion_instantiated = false;
            fire_instantiated = false;
            // set Cooldown
            money.subMoney(fireCost);
            AbilityBar.GetComponent<AbilityBar>().setCooldown(3, hellfireCooldown);
        } else if (fire_skillshot && Input.GetMouseButton(0))
        {
            //disable skillshot
            fire_skillshot_img.GetComponent<Image>().enabled = false;
            fire_skillshot = false;
            fire_active = false;

            //ability blink
            AbilityBar.GetComponent<AbilityBar>().setWarning(3);
        }

        //while skill active, spawn particle systems, apply damage
        if (fire_active)
        {
            //instantiate explosion
            if(!explosion_instantiated && Time.time - fire_start >= 0.7)   //TODO
            {
                GameObject explosion = Instantiate(explosion_vfx) as GameObject;
                explosion.transform.position = new Vector3(firePosition.x, atk_y + 0.1f, 0f); //TODO
                explosion_instantiated = true;
            }

            //instantiate fire
            if(!fire_instantiated && Time.time - fire_start >= 1.0)   
            {
                fire = Instantiate(fire_vfx) as GameObject;
                fire.transform.position = new Vector3(firePosition.x, atk_y, 0f); //TODO
                fire_instantiated = true;
            }

            //apply damage
            float tmp = Time.time - fire_start;
            //Debug.Log("curDuration: " + tmp + ", nextFireTick: " + nextFireTick + ", Time: " + Time.time);
            if(Time.time - fire_start >= 1.0 && Time.time >= nextFireTick)
            {
                nextFireTick = Time.time + fireTickRate;

                //get nearby units
                Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(firePosition.x - (fireHitBox.x/2), atk_y), new Vector2(firePosition.x + (fireHitBox.x/2), atk_y + fireHitBox.y));
                foreach (Collider2D nearbyObject in colliders)
                {
                    UnitGeneral unitGeneral = nearbyObject.gameObject.GetComponent<UnitGeneral>();
                    if (unitGeneral != null && nearbyObject.name != "red_fort" && nearbyObject.name != "blue_fort")
                    {
                        //Debug.Log(unitGeneral.gameObject.name);
                        unitGeneral.hurt(fireDmgPerTick, 0.0f, false);
                    }
                }
            }
            
            //stop fire animation
            if(fire_instantiated && Time.time - fire_start >= fireDuration + 1)
            {
                fire_instantiated = false;
                explosion_instantiated = false;
                fire_active = false;
            }
        }
    }

    public void activateHellFire()
    {
        if(fire_skillshot)
        {
            resetSkillshotAbilities();
            return;
        }

        Money money = Money.GetComponent<Money>();
        if (timeSinceHellfire >= hellfireCooldown)
        {
            resetSkillshotAbilities();
            fire_skillshot_img.GetComponent<Image>().enabled = true;
            fire_skillshot = true;
        } else
        {
            AbilityBar.GetComponent<AbilityBar>().setWarning(3);
        }
    }

    public void updateCooldowns()
    {
        if (fireUsedOnce)
        {
            timeSinceHellfire = Time.time - fire_start;
        } else
        {
            timeSinceHellfire = hellfireCooldown + 1;
        }
        if(buffUsedOnce)
        {
            timeSinceBuff = Time.time - atk_start;
        } else
        {
            timeSinceBuff = buffCooldown + 1;
        }
        if(meteorUsedOnce)
        {
            timeSinceMeteor = Time.time - startTime;
        } else
        {
            timeSinceMeteor = meteorCooldown + 1;
        }

        
    }

    //FISSURE
    public void resetSkillshotAbilities()
    {
        fire_skillshot_img.GetComponent<Image>().enabled = false;
        fire_skillshot = false;
        skillshot_marker.GetComponent<Image>().enabled = false;
        skillshot = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        // ignore collision between Ally and Magic Layer
        Physics2D.IgnoreLayerCollision(7, 8);
        skillshot_marker.GetComponent<Image>().enabled = false;
        //skillshot_marker.transform.position.Set(0f, atk_y, 0f);
        fire_skillshot_img.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        atk_Buff();
        MeteorShower();
        hellFire();

        //update cooldowns
        updateCooldowns();

        //------------------------------------------------------------------
        //Attack Buff
        if (Input.GetKeyDown(KeyCode.E))
        {
            activateBuff();
        }

        //------------------------------------------------------------------
        //Meteor Shower
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activateMeteorShower();
        }

        //------------------------------------------------------------------
        //Hellfire
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activateHellFire();
        }


    }
}
