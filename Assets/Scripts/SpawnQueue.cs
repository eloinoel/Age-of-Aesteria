using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnQueue : MonoBehaviour {

    public GameObject spawn_engine;

    public Text queueText;
    public int capacity = 5;

    private Queue buildOrders;
    private float timeSinceSpawn = 0.0f; // time since last spawn

    public Vector2 queueBaseLocation = new Vector2(-10, 6);

    // first elements are just for offset
    private string[] NAMES = { "", "Unit 1", "Unit 2", "Knight" };

    private int[] COST = { 0, 10, 15, 20 };

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
        displayQueue();
    }

    public void try_queueOrder(int order) {
        if(buildOrders.Count >= capacity) return;
        if(Money.money < COST[order]) return;
        Money.money -= COST[order];
        buildOrders.Enqueue(order);
    }

    private void displayQueue() {
        // TODO - display images
        string newQueueDisplay = "";
        float timeToSpawn = timeSinceSpawn * -1;
        foreach (var element in buildOrders) {
            newQueueDisplay += NAMES[(int)element];
            timeToSpawn += (float)(int)element;
            newQueueDisplay += " ";
            newQueueDisplay += timeToSpawn.ToString("0");
            newQueueDisplay += " | ";
        }
        queueText.text = newQueueDisplay;
    }
}
