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
                Debug.Log("Applying Powerup");
            }
            else
            {
                if (leftOrRight) other.GetComponent<Player>().ApplyPowerUP(powerUpType, negativeValue, addOrMultiply);
                else other.GetComponent<Player>().ApplyPowerUP(powerUpType, positiveValue, addOrMultiply);
                Debug.Log("Applying Powerup");
            }
          
            if(isPlayerOnLeftOrRight) { Debug.Log("LEFT"); } else { Debug.Log("RIGHT");  }

        }


    }

    public void Setup(PowerUpType powerUpType, float positiveValue, float negativeValue, bool addOrMultiply)
    {
        this.positiveValue = positiveValue;
        Debug.Log(positiveValue);
        this.negativeValue = negativeValue;
        Debug.Log(negativeValue);
        this.addOrMultiply = addOrMultiply;
        this.powerUpType = powerUpType;

        if (powerUpType == PowerUpType.PAWNINCREASE)
        {
            this.positiveValue = 1;
            this.negativeValue = -1;
        }

        powerUpDisplay.CreateDisplay(powerUpType, leftOrRight, this.positiveValue, this.negativeValue, addOrMultiply);
    }

    public PowerUpType GetRandomPowerUpType()
    {
        switch (Random.Range(1, 5))
        {
            case 1: return PowerUpType.ATTACKSPEED;
            case 2: return PowerUpType.PAWNINCREASE;
            case 3: return PowerUpType.DAMAGE;
            case 4: return PowerUpType.HEALTH;
            default: return PowerUpType.ATTACKSPEED;
        }
    }
}
