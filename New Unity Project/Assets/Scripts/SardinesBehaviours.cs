using UnityEngine;
using System.Collections;

public class SardinesBehaviours : MonoBehaviour
{

    public GameObject SardinesPrefab;   // reference to the fish prefab 
    public static int worldSize = 10; // hardcoding in the world size 
    public Vector3 velocity; //direction 
    public float mass = 1f; //weight of our agent
    public float maxSpeed = 5f; //how fast it can travel
    public GameObject target;

    public static Vector3 goal = Vector3.zero; // the starting point the fish try to swim too

    static int numFish = 10;        // number of fish we want to instatiate at the start of the code
    public static GameObject[] allFish = new GameObject[numFish]; // a static array that can accesed from the flocking script, this will give a single fish awareness of the others around it in flocking script


    // Use this for initialization
    void Start()
    {
        // instatiate a preset number of fish in the world 
        // code assumes 0.0.0 is the centre of the world
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-worldSize, worldSize),
                                      Random.Range(-worldSize, worldSize),
                                      Random.Range(-worldSize, worldSize));
            allFish[i] = (GameObject)Instantiate(SardinesPrefab, pos, Quaternion.identity);
            // instantiated at a random position in a 10 * 10 area with the rotation set to prefab orientation 
        }
    }

    // Update is called once per frame
    void Update()
    {
        // resests the goal that the fish swim to and positions it at a random location in the world 
        if (Random.Range(0, 1000) < 50)
        {
            goal = new Vector3(Random.Range(-worldSize, worldSize),
                                      Random.Range(-worldSize, worldSize),
                                      Random.Range(-worldSize, worldSize));
        }
    }


}

