using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Money : MonoBehaviour
{
    public Text moneyText;
    public static int money = 100;
    public int startmoney = 100;
    public int passiveIncome = 5;
    public float passiveIncomeInterval = 3.0f;
    private float sinceLastPassiveIncome = 0.0f;

    void Start()
    {
        money = startmoney;
    }

    void Update()
    {
        if(Time.time - sinceLastPassiveIncome >= passiveIncomeInterval) {
            money += passiveIncome;
            sinceLastPassiveIncome = Time.time;
        }
        moneyText.text = money.ToString();
    }

    public void resetMoney()
    {
        money = startmoney;
        moneyText.text = money.ToString();
    }

    public int getMoney() { return money; }

    public void subMoney(int amount) {
        money = Mathf.Max(money - amount, 0);
        moneyText.text = money.ToString();
    }
}
