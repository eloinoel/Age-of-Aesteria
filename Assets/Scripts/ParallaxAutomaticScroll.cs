using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxAutomaticScroll : MonoBehaviour
{
    private float moveSpeed = 10f;
    private float nextFire = 0f;
    private float offset = 0.1f;
    


    // Update is called once per frame

    private void Start()
    {
        nextFire = Time.time;
    }

    void Update()
    {
        Vector3 desiredPosition = this.transform.position + new Vector3(offset, 0, 0);
        Vector3 smoothedPosition = Vector3.Lerp(this.transform.position, desiredPosition, moveSpeed * Time.deltaTime);
        this.transform.position = smoothedPosition;
        
    }
}
