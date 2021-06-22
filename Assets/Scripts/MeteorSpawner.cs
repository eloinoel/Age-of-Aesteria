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

    private bool active = false;
    private float sinceActivation = 0.0f;

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
        // ignore collision between Ally and Magic Layer
        Physics2D.IgnoreLayerCollision(7, 8);
    }

    // Update is called once per frame
    void Update()
    {
        if(sinceActivation >= duration) { sinceActivation = 0.0f;  active = false; }
        if(Input.GetKeyDown("space")) {
            active = true;
        }
        if(active)
        {
            sinceActivation += Time.deltaTime;
            Spawn();
        }

        //duration -= Time.deltaTime;
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
