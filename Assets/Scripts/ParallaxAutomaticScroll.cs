using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxAutomaticScroll : MonoBehaviour
{
    private float moveSpeed = 0.02f;


    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * new Vector3(0.1f, 0, 0);
    }
}
