using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsterWaves : MonoBehaviour {

    public GameObject spawn_engine;

    public float startSpawning = 5.0f;
    public float waveSpawnTime = 10.0f;
    public float maxSpawnTimeDeviation = 3.0f;
    private float sinceLastWave = 0.0f;
    private float currentSpawnTimeDeviation = 0.0f;

    private bool spawningWave = false;
    private string currentWave = "";
    private int spawnCount = 0;
    private int spawnsLeft = 0;
    private string currentSpacing = "";
    private float sinceSpawn = 0.0f;

    public float spacingQuantization = 0.1f;
    private float ConstantSpacing;
    private int BunchSize;

    private string[] waves = {"Goblinpack", "Skeletthorde", "Fungusfamilie", "SkelettFungi", "Random"};
    private string[] units = {"Goblin", "Skelett", "Fungus"};

    private string[] spacings = {"None", "Constant", "LinearIncrease", "LinearDecrease", "LinearPalindrom", "Bunched", "Random"};

    void Start() {
        sinceLastWave = startSpawning - waveSpawnTime;
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
        currentSpacing = determineSpacing();
        spawnCount = determineSpawnCount(currentWave);
        spawnsLeft = spawnCount;
        sinceLastWave = Time.time;
        currentSpawnTimeDeviation = Random.Range(-maxSpawnTimeDeviation, maxSpawnTimeDeviation);
    }

    private void concludeWave() {
        spawningWave = false;
        currentWave = "";
        currentSpacing = "";
        spawnCount = 0;
    }

    private void spawnOn() {
        if(spawnsLeft <= 0) {
            concludeWave();
            return;
        }

        if(!canSpawn() || Time.time-sinceSpawn < spacingTime(currentSpacing, spawnsLeft)) { return; }

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
            case "SkelettFungi":
                if(spawnsLeft >= 3)
                {
                    spawn("Fungus");
                } else if(spawnsLeft >= 2)
                {
                    spawn("Skelett");
                } else
                {
                    spawn("Fungus");
                }
                break;
            case "Random":
                spawn(units[(int) Mathf.Round(Random.Range(0.0f, units.Length - 1))]);
                break;
        }

        sinceSpawn = Time.time;
        spawnsLeft--;
    }

    private int determineSpawnCount(string wavetype) {
        switch(wavetype) {
            case "Goblinpack":
                return (int) Mathf.Round(Random.Range(3, 4));
            case "Skeletthorde":
                return (int) Mathf.Round(Random.Range(2, 3));
            case "Fungusfamilie":
                return (int) Mathf.Round(Random.Range(2, 2));
            case "Random":
                return (int) Mathf.Round(Random.Range(1.0f, 3));
            case "SkelettFungi":
                return (int)Mathf.Round(Random.Range(2, 3));
            default:
                Debug.Log("This Wave does not exist");
                return 0;
        }
    }

    private string determineSpacing() {
        string spacing = spacings[(int) Mathf.Round(Random.Range(0.0f, spacings.Length - 1))];
        if(spacing == "Constant" || spacing == "Bunched") { ConstantSpacing = (float) Mathf.Round(Random.Range(0.0f, 5.0f)); }
        if(spacing == "Bunched") { BunchSize = (int) Mathf.Round(Random.Range(2.0f, 4.0f)); }
        return spacing;
    }

    private float spacingTime(string spacing, int unitsLeft) {
        switch(spacing) {
            case "None":
                return 0.0f;
            case "Constant":
                return ConstantSpacing*spacingQuantization;
            case "LinearIncrease":
                return (float) (spawnCount - spawnsLeft)*spacingQuantization;
            case "LinearDecrease":
                return (float) spawnsLeft*spacingQuantization;
            case "LinearPalindrom":
                if(spawnsLeft >= spawnCount/2.0f) {
                    return (float) ((int)(spawnsLeft - (int)spawnCount / 2.0f))*spacingQuantization;
                } else {
                    return (float)((int)((int) spawnCount / 2.0f - spawnsLeft)) * spacingQuantization;
                }
            case "Bunched":
                if(spawnsLeft % BunchSize == 0) {
                    return ConstantSpacing * spacingQuantization;
                } else {
                    return 0.0f;
                }
            case "Random":
                return (float) Mathf.Round(Random.Range(1.0f, 5.0f))*spacingQuantization;
            default:
                Debug.Log("This Spacing does not exist");
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
