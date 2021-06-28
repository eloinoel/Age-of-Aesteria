using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEngine : MonoBehaviour {
    public GameObject unitPrefab1;
    public GameObject unitPrefab2;
    public GameObject unitPrefab3;

    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    public float respawnTime = 5.0f;
    public bool spawnLeftb = false;
    public bool spawnRightb = false;
    public Vector2 leftLocation = new Vector2(-8, -2);
    public Vector2 rightLocation = new Vector2(8, -2);

    void Start() {
        StartCoroutine(spawn());
    }

    void Update() {
        
    }

    public void spawnLeft() {
        GameObject new_instance = Instantiate(unitPrefab1) as GameObject;
        new_instance.transform.position = new Vector2(leftLocation.x, leftLocation.y);
        // this is slightly changing the y dimension of the BoxCollider of the unit to create the illusion, that different units walk at different depths
        //new_instance.GetComponent<BoxCollider2D>().size = new Vector2(new_instance.GetComponent<BoxCollider2D>().size.x, new_instance.GetComponent<BoxCollider2D>().size.y + (Random.value / 4.0f));
    }

    public void spawnLeft2()
    {
        GameObject new_instance = Instantiate(unitPrefab2) as GameObject;
        new_instance.transform.position = new Vector2(leftLocation.x, leftLocation.y);
        // this is slightly changing the y dimension of the BoxCollider of the unit to create the illusion, that different units walk at different depths
        //new_instance.GetComponent<BoxCollider2D>().size = new Vector2(new_instance.GetComponent<BoxCollider2D>().size.x, new_instance.GetComponent<BoxCollider2D>().size.y + (Random.value / 4.0f));
    }

    public void spawnLeftHero() {
        GameObject new_instance = Instantiate(unitPrefab3) as GameObject;
        //GameObject new_instance = Instantiate(enemyPrefab1) as GameObject;
        new_instance.transform.position = new Vector2(leftLocation.x, leftLocation.y);
        // this is slightly changing the y dimension of the BoxCollider of the unit to create the illusion, that different units walk at different depths
        //new_instance.GetComponent<BoxCollider2D>().size = new Vector2(new_instance.GetComponent<BoxCollider2D>().size.x, new_instance.GetComponent<BoxCollider2D>().size.y + (Random.value / 4.0f));
    }

    public void spawnRightGoblin() {
        GameObject new_instance = Instantiate(enemyPrefab1) as GameObject;
        new_instance.transform.position = new Vector2(rightLocation.x, rightLocation.y);
        new_instance.GetComponent<SpriteRenderer>().flipX = true;
        new_instance.GetComponent<UnitGeneral>().onLeftPlayerSide = false;
        // this is slightly changing the y dimension of the BoxCollider of the unit to create the illusion, that different units walk at different depths
        //new_instance.GetComponent<BoxCollider2D>().size = new Vector2(new_instance.GetComponent<BoxCollider2D>().size.x, new_instance.GetComponent<BoxCollider2D>().size.y + (Random.value / 4.0f));
    }

    public void spawnRightSkeleton() {
        GameObject new_instance = Instantiate(enemyPrefab2) as GameObject;
        new_instance.transform.position = new Vector2(rightLocation.x, rightLocation.y);
        new_instance.GetComponent<SpriteRenderer>().flipX = true;
        new_instance.GetComponent<UnitGeneral>().onLeftPlayerSide = false;
        // this is slightly changing the y dimension of the BoxCollider of the unit to create the illusion, that different units walk at different depths
        //new_instance.GetComponent<BoxCollider2D>().size = new Vector2(new_instance.GetComponent<BoxCollider2D>().size.x, new_instance.GetComponent<BoxCollider2D>().size.y + (Random.value / 4.0f));
    }

    public void spawnRightFungus() {
        GameObject new_instance = Instantiate(enemyPrefab3) as GameObject;
        new_instance.transform.position = new Vector2(rightLocation.x, rightLocation.y);
        new_instance.GetComponent<SpriteRenderer>().flipX = true;
        new_instance.GetComponent<UnitGeneral>().onLeftPlayerSide = false;
        // this is slightly changing the y dimension of the BoxCollider of the unit to create the illusion, that different units walk at different depths
        //new_instance.GetComponent<BoxCollider2D>().size = new Vector2(new_instance.GetComponent<BoxCollider2D>().size.x, new_instance.GetComponent<BoxCollider2D>().size.y + (Random.value / 4.0f));
    }

    //spawn a random unit on the right side
    public void spawnRight() {
        float rnd = Random.value;
        if(rnd < 0.4) {
            spawnRightGoblin();
        } else if(rnd < 0.7) {
            spawnRightSkeleton();
        } else if(rnd < 1.0) {
            spawnRightFungus();
        }
        //spawnRightFungus();
    }

    private IEnumerator spawn() {
        while(true) {
            yield return new WaitForSeconds(respawnTime);
            if(spawnLeftb) { spawnLeftHero(); }
            if(spawnRightb) { spawnRight(); }                       
        }
    }
}
