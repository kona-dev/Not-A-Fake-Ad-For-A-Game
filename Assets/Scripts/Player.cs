using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int speed;

    [SerializeField] public Vector3 basePosition = Vector3.zero;

    [SerializeField] public float radius;

    [SerializeField] public List<GameObject> pawns = new List<GameObject>();

    [SerializeField] public List<GameObject> pawnPrefabs = new List<GameObject>();

    [SerializeField] public bool testAddPawn;
    [SerializeField] public bool testRemovePawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(testAddPawn) { TestAddPawn(); }
        if(testRemovePawn) { TestRemovePawn(); }
            
        if(Input.GetKey(KeyCode.A) && gameObject.transform.position.z >= -8.93f) { gameObject.transform.Translate(new Vector3(0, 0, -Time.deltaTime * speed)); }
        if(Input.GetKey(KeyCode.D) && gameObject.transform.position.z <= 8.93f) { gameObject.transform.Translate(new Vector3(0, 0, Time.deltaTime * speed)); }
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
}
