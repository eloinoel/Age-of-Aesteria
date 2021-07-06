using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQueue : MonoBehaviour {

    public GameObject spawn_engine;
    public int capacity = 5;

    private Queue buildOrders;
    private float timeSinceSpawn = 0.0f; // time since last spawn


    void Start() {
        buildOrders = new Queue();
    }

    void Update() {
        timeSinceSpawn += Time.deltaTime;
        // TODO - for now cost is just 1,2,3 seconds
        // TODO - sometimes Peek() throws a Nullpointer apparently (The if should terminate earlier then though)
        if(buildOrders.Count > 0 && timeSinceSpawn >= (int) buildOrders.Peek()) {
            int next = (int) buildOrders.Dequeue();
            if(next == 1) {
                spawn_engine.GetComponent<SpawnEngine>().spawnLeftBandit();
            } else if(next == 2) {
                spawn_engine.GetComponent<SpawnEngine>().spawnLeftValkyrie();
            } else if(next == 3) {
                spawn_engine.GetComponent<SpawnEngine>().spawnLeftHero();
            }
            timeSinceSpawn = 0.0f;
        }
        // TODO - draw current queue
    }

    public void try_queueOrder(int order) {
        if(buildOrders.Count >= capacity) { return; }
        buildOrders.Enqueue(order);
    }
}
