using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireAbilityText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Text abilityText;

    private bool mouse_over = false;
    void Update() {
        if(mouse_over) {

        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouse_over = true;
        abilityText.text = "Q - Höllenfeuer";
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouse_over = false;
        abilityText.text = "";
    }
}
