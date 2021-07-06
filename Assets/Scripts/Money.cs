using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Money : MonoBehaviour
{
    public Text moneyText;
    public static int money = 100;
    void Start()
    {
        
    }

    void Update()
    {
        moneyText.text = money.ToString();
    }
}
