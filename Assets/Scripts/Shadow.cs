using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {
    System.Random rand = new System.Random();
    public float time = 0.5f;

    void Start() {
        StartCoroutine(tryDespawn());
    }

    private void despawn() {
        int r = rand.Next(1, 10);
        if(r > 1) return;
        Destroy(this.gameObject);
    }

    private IEnumerator tryDespawn() {
        while(true) {
            yield return new WaitForSeconds(time);
            despawn();
        }
    }
}
