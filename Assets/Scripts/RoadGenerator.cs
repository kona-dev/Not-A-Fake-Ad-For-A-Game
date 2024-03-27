using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> pieces = new List<GameObject>();

    [SerializeField] public int speed;

    [SerializeField] public List<GameObject> currentPlacedObjects = new List<GameObject>();
    [SerializeField] public GameObject objectParent;
    [SerializeField] public List<GameObject> currentPlacedPickups = new List<GameObject>();

    [SerializeField] public GameObject barrierPrefab;

    [SerializeField] public List<GameObject> patterns = new List<GameObject>();

    [SerializeField] public bool isActive;

    [SerializeField] public bool start;

    [SerializeField] public bool testSpawn;
    [SerializeField] public int spawnX;
    [SerializeField] public int despawnX;

    [SerializeField] public float distDiff;
    [SerializeField] public bool isMakingPattern;
    [SerializeField] public List<int> currentPattern;
   
    private void Start()
    {
        currentPlacedObjects.Clear();
        isActive = false;
        isMakingPattern = false;
    }

    private void Update()
    {
      
       if (isActive)
       {
            
            /* Road Generation */ {


               
                if (currentPlacedObjects.Count > 0)
                {
                    foreach (GameObject piece in currentPlacedObjects)
                    {
                        piece.transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
                    }

                    if (currentPlacedObjects[currentPlacedObjects.Count - 1].transform.position.x >= spawnX + 25) SpawnPiece(GetRandomPiece());
                    if (currentPlacedObjects[0].transform.position.x >= despawnX + 25) DespawnPiece(currentPlacedObjects[0]);
                }

                
            }

            /* Obstacle / Pickup / Powerup Generation */ {

                distDiff += Time.deltaTime * speed;

                if (distDiff >= 25)
                {

                    if (currentPattern.Count == 0)
                    {
                        bool doPickupOrBarrier = Random.Range(1, 3) == 1 ? true : false;
                        if (doPickupOrBarrier)
                        {
                            currentPattern = CreatePickupPattern();
                            foreach (var i in currentPattern) Debug.Log(i);
                            GameObject temp = Instantiate(patterns[currentPattern[0]], new Vector3(spawnX, 0, 0), patterns[currentPattern[0]].transform.rotation);
                            foreach (Transform pickup in temp.transform) currentPlacedPickups.Add(pickup.gameObject);
                            currentPattern.RemoveAt(0);
                        }
                        else
                        {
                            bool leftOrRight = Random.Range(1, 3) == 1 ? true : false;
                            GameObject temp;
                            if (leftOrRight) {  temp = Instantiate(barrierPrefab, new Vector3(spawnX, 0, -5f), barrierPrefab.transform.rotation);  }
                            else { temp = Instantiate(barrierPrefab, new Vector3(spawnX, 0, 5f), barrierPrefab.transform.rotation); }
                            currentPlacedPickups.Add(temp);
                        }
                    }
                    else if (currentPattern.Count > 0)
                    {
                        GameObject temp = Instantiate(patterns[currentPattern[0]], new Vector3(spawnX, 0, 0), patterns[currentPattern[0]].transform.rotation);
                        foreach (Transform pickup in temp.transform)  currentPlacedPickups.Add(pickup.gameObject);
                        currentPattern.RemoveAt(0);
                    }
                    
                    distDiff = 0;
                }

               
                if (currentPlacedPickups.Count > 0)
                {
                    foreach (GameObject pickup in currentPlacedPickups)
                    {
                        pickup.transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
                    }

                    if (currentPlacedPickups[0].transform.position.x >= despawnX + 25) DespawnPickup(currentPlacedPickups[0]);
                }
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
        temp.transform.SetParent(objectParent.transform);
        temp.name = "PIECE" + currentPlacedObjects.Count;
        currentPlacedObjects.Add(temp);
    }

    public void SpawnPiece(GameObject piece, int x)
    {
        GameObject temp = Instantiate(piece, new Vector3(x, 0, 0), piece.transform.rotation);
        temp.transform.SetParent(objectParent.transform);
        temp.name = "PIECE" + currentPlacedObjects.Count;
        currentPlacedObjects.Add(temp);
    }

    public void DespawnPiece(GameObject piece)
    {
        currentPlacedObjects.Remove(piece);
        Destroy(piece);
    }

    public void DespawnPickup(GameObject pickup)
    {
        currentPlacedPickups.Remove(pickup);
        Destroy(pickup);
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

        for(int i = 0; i < (-50 + spawnX) / 25; i++)
        {

        }

    }

    public void TestSpawn()
    {
        testSpawn = false;
        Setup();
        
    }

    public List<int> CreatePickupPattern()
    {
        List<int> pattern = new List<int>();
        // First Roll
        int firstRoll = Random.Range(1, 9);
        pattern.Add(firstRoll);
        int secondRoll = 0;
        bool onlyOneRoll = Random.Range(1, 11) >= 7 ? true : false;
        if (onlyOneRoll) { return pattern; }
        List<int> possibleRolls = new List<int>();
        switch (firstRoll)
        {
            case 0:
                secondRoll = Random.Range(3, 5);
                break;
            case 1:
                possibleRolls = new List<int>() { 3, 7 };
                secondRoll = possibleRolls[Random.Range(0, 2)];
                break;
            case 2:
                possibleRolls = new List<int>() { 4, 8 };
                secondRoll = possibleRolls[Random.Range(0, 2)];
                break;
            case 3:
                secondRoll = 2;
                break;
            case 4:
                secondRoll = 1;
                break;
            case 5:
                possibleRolls = new List<int>() { 2, 8 };
                secondRoll = possibleRolls[Random.Range(0, 2)];
                break;
            case 6:
                possibleRolls = new List<int>() { 1, 7 };
                secondRoll = possibleRolls[Random.Range(0, 2)];
                break;
            case 7:
                possibleRolls = new List<int>() { 0, 5, 6 };
                secondRoll = possibleRolls[Random.Range(0, 3)];
                break;
            case 8:
                possibleRolls = new List<int>() { 0, 5, 6 };
                secondRoll = possibleRolls[Random.Range(0, 3)];
                break;
        }
        pattern.Add(secondRoll);
        return (pattern);

    }


}
