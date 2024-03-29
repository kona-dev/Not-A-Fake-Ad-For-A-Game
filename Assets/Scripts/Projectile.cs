using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public float lifeTime;
    [SerializeField] public GameObject deathParticle;
    [SerializeField] public float speed;
    void Start()
    {
        speed = Random.Range(10f, 12.5f);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        transform.Translate(new Vector3(0, Time.deltaTime * speed, 0));

        if(lifeTime <= 0) 
        {
            Destroy(this.gameObject);
            Instantiate(deathParticle, this.transform.position, deathParticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy>().isDead) return;
            other.GetComponent<Enemy>().RemoveHealth((int)damage);
            Destroy(this.gameObject);
            Instantiate(deathParticle, this.transform.position, deathParticle.transform.rotation);
            
        }
    }
}
