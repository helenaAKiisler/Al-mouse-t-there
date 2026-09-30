using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int _money = 0;
    [HideInInspector] public int MyPublicValue = 0;

    public void Awake()
    {
        Events.OnChangeMoney += ChangeMoney;
        Events.OnRequestMoney += RequestMoney;
    }

    public void OnDestroy()
    {
        Events.OnChangeMoney -= ChangeMoney;
        Events.OnRequestMoney -= RequestMoney;
    }

    public void ChangeMoney(int money) => this._money = money;
    public int RequestMoney() =>  _money;

    public void Start()
    {
        Events.ChangeMoney(_money);
    }
}
