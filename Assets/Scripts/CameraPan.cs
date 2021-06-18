using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraPan : MonoBehaviour {
    public Slider slider;
    public GameObject target;
    public int panningStrength = 5;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        target.transform.position = new Vector2(panningStrength * slider.value, target.transform.position.y);
    }

    void onDrag() {
        //target.transform.position = new Vector2(panningStrength * slider.value, target.transform.position.y);
    }

    void OnDeselect() {
        //this.GetComponent<Slider>().value = 0.5;
    }
}
