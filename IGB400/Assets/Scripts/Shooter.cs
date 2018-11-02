using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shooter : MonoBehaviour {
    //Player Objects
    public GameObject player;
    private Rigidbody rigidbodyPlayer;
    public float playerDistanceScaleInitial;
    public float playerDistanceCurrent;

    //Bullet and images
    public GameObject bullet;
    public GameObject aimImage;


    //Checks
    public bool isInSight = false;
    public bool shot = false;
    public GameObject instancer;

    //Position and Direction values
    public Vector3 oldPos;
    public Vector3 direction;

    //Bullet values
    public float nextBullet = 0;
    private float rateOfFire = 1;
    public int numberOfHits;
    public int numberOfMiss;

    //List of distances.
    public List<float> listOfDistances = new List<float>();
    public float positionCheckTimer = 0.1f;
    public float nextCheck = 0;
    public float linearDistance;
    
	// Check for the rigidbody and determine the start distance.
	void Start () {
        rigidbodyPlayer = player.GetComponent<Rigidbody>();
        playerDistanceScaleInitial = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        InSight();
        
	}

    //Check if the player is in sight
    bool InSight()
    {
       
        if (instancer != null)
        {
            //Destroy(instancer);
        }
        RaycastHit hit;
        var rayDirection = player.transform.position - transform.position;

        linearDistance = LinearContinuity();

        if (Physics.Raycast(transform.position, rayDirection,out hit))
        {
            if (hit.transform.tag == "Player")
            {
                if (Time.time > nextBullet)
                {
                    //Divide the max distance by the current distance to obtain a number between 0 and 1
                    playerDistanceCurrent = (Vector3.Distance(this.gameObject.transform.position, player.transform.position) / playerDistanceScaleInitial);

                  

                    GameObject aimZone = new GameObject();
                    aimZone.transform.position = player.transform.position + (Direction(playerDistanceCurrent, linearDistance));
                    Shoot(aimZone);
                    nextBullet = Time.timeSinceLevelLoad + rateOfFire;
                    Destroy(aimZone);
                }
                oldPos = player.transform.position;
                return true;
                
            }
            else
            {
                oldPos = player.transform.position;
                return false;
            }
        } else
        {
            oldPos = player.transform.position;
            return false;
        }
    }

    //First component of the expected position. Direction of the player
    Vector3 Direction(float scale, float linearCoeff)
    {
        Vector3 playerDirection =  player.transform.position - oldPos;
        playerDirection.Normalize();
        playerDirection = playerDirection * (4 * scale) * linearCoeff;
        return playerDirection;
    }

    // Instantiate the bullet projectile and aim it at the expected playert position
    private void Shoot(GameObject shootPos)
    {
        GameObject instance = Instantiate(bullet, this.transform.position, this.transform.rotation) as GameObject;
        instance.transform.LookAt(shootPos.transform.position);
        instance.GetComponent<Rigidbody>().AddForce(instance.transform.forward * 7000);
    }


    //Will check the distance between the new and old position every tenth of seconds. It helps deals with player who're using small movement.
    private float LinearContinuity()
    {
       
        if (nextCheck < Time.timeSinceLevelLoad)
        {
            float newDistance = Vector3.Distance(player.transform.position, oldPos);
            if (newDistance > 0.15f)
            {
                newDistance = 0.14f;
            }
            listOfDistances.Add(newDistance);
            
            if (listOfDistances.Count > 20)
            {
                listOfDistances.RemoveRange(0, 1);
            }

            nextCheck = Time.timeSinceLevelLoad + positionCheckTimer;
            linearDistance = listOfDistances.Average() / 0.14f;
           
        }
        return linearDistance;
    }
}
