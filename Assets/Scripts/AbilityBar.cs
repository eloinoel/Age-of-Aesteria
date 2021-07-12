using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour {

    public Text meteorCounter;
    public Text lightCounter;
    public Text fireCounter;

    public Color grey;
    public Color warnColor;

    private float sinceMeteor;
    private float sinceLight;
    private float sinceFire;

    private float meteorTime;
    private float lightTime;
    private float fireTime;

    private bool meteorOnCooldown = false;
    private bool lightOnCooldown = false;
    private bool fireOnCooldown = false;


    private bool meteorWarning = false;
    private bool lightWarning = false;
    private bool fireWarning = false;
    private float sinceMeteorWarning;
    private float sinceLightWarning;
    private float sinceFireWarning;
    private float warningTime = 0.025f;

    void Start() {
        endAllCooldowns();
    }

    void Update() {
        handleCooldowns();
        handleWarnings();
    }

    private void handleCooldowns() {
        // end cooldowns
        if (Time.time - sinceMeteor < meteorTime && meteorOnCooldown) {
            // decrement cooldowntime counter
            meteorCounter.text = ((int)Mathf.Ceil(meteorTime - (Time.time - sinceMeteor))).ToString();
            //setAlpha(transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>(), (120f*(1.0f - ((Time.time - sinceMeteor)/meteorTime)))/255f);
        } else if (meteorOnCooldown) {
            endCooldown(1);
        }

        if (Time.time - sinceLight < lightTime && lightOnCooldown) {
            // decrement cooldowntime counter
            lightCounter.text = ((int)Mathf.Ceil(lightTime - (Time.time - sinceLight))).ToString();
        } else if (lightOnCooldown) {
            endCooldown(2);
        }

        if (Time.time - sinceFire < fireTime && fireOnCooldown) {
            // decrement cooldowntime counter
            fireCounter.text = ((int)Mathf.Ceil(fireTime - (Time.time - sinceFire))).ToString();
        } else if (fireOnCooldown) {
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

    private void handleWarnings() {
        // end warnings
        if(Time.time - sinceMeteorWarning >= warningTime && meteorWarning) {
            endWarning(1);
        }

        if(Time.time - sinceLightWarning >= warningTime && lightWarning) {
            endWarning(2);
        }

        if(Time.time - sinceFireWarning >= warningTime && fireWarning) {
            endWarning(3);
        }
    }

    public void setWarning(int ability) {
        switch(ability) {
            case 1:
                // Meteor Warning
                meteorWarning = true;
                sinceMeteorWarning = Time.time;
                transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = warnColor;
                break;
            case 2:
                // Light Warning
                lightWarning = true;
                sinceLightWarning = Time.time;
                transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = warnColor;
                break;
            case 3:
                // Fire Warning
                fireWarning = true;
                sinceFireWarning = Time.time;
                transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = warnColor;
                break;
        }
    }

    public void endWarning(int ability) {
        switch(ability) {
            case 1:
                // Meteor Warning
                meteorWarning = false;
                transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(meteorWarning);
                transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = grey;
                break;
            case 2:
                // Light Cooldown
                lightWarning = false;
                transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(lightWarning);
                transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = grey;
                break;
            case 3:
                // Fire Cooldown
                fireWarning = false;
                transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(fireWarning);
                transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = grey;
                break;
        }
    }
}
