using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public float speed;

    [SerializeField] public bool isDead;

    private void Start()
    {
        speed = FindAnyObjectByType<RoadGenerator>().speed / 1.5f;
        isDead = false;
    }


    void Update()
    {
        if (health <= 0)
        {
            GetComponent<Animator>().SetTrigger("Die");
            isDead = true;
        }
        if (FindObjectOfType<RoadGenerator>().isActive) transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("EnemyDetector"))
        {
            if (!isDead) FindObjectOfType<GameManager>().RemoveHealth(1);
            Destroy(this.gameObject);
        }
        

    }

    public void RemoveHealth(int damage) { health -= damage; Debug.Log("Removed Health: " + damage); }
}
