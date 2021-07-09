using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Money : MonoBehaviour
{
    public Text moneyText;
    public static int money = 100;
    public int startmoney = 100;

    void Start()
    {
        money = startmoney;
    }

    void Update()
    {
        moneyText.text = money.ToString();
    }

    public void resetMoney()
    {
        money = startmoney;
        moneyText.text = money.ToString();
    }
}
