using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider slider;

    public void setMaxHealth(int max_health) {
        slider.maxValue = max_health;
        slider.value = max_health;
    }

    public void setHealth(int health) { slider.value = health;  }

    public int getMaxHealth() {
        return (int) slider.maxValue;
    }
}
