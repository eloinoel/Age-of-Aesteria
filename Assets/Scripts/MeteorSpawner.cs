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
    private float nextFire = 0.0f;


    public void Spawn()
    {
        for(int i = 0; i<meteorsPerSpawn; i++)
        {
            GameObject new_meteor = Instantiate(meteor) as GameObject;
            new_meteor.transform.position = new Vector2(Random.Range(posleft, posright), height);
            new_meteor.GetComponent<Rigidbody2D>().velocity = startvelocity;
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextFire)
        {
            Spawn();
            nextFire += fireRate;
        }

        duration -= Time.deltaTime;
        /*if(duration <= 0)
        {
            Destroy(this);
        }*/
        
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }*/
    }
}
