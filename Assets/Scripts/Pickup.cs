using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] public int value;
    [SerializeField] public GameObject pickupParticle;

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().AddCoins(value);
            Debug.Log("Picked Up! Value: " +  value);
            FindObjectOfType<RoadGenerator>().currentPlacedPickups.Remove(this.gameObject);
            Destroy(this.gameObject);
            Instantiate(pickupParticle, this.transform.position, pickupParticle.transform.rotation);

        }

     
    }


}
