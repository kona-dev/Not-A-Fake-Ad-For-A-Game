using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] public int value;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().AddCoins(value);
            Debug.Log("Picked Up! Value: " +  value);
            FindObjectOfType<RoadGenerator>().currentPlacedPickups.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

     
    }


}
