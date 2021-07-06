using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
    public int damage = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        // ignore collision between Ally and Magic Layer
        Physics2D.IgnoreLayerCollision(7, 8);
    }

    //predefined method called on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //get the object that collided with this
        GameObject collided = collision.gameObject;

        if (collided.GetComponent<UnitGeneral>() != null && (!collided.GetComponent<UnitGeneral>().onLeftPlayerSide)) {
            collided.GetComponent<UnitGeneral>().hurt(damage, 0.0f, false);
            Destroy(this.gameObject);
        } else if(collided.GetComponent<UnitGeneral>() != null && (collided.GetComponent<UnitGeneral>().onLeftPlayerSide))
        {
            //Physics2D.IgnoreCollision(collided.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<CircleCollider2D>());   
        } else //ground
        {
            Destroy(this.gameObject);
        }
        //do nothing if ally
    }

    // Update is called once per frame
    void Update()
    {
        /*despawnTime -= Time.deltaTime;
        if (despawnTime <= 0)
        {
            Destroy(this);
        }*/
    }
}
