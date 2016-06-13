using UnityEngine;
using System.Collections;

public class Flock : MonoBehaviour
{
    public float speed = 0.2f;
    public float rotationSpeed = 4.0f;

    Vector3 averageHeading;
    Vector3 averagePosition;
    Vector3 moveDirection;
    public GameObject targetToAvoid;
    public Transform targetTransform;
    float distanceToAvoid = 5;
    public bool seekEnabled, fleeEnabled, pursueEnabled;

    public float neighbourDistance = 5.0f;

    public float fleeSpeed = 2f;
    float amount;

    bool turning = false;


    // Use this for initialization
    void Start()
    {

        speed = Random.Range(0.5f, 2.5f);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        amount = ((targetToAvoid.transform.position) - (transform.position)).magnitude;
        moveDirection = ((targetToAvoid.transform.position) - (transform.position)).normalized;
        if (amount > 100)
        { // if player is more then 100 units away
            ApplyRules(); // normal system or what you want
        }
        else if (amount > 20)
        { // player is beetwen 20 and 100 units away
            Flee(); // animal has chased the player
        }

        // find the size of the tank if the fish move to the outside change turning to true set a random speed and the direction back to the centre of the tank 
        if (Vector3.Distance(transform.position, Vector3.zero) >= FishBehaviours.worldSize)
        {
            turning = true;
        }
        else
            turning = false;

        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            speed = Random.Range(0.5f, 1);


        }
        else
        {
            if (Random.Range(0, 5) < 1)
                ApplyRules(); // applys the flocking behaviours 1 in 5 times randomely


        }
        transform.Translate(0, 0, Time.deltaTime * speed); // mmove forward 
    }
    void ApplyRules()

    {
        GameObject[] gos; // gos stands for gameonjects 
        gos = FishBehaviours.allFish; // reference to the array allfish

        Vector3 center = Vector3.zero; // where is the centre of the group fish will move towards the centre of the group
        Vector3 vavoid = Vector3.zero; // set to avoid fish that are near prevents colisions while trying to get to the centre of the group

        float groupSpeed = 0.3f; // declare a groupspeed

        Vector3 goal = FishBehaviours.goal; // reference to the position set in Fish behaviours

        float dist;

        int groupSize = 0; // groupsize will be determined by whose close in a 2 distance as set earlier by neighbour distnace
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= neighbourDistance)
                {
                    center += go.transform.position; // add up the centre positons
                    groupSize++;                        // add up the group size

                    if (dist < 2.0f)        // if we are 1 unit away we are about to collide and avoid facing the way in opposite direction
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);

                    }
                    Flock anotherFlock = go.GetComponent<Flock>(); // grab the component of the other fish and set group speed 
                    groupSpeed = groupSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            center = center / groupSize + (goal - this.transform.position); // calcualate the acverage centre
            speed = groupSpeed / groupSize; // get the average speed of the group 

            Vector3 direction = (center + vavoid) - transform.position; // get the centre add the vector for avoid - our position which gives us the direction we need to turn into
            if (direction != Vector3.zero) // if the direction is not equal zero rotate towards the direction
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        }

        

    }
    void Flee()
    {
        transform.LookAt(new Vector3(targetToAvoid.transform.position.x, 0, targetToAvoid.transform.position.z));
        transform.position += -moveDirection * speed * Time.deltaTime;
    }

}