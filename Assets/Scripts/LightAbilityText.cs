using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LightAbilityText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Text abilityText;

    private bool mouse_over = false;
    void Update() {
        if (mouse_over) {

        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouse_over = true;
        abilityText.text = "Lights Blessing";
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouse_over = false;
        abilityText.text = "";
    }
}