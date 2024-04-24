using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int coins;
    [SerializeField] public int health;

    void Start()
    {
        coins = 0;
        health = 50;
    }

    void Update()
    {
        
    }

    public void AddCoins(int value) { coins += value; }
    public void AddHealth(int value) { health += value; }

}
