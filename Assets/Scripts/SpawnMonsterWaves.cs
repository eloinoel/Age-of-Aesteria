using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsterWaves : MonoBehaviour {

    public GameObject spawn_engine;

    public float waveSpawnTime = 10.0f;
    public float maxSpawnTimeDeviation = 3.0f;
    private float sinceLastWave = 0.0f;
    private float currentSpawnTimeDeviation = 0.0f;

    private bool spawningWave = false;
    private string currentWave = "";
    private int spawnsLeft = 0;

    private string[] waves = {"Goblinpack", "Skeletthorde", "Fungusfamilie", "Random", "Random", "Random" };
    private string[] units = {"Goblin", "Skelett", "Fungus"};

    void Start() {
        
    }

    void Update() {
        if(!spawningWave && Time.time - sinceLastWave >= waveSpawnTime + currentSpawnTimeDeviation) {
            commenceWave();
        }

        if(spawningWave) {
            spawnOn();
        }
    }

    private void commenceWave() {
        //Debug.Log("COMMENCE WAVE");
        spawningWave = true;
        currentWave = waves[(int) Mathf.Round(Random.Range(0.0f, waves.Length - 1))];
        spawnsLeft = determineSpawnCount(currentWave);
        sinceLastWave = Time.time;
        currentSpawnTimeDeviation = Random.Range(-maxSpawnTimeDeviation, maxSpawnTimeDeviation);
    }

    private void concludeWave() {
        spawningWave = false;
        currentWave = "";
    }

    private void spawnOn() {
        if(spawnsLeft <= 0) {
            concludeWave();
            return;
        }

        if(!canSpawn()) { return; }

        switch(currentWave) {
            case "Goblinpack":
                spawn("Goblin");
                break;
            case "Skeletthorde":
                spawn("Skelett");
                break;
            case "Fungusfamilie":
                spawn("Fungus");
                break;
            case "Random":
                spawn(units[(int) Mathf.Round(Random.Range(0.0f, units.Length - 1))]);
                break;
        }

        spawnsLeft--;
    }

    private int determineSpawnCount(string wavetype) {
        switch(wavetype) {
            case "Goblinpack":
                return (int) Mathf.Round(Random.Range(3.0f, 6.0f));
            case "Skeletthorde":
                return (int) Mathf.Round(Random.Range(2.0f, 5.0f));
            case "Fungusfamilie":
                return (int) Mathf.Round(Random.Range(1.0f, 4.0f));
            case "Random":
                return (int) Mathf.Round(Random.Range(1.0f, 4.0f));
            default:
                Debug.Log("This Wave does not exist");
                return 0;
        }
    }

    private bool canSpawn() {
        return spawn_engine.GetComponent<SpawnEngine>().isRightClear();
    }

    private void spawn(string creature) {
        switch(creature) {
            case "Goblin":
                spawn_engine.GetComponent<SpawnEngine>().spawnRightGoblin();
                break;
            case "Skelett":
                spawn_engine.GetComponent<SpawnEngine>().spawnRightSkeleton();
                break;
            case "Fungus":
                spawn_engine.GetComponent<SpawnEngine>().spawnRightFungus();
                break;
        }
    }
}
