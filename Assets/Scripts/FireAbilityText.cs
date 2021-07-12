using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireAbilityText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Text abilityText;

    public GameObject meteorCost;
    public GameObject lightCost;
    public GameObject fireCost;
    public GameObject meteorGem;
    public GameObject lightGem;
    public GameObject fireGem;

    private bool mouse_over = false;
    void Update() {
        if(mouse_over) {

        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouse_over = true;
        abilityText.text = "Q - Höllenfeuer";
        setInactive();
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouse_over = false;
        abilityText.text = "";
        setActive();
    }

    public void setActive() {
        meteorCost.gameObject.SetActive(true);
        lightCost.gameObject.SetActive(true);
        fireCost.gameObject.SetActive(true);
        meteorGem.gameObject.SetActive(true);
        lightGem.gameObject.SetActive(true);
        fireGem.gameObject.SetActive(true);
    }

    public void setInactive() {
        meteorCost.gameObject.SetActive(false);
        lightCost.gameObject.SetActive(false);
        fireCost.gameObject.SetActive(false);
        meteorGem.gameObject.SetActive(false);
        lightGem.gameObject.SetActive(false);
        fireGem.gameObject.SetActive(false);
    }
}
