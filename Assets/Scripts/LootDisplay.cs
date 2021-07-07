using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootDisplay : MonoBehaviour
{
    public Text text;

    public Image image;
    public float lifetime = 0.9f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector2 iniPos;
    private Vector2 targetPos;
    private float timer;


    void Start()
    {
        iniPos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = iniPos + new Vector2(0, dist);
        transform.localScale = new Vector2(0.01f, 0.01f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime / 2f;

        if (timer > lifetime) Destroy(gameObject);
        else if (timer > fraction) {
            text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifetime - fraction));
            image.color = Color.Lerp(image.color, Color.clear, (timer - fraction) / (lifetime - fraction));
        }

        transform.position = Vector2.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector2.Lerp(new Vector2(0.01f, 0.01f), new Vector2(0.016f, 0.016f), Mathf.Sin(timer / lifetime));
    }

    public void SetLootText(int loot)
    {
        //Debug.Log("setting loot: ");
        //Debug.Log(loot);
        text.text = loot.ToString();
    }
}