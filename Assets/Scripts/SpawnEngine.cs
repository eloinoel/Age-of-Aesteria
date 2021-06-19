using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEngine : MonoBehaviour {
    public GameObject unitPrefab1;
    public GameObject unitPrefab2;
    public GameObject unitPrefab3;
    public float respawnTime = 5.0f;
    public bool spawnforBoth = false;
    public Vector2 leftLocation = new Vector2(-8, -2);
    public Vector2 rightLocation = new Vector2(8, -2);

    private int lastLeftSpawn = 0;
    //private int lastRightSpawn = 0; // TODO - just commented, so the compiler is happy for now

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void spawnLeft() {
        lastLeftSpawn = Time.frameCount;
        GameObject new_instance = Instantiate(unitPrefab1) as GameObject;
        new_instance.transform.position = new Vector2(leftLocation.x, leftLocation.y);
    }

    public void spawnLeftHero() {
        lastLeftSpawn = Time.frameCount;
        GameObject new_instance = Instantiate(unitPrefab3) as GameObject;
        new_instance.transform.position = new Vector2(leftLocation.x, leftLocation.y);
    }

    public void spawnRight() {
        GameObject new_instance = Instantiate(unitPrefab3) as GameObject;
        new_instance.transform.position = new Vector2(rightLocation.x, rightLocation.y);
        new_instance.GetComponent<SpriteRenderer>().flipX = true;
        new_instance.GetComponent<UnitGeneral>().onLeftPlayerSide = false;
        // this is slightly changing the y dimension of the BoxCollider of the unit to create the illusion, that different units walk at different depths
        new_instance.GetComponent<BoxCollider2D>().size = new Vector2(new_instance.GetComponent<BoxCollider2D>().size.x, new_instance.GetComponent<BoxCollider2D>().size.y+(Random.value/4.0f));
    }

    private IEnumerator spawn() {
        while(true) {
            yield return new WaitForSeconds(respawnTime);
            if(spawnforBoth) { spawnLeftHero(); }
            //spawnLeft();
            spawnRight();            
        }
    }
}
