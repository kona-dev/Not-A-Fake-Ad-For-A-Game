using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] bool leftOrRight;

    [SerializeField] public GameObject middle;

    [SerializeField] public PowerUpType powerUpType;

    [SerializeField] public float positiveValue;
    [SerializeField] public float negativeValue;

    [SerializeField] public bool addOrMultiply;


    [SerializeField] public PowerUpDisplay powerUpDisplay;

    private void Start()
    {
        Setup(PowerUpType.ATTACKSPEED, 2, -2, true);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            bool isPlayerOnLeftOrRight = middle.transform.position.z > other.transform.position.z ? true : false;
            if (isPlayerOnLeftOrRight)
            {
                if (leftOrRight) other.GetComponent<Player>().ApplyPowerUP(powerUpType, positiveValue, addOrMultiply);
                else other.GetComponent<Player>().ApplyPowerUP(powerUpType, negativeValue, addOrMultiply);
            }
            else
            {
                if (leftOrRight) other.GetComponent<Player>().ApplyPowerUP(powerUpType, negativeValue, addOrMultiply);
                else other.GetComponent<Player>().ApplyPowerUP(powerUpType, positiveValue, addOrMultiply);
            }
          
            if(isPlayerOnLeftOrRight) { Debug.Log("LEFT"); } else { Debug.Log("RIGHT");  }

        }


    }

    public void Setup(PowerUpType powerUpType, float positiveValue, float negativeValue, bool addOrMultiply)
    {
        this.positiveValue = positiveValue;
        this.negativeValue = negativeValue;
        this.addOrMultiply = addOrMultiply;

        powerUpDisplay.CreateDisplay(powerUpType, leftOrRight, positiveValue, negativeValue, addOrMultiply);
    }
}
