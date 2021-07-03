using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
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

    private void SpawnMeteors()
    {
        for(int i = 0; i<meteorsPerSpawn; i++)
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

    

    // Start is called before the first frame update
    void Start()
    {
        // ignore collision between Ally and Magic Layer
        Physics2D.IgnoreLayerCollision(7, 8);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activateMeteorShower();
        }

        //stop when duration exceeded limit
        if(meteors_active)
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
