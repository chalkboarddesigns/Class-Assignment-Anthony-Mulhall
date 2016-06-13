using UnityEngine;
using System.Collections;

public class Whales2 : MonoBehaviour
{
    
    public Transform target;


        
        public void seekTarget(Transform go, Transform target, int moveSpeed, float rotateSpeed, float radius)
        {
            //Find distance
            Vector3 direction = target.position - go.position;
            //Check if we are within the radius
            if (direction.magnitude < radius)
                return;
            //Move to the target
            go.position = Vector3.MoveTowards(go.position, target.position, moveSpeed * Time.deltaTime);
            //Rotate to the target
            go.rotation = Quaternion.RotateTowards(go.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
        }
    }
