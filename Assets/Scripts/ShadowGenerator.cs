using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGenerator : MonoBehaviour {

    public GameObject shadowPrefab1;
    public GameObject shadowPrefab2;
    public GameObject shadowPrefab3;
    public GameObject shadowPrefab4;

    public float respawnTime = 1.0f;

    System.Random rand = new System.Random();

    void Start() {
        StartCoroutine(spawn());
    }

    private void spawnShadow() {
        int prefabChoice = rand.Next(1,4);
        GameObject choosenPrefab;
        switch (prefabChoice)
        {
            case 1:
            choosenPrefab = shadowPrefab1;
            break;
            case 2:
            choosenPrefab = shadowPrefab2;
            break;
            case 3:
            choosenPrefab = shadowPrefab3;
            break;
            default:
            choosenPrefab = shadowPrefab4;
            break;
        }
        GameObject newInstance = Instantiate(choosenPrefab) as GameObject;
        //Debug.Log("spawned!");
        int x = rand.Next(-8, 8);
        // knight figure has to be placed lower
        float y = prefabChoice == 1 ? -4.2f : -3.5f;
        newInstance.transform.position = new Vector2
        (x, y);
        newInstance.GetComponent<SpriteRenderer>().sortingOrder = rand.Next(4,12);
    }

    private IEnumerator spawn() {
        while(true) {
            yield return new WaitForSeconds(respawnTime);
            spawnShadow();                  
        }
    }
}
