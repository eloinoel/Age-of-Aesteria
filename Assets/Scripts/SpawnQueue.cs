using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnQueue : MonoBehaviour {

    public GameObject spawn_engine;

    public float BanditSpawnDelay = 3.0f;
    public float ValkyrieSpawnDelay = 5.0f;
    public float HeroKnightSpawnDelay = 7.0f;

    public Text queueText;

    public LootDisplay lootDisplay;

    public int capacity = 5;

    private Queue buildOrders;
    private float timeSinceSpawn = 0.0f; // time since last spawn

    private Animator cooldownAnimator;

    private Sprite Bandit;
    private Sprite Valkyrie;
    private Sprite HeroKnight;

    public Vector2 queueBaseLocation = new Vector2(-10, 6);

    public Vector2 castleLocation = new Vector2(-13, -3);

    // first elements are just for offset
    private string[] Names = { "", "Bandit", "Walküre", "Ritter" };

    private int[] COST = { 0, 10, 20, 40 };

    void Start() {
        resetQueue();

        cooldownAnimator = transform.GetChild(5).gameObject.GetComponent<Animator>();

        Bandit = Resources.Load<Sprite>("Icons/BanditIcon");
        Valkyrie = Resources.Load<Sprite>("Icons/ValkyrieIcon");
        HeroKnight = Resources.Load<Sprite>("Icons/HeroKnightIcon");
    }

    void Update() {
        timeSinceSpawn += Time.deltaTime;
        // TODO - for now cost is just 1,2,3 seconds
        // TODO - sometimes Peek() throws a Nullpointer apparently (The if should terminate earlier then though)
        if(buildOrders.Count > 0 && timeSinceSpawn >= getSpawnDelay((int) buildOrders.Peek()) && spawn_engine.GetComponent<SpawnEngine>().isLeftClear()) {
            int next = (int) buildOrders.Dequeue();
            if(next == 1) {
                spawn_engine.GetComponent<SpawnEngine>().spawnLeftBandit();
            } else if(next == 2) {
                spawn_engine.GetComponent<SpawnEngine>().spawnLeftValkyrie();
            } else if(next == 3) {
                spawn_engine.GetComponent<SpawnEngine>().spawnLeftHero();
            }
            slide();
            if(buildOrders.Count > 0) { startCooldown(); }
            transform.GetChild(buildOrders.Count).gameObject.SetActive(false);
            timeSinceSpawn = 0.0f;
        }
        displayQueue();
    }

    public void try_queueOrder(int order) {
        if(buildOrders.Count >= capacity) return;
        if(Money.money < COST[order]) return;

        transform.GetChild(buildOrders.Count).gameObject.SetActive(true);
        if(order == 1) {
            transform.GetChild(buildOrders.Count).gameObject.GetComponent<Image>().sprite = Bandit;
        } else if(order == 2) {
            transform.GetChild(buildOrders.Count).gameObject.GetComponent<Image>().sprite = Valkyrie;
        } else if(order == 3) {
            transform.GetChild(buildOrders.Count).gameObject.GetComponent<Image>().sprite = HeroKnight;
        }

        Money.money -= COST[order];
        buildOrders.Enqueue(order);
        if(buildOrders.Count == 1) { startCooldown(); timeSinceSpawn = 0.0f; }
        costAnimation(COST[order]);
    }

    private void displayQueue() {
        string trainText = "";
        if(buildOrders.Count >= 1) {
            trainText += "Trainiere "+Names[(int) buildOrders.Peek()]+" "+"...";

        }
        queueText.text = trainText;
    }

    private float getSpawnDelay(int order) {
        switch (order) {
            case 1:
                return this.BanditSpawnDelay;
            case 2:
                return this.ValkyrieSpawnDelay;
            case 3:
                return this.HeroKnightSpawnDelay;
            default:
                return 0.0f;
        }
    }

    private void slide() {
        for(int i=0; i<buildOrders.Count && i<4; i++) {
            transform.GetChild(i).gameObject.GetComponent<Image>().sprite = transform.GetChild(i+1).gameObject.GetComponent<Image>().sprite;
        }
    }

    private void startCooldown() {
        cooldownAnimator.SetTrigger("Cooldown" + this.buildOrders.Peek());
    }

    public void costAnimation(int cost) {
        LootDisplay costPopup = Instantiate(lootDisplay, castleLocation, Quaternion.identity).GetComponent<LootDisplay>();
        costPopup.SetLootText(cost * -1);
    }

    public void resetQueue() {
        buildOrders = new Queue();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
    }
}
