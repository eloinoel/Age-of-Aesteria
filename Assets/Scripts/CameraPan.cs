using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraPan : MonoBehaviour {
    public Slider slider;
    public GameObject target;
    public int panningStrength = 5;
    private float cameraPan = 0.0075f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(slider.value - cameraPan > slider.minValue)
            {
                slider.value -= cameraPan;
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (slider.value + cameraPan <= slider.maxValue)
            {
                slider.value += cameraPan;
            }
        }

        target.transform.position = new Vector2(panningStrength * slider.value, target.transform.position.y);
    }

    void onDrag() {
        //target.transform.position = new Vector2(panningStrength * slider.value, target.transform.position.y);
    }

    void OnDeselect() {
        //this.GetComponent<Slider>().value = 0.5;
    }
}
