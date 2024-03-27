using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> pieces = new List<GameObject>();

    [SerializeField] public int speed;

    [SerializeField] public List<GameObject> currentPlacedObjects = new List<GameObject>();
    [SerializeField] public List<GameObject> currentPlacedPickups = new List<GameObject>();

    [SerializeField] public bool isActive;

    [SerializeField] public bool start;

    [SerializeField] public bool testSpawn;
    [SerializeField] public int spawnX;
    [SerializeField] public int despawnX;

   

    private void Start()
    {
        currentPlacedObjects.Clear();
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            if (currentPlacedObjects.Count > 0)
            {
                foreach (GameObject piece in currentPlacedObjects)
                {
                    piece.transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
                }

                if (currentPlacedObjects[currentPlacedObjects.Count - 1].transform.position.x >= spawnX + 25) SpawnPiece(GetRandomPiece());
                
            }

            if (currentPlacedPickups.Count > 0)
            {
                foreach (GameObject pickup in currentPlacedPickups)
                {
                    pickup.transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
                }

                if (currentPlacedPickups[0].transform.position.x >= despawnX + 25) DespawnPiece(currentPlacedPickups[0]);
            }
             
        }

        if (testSpawn) TestSpawn();

    }

    public GameObject GetRandomPiece() {  return pieces[Random.Range(0, pieces.Count)]; }

    public GameObject GetSpecificPiece(int index)
    {
        return pieces[index];
    }

    public void SpawnPiece(GameObject piece)
    {
        GameObject temp = Instantiate(piece, new Vector3(spawnX, 0, 0), piece.transform.rotation);
        temp.name = "PIECE" + currentPlacedObjects.Count;
        currentPlacedObjects.Add(temp);
    }

    public void SpawnPiece(GameObject piece, int x)
    {
        GameObject temp = Instantiate(piece, new Vector3(x, 0, 0), piece.transform.rotation);
        temp.name = "PIECE" + currentPlacedObjects.Count;
        currentPlacedObjects.Add(temp);
    }

    public void DespawnPiece(GameObject piece)
    {
        currentPlacedObjects.Remove(piece);
        Destroy(piece);
    }

    public void Reset()
    {
        currentPlacedObjects.Clear();
        isActive = false;
    }

    public void Setup()
    {
        Reset();

        for(int i = 0; i < (despawnX - spawnX) / 25; i++)
        {
            SpawnPiece(GetRandomPiece(), -spawnX - (i * 25) - 25);
        }

    }

    public void TestSpawn()
    {
        testSpawn = false;
        Setup();
        
    }


}
