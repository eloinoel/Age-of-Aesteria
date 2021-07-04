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
    private bool skillshot = false;
    public GameObject ressurection_vfx;
    public GameObject buff_vfx;
    public Image skillshot_marker;
    public float atk_y = 0;
    private GameObject beam;
    private float timeBeam = 3.5f;
    private float timeField = 3f;
    private float atk_start = 0f;


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
    public void activateMeteorShower()
    {
        startTime = Time.time;
        meteors_active = true;
    }

   
    public void atk_Buff()
    {
        //spawn beam
        if(skillshot && Input.GetMouseButton(0))
        {
            skillshot = true;
            atk_start = Time.time;
            skillshot_marker.GetComponent<Image>().enabled = false;
            GameObject beam = Instantiate(ressurection_vfx) as GameObject;
        }

        //spawn field with delay
        if (Time.time - atk_start >= 1 )
        {
            
        }

        //despawn beam
        if (Time.time - atk_start >= timeBeam)
        {
            
        }


        //despawn field
        if (Time.time - atk_start >= nextFire)
        {
             
        }
    }


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

        //------------------------------------------------------------------
        //Attack Buff
        if (Input.GetKeyDown(KeyCode.E))
        {
            skillshot_marker.GetComponent<Image>().enabled = true;    
        }

        //Mouse Input
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            skillshot_marker.transform.position = new Vector3(hit.point.x, skillshot_marker.transform.position.y, skillshot_marker.transform.position.z);
        }

        //------------------------------------------------------------------
        //Meteor Shower
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activateMeteorShower();
        }
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
}
