using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySpawner : MonoBehaviour
{
    //METEOR VARIABLES
    public GameObject meteor;
    public Vector2 startvelocity = new Vector2(30, -3);

    public float posleft = -25;
    public float posright = 25;
    public float height = 10;

    public float duration = 5;
    public int meteorsPerSpawn = 3;
    public float fireRate = 0.5f;
    private bool meteors_active = false;
    private float startTime = 0.0f;
    private float nextFire = 0.0f;

    //ATTACK BUFF VARIABLES
    public GameObject ressurection_vfx;
    public GameObject buff_vfx;
    public Image skillshot_marker;
    public float atk_y = 0;
    public float buffRadius = 5f;

    private bool skillshot = false;
    private bool skillactive = false;
    private float buffvfxDuration = 3.5f;
    private bool buffvfx = false;
    private float atk_start = 0f;
    private Vector3 buffPosition;


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
        startTime = Time.time;
        meteors_active = true;
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
        if(skillshot && Input.GetMouseButton(0))
        {
            atk_start = Time.time;
            skillshot_marker.GetComponent<Image>().enabled = false;
            buffPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject beam = Instantiate(ressurection_vfx) as GameObject;
            beam.transform.position = new Vector3(buffPosition.x, atk_y + 0.2f, 0f);
            skillshot = false;
            skillactive = true;
            buffvfx = false;
        }

        //while ability is active
        if(skillactive)
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
                        nearbyObject.gameObject.GetComponent<UnitMelee>().buff();
                    }
                }
            }
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
        skillshot_marker.GetComponent<Image>().enabled = true;
        skillshot = true;
    }

    //DEBUG TO SHOW CIRCLES
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(new Vector3(buffPosition.x, atk_y + 0.2f, 0f), buffRadius);
    }*/


    // Start is called before the first frame update
    void Start()
    {
        // ignore collision between Ally and Magic Layer
        Physics2D.IgnoreLayerCollision(7, 8);
        skillshot_marker.GetComponent<Image>().enabled = false;
        skillshot_marker.transform.position.Set(0f, atk_y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        atk_Buff();
        MeteorShower();

        //------------------------------------------------------------------
        //Attack Buff
        if (!skillshot && Input.GetKeyDown(KeyCode.E))
        {
            activateBuff();
        }

        //------------------------------------------------------------------
        //Meteor Shower
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activateMeteorShower();
        }
        


    }
}
