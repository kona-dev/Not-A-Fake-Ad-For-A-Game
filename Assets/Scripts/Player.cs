using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float damage;

    [SerializeField] public Vector3 basePosition = Vector3.zero;

    [SerializeField] public float radius;

    [SerializeField] public List<GameObject> pawns = new List<GameObject>();

    [SerializeField] public List<GameObject> pawnPrefabs = new List<GameObject>();

    [SerializeField] public bool testAddPawn;
    [SerializeField] public bool testRemovePawn;

    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float attackTimer;
    [SerializeField] public float currentDamageMultiplier;

 
    void Start()
    {
        attackTimer = 0;
        currentDamageMultiplier = 0;
    }


    void Update()
    {
        if(testAddPawn) { TestAddPawn(); }
        if(testRemovePawn) { TestRemovePawn(); }
            
        if(Input.GetKey(KeyCode.A) && gameObject.transform.position.z >= -8.93f) { gameObject.transform.Translate(new Vector3(0, 0, -Time.deltaTime * speed)); }
        if(Input.GetKey(KeyCode.D) && gameObject.transform.position.z <= 8.93f) { gameObject.transform.Translate(new Vector3(0, 0, Time.deltaTime * speed)); }

        if(Input.GetKey(KeyCode.Mouse1))
        {
           if(attackTimer >= 1 / attackSpeed)
            {
                Shoot();
                attackTimer = 0;
            }
        }

        if(attackTimer < 1 / attackSpeed) { attackTimer += Time.deltaTime; }

    }


    public void ArrangePawns()
    {
       if (pawns.Count == 1) { pawns[0].transform.position = Vector3.zero; GetComponent<BoxCollider>().size = new Vector3(1.5f, 2f, 1.5f); }
       else
       {
            for (int i = 0; i < pawns.Count; i++)
            {
                Vector3 pos = new Vector3();
                float offsetAngle = i * MathF.PI * 2 / pawns.Count;
                if (pawns.Count == 2) { pos = new Vector3(Mathf.Sin(offsetAngle), 0, Mathf.Cos(offsetAngle)) * radius + this.transform.position; }
                else { pos = new Vector3(Mathf.Cos(offsetAngle), 0, Mathf.Sin(offsetAngle)) * radius + this.transform.position; }
                pawns[i].transform.position = pos;
            }

            GetComponent<BoxCollider>().size = new Vector3(1.5f, 2f, 3.35f);
        }

        
    }

    public void AddPawnUniform(int index)
    {
        GameObject temp = Instantiate(pawnPrefabs[index], pawnPrefabs[index].transform.position, pawnPrefabs[index].transform.rotation, this.transform);
        pawns.Add(temp);
        ArrangePawns();
    }

    public void RemovePawnUniform()
    {
        GameObject pawn = pawns[0];
        pawns.Remove(pawn);
        Destroy(pawn);
        ArrangePawns();
    }


    public void TestAddPawn()
    {
        AddPawnUniform(0);
        testAddPawn = false;
    }

    public void TestRemovePawn()
    {
        RemovePawnUniform();
        testRemovePawn = false;
    }

    public void ResetPlayer()
    {
        foreach(var pawn in pawns)
        {
            pawns.Remove(pawn);
            Destroy(pawn);
        }

        GameObject temp = Instantiate(pawnPrefabs[0], pawnPrefabs[0].transform.position, pawnPrefabs[0].transform.rotation, this.transform);
        pawns.Add(temp);

    }

    public void IncreasePawns(int amount)
    {
        if (amount < 7)
        {
            AddPawnUniform(0);
        }
        
    }

    public void DecreasePawns(int amount)
    {
        if(amount > 0)
        {
            RemovePawnUniform();
        }
    }

    public void Shoot()
    {
        foreach (var pawn in pawns)
        {
           GameObject temp = Instantiate(projectilePrefab, pawn.transform.position + new Vector3(-1,UnityEngine.Random.Range(2, 3.5f), UnityEngine.Random.Range(-1,1f)), projectilePrefab.transform.rotation);
           temp.GetComponent<Projectile>().damage = damage;
        }
    }

    public void ApplyPowerUP(PowerUpType type, float value, bool addOrMultiply)
    {
        switch(type)
        {
            case PowerUpType.ATTACKSPEED:
                if(addOrMultiply)
                {
                    attackSpeed += value;
                }
                else
                {
                    attackSpeed = attackSpeed * value;
                }
                if(attackSpeed < 1) attackSpeed = 1;
                break;
            case PowerUpType.DAMAGE:
                if (addOrMultiply)
                {
                    damage += value;
                }
                else
                {
                    currentDamageMultiplier = currentDamageMultiplier + value;
                    damage = currentDamageMultiplier * damage;
                }
                if (damage < 0.5f) damage = 0.5f;
                break;
            case PowerUpType.PAWNINCREASE:
                    IncreasePawns((int)Mathf.Floor(value));
                break;
            case PowerUpType.HEALTH:
                    FindAnyObjectByType<GameManager>().AddHealth(Mathf.FloorToInt(value));
                break;

        }
    }
}
