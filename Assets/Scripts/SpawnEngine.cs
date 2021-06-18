using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEngine : MonoBehaviour {
    public GameObject unitPrefab1;
    public GameObject unitPrefab2;
    public GameObject unitPrefab3;
    public float respawnTime = 5.0f;
    public Vector2 leftLocation = new Vector2(-8, -2);
    public Vector2 rightLocation = new Vector2(8, -2);

    private int lastLeftSpawn = 0;
    private int lastRightSpawn = 0;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void spawnLeft() {
        if(Time.frameCount - lastLeftSpawn < respawnTime*(1.0f/Time.deltaTime)) { return; }
        lastLeftSpawn = Time.frameCount;
        GameObject new_instance = Instantiate(unitPrefab1) as GameObject;
        new_instance.transform.position = new Vector2(leftLocation.x, leftLocation.y);
    }

    public void spawnLeftHero() {
        // TODO create build-queue instead of this test
        if(Time.frameCount - lastLeftSpawn < respawnTime * (1.0f / Time.deltaTime)) { return; }
        lastLeftSpawn = Time.frameCount;
        GameObject new_instance = Instantiate(unitPrefab3) as GameObject;
        new_instance.transform.position = new Vector2(leftLocation.x, leftLocation.y);
    }

    public void spawnRight() {
        GameObject new_instance = Instantiate(unitPrefab3) as GameObject;
        new_instance.transform.position = new Vector2(rightLocation.x, rightLocation.y);
        new_instance.GetComponent<SpriteRenderer>().flipX = true;
        new_instance.GetComponent<UnitGeneral>().onLeftPlayerSide = false;
    }

    private IEnumerator spawn() {
        while(true) {
            yield return new WaitForSeconds(respawnTime);
            //spawnLeft();
            spawnRight();            
        }
    }
}
