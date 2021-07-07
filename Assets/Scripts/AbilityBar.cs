using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour {

    public Text meteorCounter;
    public Text lightCounter;
    public Text fireCounter;

    private float sinceMeteor;
    private float sinceLight;
    private float sinceFire;

    private float meteorTime;
    private float lightTime;
    private float fireTime;

    private bool meteorOnCooldown = false;
    private bool lightOnCooldown = false;
    private bool fireOnCooldown = false;

    void Start() {
        endAllCooldowns();
    }

    void Update() {
        
        // end cooldowns
        if(Time.time - sinceMeteor < meteorTime && meteorOnCooldown) {
            // decrement cooldowntime counter
            meteorCounter.text = ((int) Mathf.Ceil(meteorTime - (Time.time - sinceMeteor))).ToString();
            //setAlpha(transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>(), (120f*(1.0f - ((Time.time - sinceMeteor)/meteorTime)))/255f);
        } else if(meteorOnCooldown) {
            endCooldown(1);
        }

        if(Time.time - sinceLight < lightTime && lightOnCooldown) {
            // decrement cooldowntime counter
            lightCounter.text = ((int) Mathf.Ceil(lightTime - (Time.time - sinceLight))).ToString();
        } else if(lightOnCooldown) {
            endCooldown(2);
        }

        if(Time.time - sinceFire < fireTime && fireOnCooldown) {
            // decrement cooldowntime counter
            fireCounter.text = ((int) Mathf.Ceil(fireTime - (Time.time - sinceFire))).ToString();
        } else if(fireOnCooldown) {
            endCooldown(3);
        }
    }

    public void setCooldown(int cooldown, float cooldownTime) {
        switch(cooldown) {
            case 1:
                // Meteor Cooldown
                meteorOnCooldown = true;
                sinceMeteor = Time.time;
                meteorTime = cooldownTime;
                transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                // Light Cooldown
                lightOnCooldown = true;
                sinceLight = Time.time;
                lightTime = cooldownTime;
                transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 3:
                // Fire Cooldown
                fireOnCooldown = true;
                sinceFire = Time.time;
                fireTime = cooldownTime;
                transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                break;
        }
    }

    public void endCooldown(int cooldown) {
        switch(cooldown) {
            case 1:
                // Meteor Cooldown
                meteorOnCooldown = false;
                transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                break;
            case 2:
                // Light Cooldown
                lightOnCooldown = false;
                transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                break;
            case 3:
                // Fire Cooldown
                fireOnCooldown = false;
                transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                break;
        }
    }

    public void endAllCooldowns() {
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    private void setAlpha(Image greyout, float alpha) {
        var tempColor = greyout.color;
        tempColor.a = alpha;
        greyout.color = tempColor;
    }
}
