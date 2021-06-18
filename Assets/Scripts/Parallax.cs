using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    private float length, startpos;
    public GameObject scene_camera;
    public float parallaxStrength;

    void Start() {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate() {
        float temp = (scene_camera.transform.position.x * (1 - parallaxStrength));
        float distance = (scene_camera.transform.position.x * parallaxStrength);
        transform.position = new Vector2(startpos + distance, transform.position.y); // Maybe do 3d ??

        if(temp > startpos + length) startpos += length;
        else if(temp < startpos - length) startpos -= length;
    }
}
