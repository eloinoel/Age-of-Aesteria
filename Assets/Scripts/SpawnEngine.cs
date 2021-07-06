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

    public void spawnLeftBandit() {
        GameObject new_instance = Instantiate(unitPrefab1) as GameObject;
        new_instance.transform.position = new Vector2(leftLocation.x, leftLocation.y);
        // this is slightly changing the y dimension of the BoxCollider of the unit to create the illusion, that different units walk at different depths
        //new_instance.GetComponent<BoxCollider2D>().size = new Vector2(new_instance.GetComponent<BoxCollider2D>().size.x, new_instance.GetComponent<BoxCollider2D>().size.y + (Random.value / 4.0f));
    }

    public void spawnLeftValkyrie() {
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
    }

    //spawn a random unit on the right side
    public void spawnLeft() {
        float rnd = Random.value;
        if (rnd < 0.4) {
            spawnLeftBandit();
        } else if (rnd < 0.7) {
            spawnLeftValkyrie();
        } else if (rnd < 1.0) {
            spawnLeftHero();
        }
    }

    private IEnumerator spawn() {
        while(true) {
            yield return new WaitForSeconds(respawnTime);
            if(spawnLeftb) {
                // this tests whether there is something blocking the spawn location
                bool leftClear = true;
                Collider2D[] left_collisions = new Collider2D[10];
                Physics2D.OverlapBox(leftLocation, new Vector2(1.5f, 1.5f), 0.0f, new ContactFilter2D().NoFilter(), left_collisions);
                foreach(Collider2D collision in left_collisions) {
                    if(collision == null || collision.gameObject == null) { continue; }
                    if(collision.gameObject.tag != "ground" && collision.gameObject.tag != "blue_fort") { leftClear = false; break; }
                }
                if(leftClear) { spawnLeft(); }
            }
            if(spawnRightb) {
                // this tests whether there is something blocking the spawn location
                bool rightClear = true;
                Collider2D[] right_collisions = new Collider2D[10];
                Physics2D.OverlapBox(rightLocation, new Vector2(1.5f, 1.5f), 0.0f, new ContactFilter2D().NoFilter(), right_collisions);
                foreach(Collider2D collision in right_collisions) {
                    if(collision == null || collision.gameObject == null) { continue; }
                    if(collision.gameObject.tag != "ground" && collision.gameObject.tag != "red_fort") { rightClear = false; break; }
                }
                if(rightClear) { spawnRight(); }
            }
        }
    }
}
